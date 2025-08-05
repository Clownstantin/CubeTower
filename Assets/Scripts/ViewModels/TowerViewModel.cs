using UniRx;

public class TowerViewModel
{
    public TowerModel TowerModel { get; }
    public IReadOnlyReactiveCollection<CubeModel> Cubes => TowerModel.Cubes;

    public TowerViewModel(TowerModel towerModel)
    {
        TowerModel = towerModel;
    }
} 