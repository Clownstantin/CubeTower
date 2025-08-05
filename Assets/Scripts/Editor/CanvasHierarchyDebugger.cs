using UnityEngine;
using UnityEditor;

public class CanvasHierarchyDebugger : MonoBehaviour
{
	[ContextMenu("Debug Canvas Hierarchy")]
	public void DebugCanvasHierarchy()
	{
		Debug.Log("=== Debug Canvas Hierarchy ===");
		
		// Найти все Canvas в сцене
		var allCanvases = FindObjectsOfType<Canvas>();
		Debug.Log($"Найдено Canvas в сцене: {allCanvases.Length}");
		
		foreach(var canvas in allCanvases)
		{
			Debug.Log($"Canvas: {canvas.name} (RenderMode: {canvas.renderMode})");
		}
		
		// Проверить иерархию от текущего объекта
		var current = transform;
		int level = 0;
		
		while(current != null)
		{
			var canvas = current.GetComponent<Canvas>();
			if(canvas != null)
			{
				Debug.Log($"Canvas найден на уровне {level}: {canvas.name}");
			}
			
			current = current.parent;
			level++;
		}
		
		// Проверить GetComponentInParent
		var parentCanvas = GetComponentInParent<Canvas>();
		if(parentCanvas != null)
		{
			Debug.Log($"GetComponentInParent<Canvas>() вернул: {parentCanvas.name}");
		}
		else
		{
			Debug.LogWarning("GetComponentInParent<Canvas>() вернул null");
		}
	}
}

#if UNITY_EDITOR
[CustomEditor(typeof(CanvasHierarchyDebugger))]
public class CanvasHierarchyDebuggerEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		
		if(GUILayout.Button("Debug Canvas Hierarchy"))
		{
			((CanvasHierarchyDebugger)target).DebugCanvasHierarchy();
		}
	}
}
#endif 