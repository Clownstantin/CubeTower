using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class CubeView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	[SerializeField] private Image _cubeImage;

	private RectTransform _rectTransform;
	private GameViewModel _gameViewModel;
	private CubeModel _cubeModel;
	private Canvas _canvas;
	private CanvasGroup _canvasGroup;
	private Transform _originalParent;
	private AnimationService _animationService;
	private List<RaycastResult> _raycastResults = new();
	private Vector3 _originalPosition;


	private void Awake()
	{
		_rectTransform = transform as RectTransform;
		_canvasGroup = GetComponent<CanvasGroup>();

		if(_canvasGroup == null)
			_canvasGroup = gameObject.AddComponent<CanvasGroup>();
	}

	private void Start() => _rectTransform.localScale = Vector3.one;

	[Inject]
	public void Construct(GameViewModel gameViewModel, Canvas canvas, AnimationService animationService)
	{
		_gameViewModel = gameViewModel;
		_animationService = animationService;
		_canvas = canvas;
	}

	public void Initialize(CubeModel cubeModel)
	{
		_cubeModel = cubeModel;
		_cubeImage.color = _cubeModel.Color;
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		_originalPosition = _rectTransform.position;
		_originalParent = _rectTransform.parent;
		_rectTransform.SetParent(_canvas.transform);

		_canvasGroup.alpha = 0.6f;
		_canvasGroup.blocksRaycasts = false;
	}

	public void OnDrag(PointerEventData eventData)
	{
		Vector3 pos = eventData.position;
		_cubeModel.Position = pos;
		_rectTransform.position = pos;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		_canvasGroup.alpha = 1f;
		_canvasGroup.blocksRaycasts = true;

		// Проверяем, куда уронили кубик
		EventSystem.current.RaycastAll(eventData, _raycastResults);

		Vector3 holePosition = Vector3.zero;
		bool isHole = false;
		bool isPlaced = false;

		foreach(RaycastResult result in _raycastResults)
		{
			if(result.gameObject.CompareTag(Strings.TowerTag) && _cubeModel.Source == CubeSource.Scroll)
			{
				isPlaced = true;
				_gameViewModel.AddCubeCommand.Execute(_cubeModel);
				break;
			}
			else if(result.gameObject.CompareTag(Strings.HoleTag) && _cubeModel.Source == CubeSource.Tower)
			{
				isHole = true;
				holePosition = result.gameObject.transform.position;

				// Проверяем источник кубика
				_gameViewModel.RemoveCubeCommand.Execute(_cubeModel);
				break;
			}
		}

		if(!isHole && !isPlaced)
			_animationService.AnimateCubeDestruction(_rectTransform, () => ResetCube());
		else
			ResetCube();
	}

	private void ResetCube()
	{
		_rectTransform.position = _originalPosition;
		_rectTransform.SetParent(_originalParent);
		_rectTransform.SetSiblingIndex(_cubeModel.Index);
		transform.localScale = Vector3.one;
		_canvasGroup.alpha = 1f;
	}

	public class Pool : MonoMemoryPool<CubeModel, Transform, CubeView>
	{
		protected override void Reinitialize(CubeModel cubeModel, Transform parent, CubeView cubeView)
		{
			cubeView.Initialize(cubeModel);
			cubeView.transform.SetParent(parent);
		}
	}
}