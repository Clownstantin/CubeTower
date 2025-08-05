using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/GameConfig")]
public class GameConfig : ScriptableObject
{
	[field: SerializeField] public int CubeCount { get; private set; }

	[field: SerializeField] public Color[] CubeColors { get; private set; }
}