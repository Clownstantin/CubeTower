using System.IO;
using UnityEngine;

public static partial class Strings
{
	#region Save
	public static string SavePath => Path.Combine(Application.persistentDataPath, "save.json");
	public static string StoredPath => Path.Combine(Application.persistentDataPath, "storedSave.json");
	#endregion

	#region Tags
	public const string TowerTag = "TowerArea";
	public const string HoleTag = "HoleArea";
	#endregion

	#region Localization
	public const string CubeAdded = nameof(CubeAdded);
	public const string CubeRemoved = nameof(CubeRemoved);
	public const string TowerHeightReached = nameof(TowerHeightReached);
	public const string CanPlaceOnlyOnAnotherCube = nameof(CanPlaceOnlyOnAnotherCube);
	#endregion
}