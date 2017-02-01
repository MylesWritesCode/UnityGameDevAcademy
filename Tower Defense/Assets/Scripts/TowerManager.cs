using UnityEngine;

public class TowerManager : Singleton<TowerManager> 
{
	// Type of TowerBtn from /Towers/TowerBtn.cs
	private TowerBtn towerBtnPressed;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void selectedTower(TowerBtn towerSelected)
	{
		towerBtnPressed = towerSelected;
		Debug.Log(towerBtnPressed.gameObject);
	}
}
