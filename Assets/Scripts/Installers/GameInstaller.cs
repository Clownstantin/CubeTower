using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
	[SerializeField] private GameConfig _cubeConfig;
	[SerializeField] private CubeView _cubePrefab;
	[SerializeField] private Canvas _mainCanvas;
	[SerializeField] private TowerView _towerView;

	public override void InstallBindings()
	{
		Container.BindInstance(_cubeConfig).AsSingle();
		Container.BindInstance(_mainCanvas).AsSingle();

		//Пул кубиков
		Container.BindMemoryPool<CubeView, CubeView.Pool>()
				 .WithInitialSize(_cubeConfig.CubeCount)
				 .FromComponentInNewPrefab(_cubePrefab)
				 .UnderTransformGroup("CubePool");

		// Сервисы
		Container.Bind<SaveService>().AsSingle();
		Container.Bind<AnimationService>().AsSingle();

		// Правила размещения кубиков
		Container.Bind<ICubePlacementRule>().To<TowerPlacementRule>().AsSingle();
		Container.Bind<ICubePlacementRule>().To<HeightLimitRule>().AsSingle()
				 .WithArguments(_towerView.transform as RectTransform);

		// Валидатор
		Container.Bind<ITowerValidator>().To<TowerValidator>().AsSingle();

		// Модели и вью-модели
		Container.Bind<TowerModel>().AsSingle();
		Container.Bind<TowerViewModel>().AsSingle();
		Container.Bind<GameViewModel>().AsSingle();
	}
}