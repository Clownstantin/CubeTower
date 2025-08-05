using UnityEngine;

[System.Serializable]
public struct SerializableVector
{
	public float x, y, z;

	public SerializableVector(Vector3 vector)
	{
		x = vector.x;
		y = vector.y;
		z = vector.z;
	}

	public static implicit operator Vector3(SerializableVector serializableVector)
	=> new(serializableVector.x, serializableVector.y, serializableVector.z);

	public static implicit operator SerializableVector(Vector3 vector) => new(vector);
}