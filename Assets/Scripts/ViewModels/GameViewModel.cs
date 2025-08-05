using Lean.Localization;
using UniRx;
using Zenject;

public class GameViewModel
{
	private readonly TowerModel _towerModel;

	public Subject<string> Message { get; } = new();

	// Команды
	public ReactiveCommand<CubeModel> AddCubeCommand { get; } = new();
	public ReactiveCommand<CubeModel> RemoveCubeCommand { get; } = new();

	[Inject]
	public GameViewModel(TowerModel towerModel)
	{
		_towerModel = towerModel;

		AddCubeCommand.Subscribe(OnAddCube);
		RemoveCubeCommand.Subscribe(OnRemoveCube);
	}

	private void OnAddCube(CubeModel originalCube)
	{
		// Создаем новый CubeModel для башни
		CubeModel towerCube = new(originalCube.Index, originalCube.Color, originalCube.Id, originalCube.Size)
		{
			Size = originalCube.Size,
			Source = CubeSource.Tower,
			Position = originalCube.Position
		};

		if(_towerModel.TryAddCube(towerCube))
			ShowMessage(Strings.CubeAdded);
		else
		{
			var validationKey = _towerModel.GetValidationKey(towerCube);
			ShowMessage(validationKey);
		}
	}

	private void OnRemoveCube(CubeModel cube)
	{
		_towerModel.RemoveCube(cube);
		ShowMessage(Strings.CubeRemoved);
	}

	private void ShowMessage(string key)
	{
		var message = LeanLocalization.GetTranslationText(key);
		Message.OnNext(message);
	}
}