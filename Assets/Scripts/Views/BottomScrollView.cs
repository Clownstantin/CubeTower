using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BottomScrollView : MonoBehaviour
{
	[SerializeField] private ScrollRect _scrollRect;
	[SerializeField] private Transform _contentParent;

	private List<CubeView> _cubeViews = new();
	private GameConfig _cubeConfig;
	private CubeView.Pool _cubeViewPool;

	[Inject]
	private void Construct(GameConfig cubeConfig, CubeView.Pool cubeViewPool)
	{
		_cubeConfig = cubeConfig;
		_cubeViewPool = cubeViewPool;
	}

	private void Start() => CreateCubes();

	private void CreateCubes()
	{
		for(int i = 0; i < _cubeConfig.CubeCount; i++)
		{
			var colorIndex = i % _cubeConfig.CubeColors.Length;
			var cubeModel = new CubeModel(i, _cubeConfig.CubeColors[colorIndex], $"cube_{i}");

			CubeView cubeView = _cubeViewPool.Spawn(cubeModel, _contentParent);
			cubeModel.Position = cubeView.transform.position;
			cubeModel.Size = (cubeView.transform as RectTransform).rect.height;
			_cubeViews.Add(cubeView);
		}
	}
}