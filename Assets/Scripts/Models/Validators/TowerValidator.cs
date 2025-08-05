using System.Collections.Generic;
using System.Linq;

public class TowerValidator : ITowerValidator
{
	private readonly List<ICubePlacementRule> _placementRules;

	public TowerValidator(IEnumerable<ICubePlacementRule> placementRules) => _placementRules = placementRules.ToList();

	public bool CanAddCube(CubeModel cube, TowerModel tower) => _placementRules.All(rule => rule.CanPlaceCube(cube, tower));

	public string GetValidationKey(CubeModel cube, TowerModel tower)
	{
		ICubePlacementRule failedRule = null;
		foreach(ICubePlacementRule rule in _placementRules)
			if(!rule.CanPlaceCube(cube, tower))
				failedRule = rule;

		if(failedRule is null)
			return string.Empty;

		return failedRule.GetRuleKey();
	}
}