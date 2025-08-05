using UniRx;
using Zenject;

public class TowerModel
{
	private ReactiveCollection<CubeModel> _cubes = new();
	private ITowerValidator _validator;
	private SaveService _saveService;

	public IReadOnlyReactiveCollection<CubeModel> Cubes => _cubes;

	[Inject]
	public TowerModel(ITowerValidator validator, SaveService saveService)
	{
		_validator = validator;
		_saveService = saveService;
	}

	public bool TryAddCube(CubeModel cube)
	{
		if(!_validator.CanAddCube(cube, this))
			return false;

		_cubes.Add(cube);
		return true;
	}

	public void RemoveCube(CubeModel cube)
	{
		if(_cubes.Count > 0)
			_cubes.Remove(cube);
	}

	public string GetValidationKey(CubeModel cube) => _validator.GetValidationKey(cube, this);

	public void LoadTower()
	{
		SaveData saveData = _saveService.Load();
		if(saveData is null)
			return;

		_cubes.Clear();

		foreach(CubeSaveData cube in saveData.Cubes)
			_cubes.Add(cube);
	}

	public void SaveTower()
	{
		SaveData saveData = _saveService.SaveData;
		saveData.Cubes.Clear();

		foreach(CubeModel cube in _cubes)
			saveData.Cubes.Add(cube);

		_saveService.Save();
	}
}