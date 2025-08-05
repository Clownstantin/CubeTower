using System.IO;
using UnityEngine;

public class SaveService
{
	public SaveData SaveData = new();

	public SaveData Load()
	{
		string path = Strings.SavePath;
		if(!File.Exists(path))
			return null;

		string text = File.ReadAllText(path);
		SaveData = JsonUtility.FromJson<SaveData>(text);
		return SaveData;
	}

	public void Save()
	{
		string json = JsonUtility.ToJson(SaveData);
		File.WriteAllText(Strings.SavePath, json);
	}

	public void Clear() => File.Delete(Strings.SavePath);
}