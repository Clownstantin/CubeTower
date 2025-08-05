public class ColorMatchingPlacementRule : ICubePlacementRule
{
	public bool CanPlaceCube(CubeModel cubeToPlace, TowerModel tower)
	{
		if(tower.Cubes.Count == 0)
			return true;

		var topCube = tower.Cubes[^1];
		return topCube.Color == cubeToPlace.Color;
	}

	public string GetRuleKey() => string.Empty;
}