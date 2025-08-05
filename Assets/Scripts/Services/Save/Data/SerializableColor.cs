using UnityEngine;

[System.Serializable]
public struct SerializableColor
{
	public float r;
	public float g;
	public float b;
	public float a;

	public SerializableColor(Color color)
	{
		r = color.r;
		g = color.g;
		b = color.b;
		a = color.a;
	}

	public static implicit operator Color(SerializableColor serializableColor)
	=> new(serializableColor.r, serializableColor.g, serializableColor.b, serializableColor.a);

	public static implicit operator SerializableColor(Color color) => new(color);
}
