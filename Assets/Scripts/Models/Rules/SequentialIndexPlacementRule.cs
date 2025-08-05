public class SequentialIndexPlacementRule : ICubePlacementRule
{
	public bool CanPlaceCube(CubeModel cubeToPlace, TowerModel tower)
	{
		// Если башня пустая, можно разместить любой кубик
		if(tower.Cubes.Count == 0)
			return true;

		var topCube = tower.Cubes[^1];
		return cubeToPlace.Index > topCube.Index;
	}

	public string GetRuleKey() => string.Empty;
}