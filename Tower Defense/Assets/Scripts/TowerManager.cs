using UnityEngine;
using UnityEngine.EventSystems;

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
		if (Input.GetMouseButtonDown(0))
		{
			Vector2 mapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(mapPoint, Vector2.zero);
			if(hit.collider.tag == "Build Site")
			{
				PlaceTower(hit);
			}
		}
	}

	public void PlaceTower(RaycastHit2D hit)
	{
		if(!EventSystem.current.IsPointerOverGameObject() && towerBtnPressed != null)
		{
			GameObject newTower = Instantiate(towerBtnPressed.TowerObject);
			newTower.transform.position = hit.transform.position;
		}
	}

	public void selectedTower(TowerBtn towerSelected)
	{
		towerBtnPressed = towerSelected;
		Debug.Log(towerBtnPressed.gameObject);
	}
}
