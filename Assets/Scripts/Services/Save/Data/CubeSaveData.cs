[System.Serializable]
public struct CubeSaveData
{
	public int index;
	public SerializableColor color;
	public string id;
	public float size;
	public CubeSource source;
	public SerializableVector position;

	public CubeSaveData(CubeModel cube)
	{
		index = cube.Index;
		color = cube.Color;
		id = cube.Id;
		size = cube.Size;
		source = cube.Source;
		position = cube.Position;
	}

	public static implicit operator CubeModel(CubeSaveData data) => new(data.index, data.color, data.id, data.size)
	{
		Position = data.position,
		Source = data.source,
	};

	public static implicit operator CubeSaveData(CubeModel cube) => new(cube);
}
