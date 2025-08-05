using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using Zenject;

public class TowerView : MonoBehaviour
{
	[SerializeField] private Transform _towerParent;

	private List<CubeView> _towerCubes = new();
	private TowerViewModel _towerViewModel;
	private CubeView.Pool _cubeViewPool;
	private AnimationService _animationService;
	private SaveService _saveService;

	[Inject]
	private void Construct(TowerViewModel towerViewModel, CubeView.Pool cubeViewPool, AnimationService animationService, SaveService saveService)
	{
		_towerViewModel = towerViewModel;
		_cubeViewPool = cubeViewPool;
		_animationService = animationService;
		_saveService = saveService;
	}

	private void Start()
	{
		_towerViewModel.Cubes.ObserveAdd().Subscribe(OnCubeAdded);
		_towerViewModel.Cubes.ObserveRemove().Subscribe(OnCubeRemoved);

		LoadTowerFromSave();
	}

	private void OnCubeAdded(CollectionAddEvent<CubeModel> addEvent)
	{
		CubeView cubeView = _cubeViewPool.Spawn(addEvent.Value, _towerParent);

		int index = addEvent.Index;
		float x = 0;
		float size = addEvent.Value.Size;

		if(index > 0)
		{
			float offset = size * 0.4f;
			x = Random.Range(-offset, offset);
		}

		Vector3 basePosition = new(x, index * size, 0);
		cubeView.transform.localPosition = basePosition;

		_animationService.AnimateCubePlacement(cubeView.transform, basePosition, () => addEvent.Value.Position = cubeView.transform.position);
		_towerCubes.Insert(index, cubeView);
	}

	private void OnCubeRemoved(CollectionRemoveEvent<CubeModel> removeEvent)
	{
		int index = removeEvent.Index;
		CubeView removedView = _towerCubes[index];
		_towerCubes.RemoveAt(index);

		_animationService.AnimateCubeIntoHole(removedView.transform, Input.mousePosition, () => _cubeViewPool.Despawn(removedView));
		_animationService.AnimateCubesDownFrom(index, removeEvent.Value.Size, _towerCubes, UpdatePositions);
	}

	private void UpdatePositions()
	{
		List<CubeModel> cubeModels = _towerViewModel.Cubes.ToList();

		if(cubeModels.Count == _towerCubes.Count)
			for(int i = 0; i < cubeModels.Count; i++)
				cubeModels[i].Position = _towerCubes[i].transform.position;
	}

	private void LoadTowerFromSave()
	{
		foreach(var cubeView in _towerCubes)
			_cubeViewPool.Despawn(cubeView);

		_towerCubes.Clear();
		_towerViewModel.TowerModel.LoadTower();
	}
}