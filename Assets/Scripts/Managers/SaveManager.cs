using UnityEngine;
using Zenject;

public class SaveManager : MonoBehaviour
{
	[Inject] private readonly TowerModel _towerModel;

	private void OnApplicationPause(bool pauseStatus)
	{
		if(pauseStatus)
			_towerModel.SaveTower();
	}

	private void OnApplicationFocus(bool hasFocus)
	{
		if(!hasFocus)
			_towerModel.SaveTower();
	}

	private void OnApplicationQuit() => _towerModel.SaveTower();
}