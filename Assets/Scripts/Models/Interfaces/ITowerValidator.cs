public interface ITowerValidator
{
	bool CanAddCube(CubeModel cube, TowerModel tower);
	string GetValidationKey(CubeModel cube, TowerModel tower);
}