public interface ICubePlacementRule
{
	bool CanPlaceCube(CubeModel cubeToPlace, TowerModel tower);
	string GetRuleKey();
}