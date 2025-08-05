using UnityEngine;

public class HeightLimitRule : ICubePlacementRule
{
	private readonly RectTransform _towerRect;
	private float? _maxTowerHeight;

	public HeightLimitRule(RectTransform towerRect) => _towerRect = towerRect;

	public bool CanPlaceCube(CubeModel cubeToPlace, TowerModel tower)
	{
		EnsureMaxHeight();
		float cubeSize = cubeToPlace.Size;
		float towerHeight = tower.Cubes.Count * cubeSize;
		return towerHeight + cubeSize <= _maxTowerHeight;
	}

	public string GetRuleKey() => Strings.TowerHeightReached;

	private void EnsureMaxHeight()
	{
		if(!_maxTowerHeight.HasValue)
		{
			float towerHeightPx = _towerRect.rect.height;
			_maxTowerHeight = towerHeightPx;
		}
	}
}