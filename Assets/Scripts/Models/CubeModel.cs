using UnityEngine;

public class CubeModel
{
	public int Index { get; }
	public Color Color { get; }
	public string Id { get; }
	public float Size { get; set; }
	public CubeSource Source { get; set; }
	public Vector3 Position { get; set; }

	public CubeModel(int index, Color color, string id = default, float size = default)
	{
		Index = index;
		Color = color;
		Id = id;
		Size = size;
		Position = Vector3.zero;
		Source = CubeSource.Scroll;
	}
}

public enum CubeSource
{
	Scroll,
	Tower
}