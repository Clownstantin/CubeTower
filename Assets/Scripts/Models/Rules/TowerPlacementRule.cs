using UnityEngine;

public class TowerPlacementRule : ICubePlacementRule
{
	private Bounds _bounds;

	public bool CanPlaceCube(CubeModel cubeToPlace, TowerModel tower)
	{
		if(tower.Cubes.Count == 0)
			return true;

		CubeModel topCube = tower.Cubes[^1];
		float cubeSize = topCube.Size;
		Vector3 topCubePos = topCube.Position;
		Vector3 cubeToPlacePos = cubeToPlace.Position;

		_bounds = new(topCubePos, new Vector2(cubeSize, cubeSize));
		_bounds.Encapsulate(topCubePos + (Vector3.up * cubeSize));

		return _bounds.Contains(cubeToPlacePos);
	}

	public string GetRuleKey() => Strings.CanPlaceOnlyOnAnotherCube;
}