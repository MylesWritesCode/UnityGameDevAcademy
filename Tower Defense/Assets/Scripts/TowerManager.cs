using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class TowerManager : Singleton<TowerManager> 
{
	// Type of TowerBtn from /Towers/TowerBtn.cs
	public TowerBtn towerBtnPressed {get; set;}
	private SpriteRenderer spriteRenderer;
	private List<Tower> TowerList = new List<Tower>();
	private List<Collider2D> BuildList = new List<Collider2D>();
	private Collider2D buildTile;

	// Use this for initialization
	void Start () 
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		buildTile = GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Getting input needs to be in Update(). If left-click on screen...
		if(Input.GetMouseButtonDown(0))
		{
			// Find the position of click and set to mapPoint.
			Vector2 mapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			// Find the distance between (0, 0) and the point that was touched (clicked). Set to hit.
			RaycastHit2D hit = Physics2D.Raycast(mapPoint, Vector2.zero);
			// If you're allowed to build on the hit point...
			if(hit.collider.tag == "BuildSite")
			{
				buildTile = hit.collider;
				// Changes collider tag so we can't build another tower there.
				buildTile.tag = "BuildSiteFull";
				RegisterBuildSite(buildTile);
				// Run PlaceTower() on hit (pos of click).
				PlaceTower(hit);
			}
		}
		if(spriteRenderer.enabled)
		{
			FollowMouse();
		}
	}

	public void RegisterBuildSite(Collider2D buildTag)
	{
		BuildList.Add(buildTag);
	}

	public void RegisterTower(Tower tower)
	{
		TowerList.Add(tower);
	}

	public void RenameTagsBuildSite()
	{
		foreach (Collider2D buildTag in BuildList)
		{
			buildTag.tag = "BuildSite";
		}
		BuildList.Clear();
	}

	public void DestroyAllTowers()
	{
		foreach (Tower tower in TowerList)
		{
			Destroy(tower.gameObject);
		}
		TowerList.Clear();
	}

	// Uses Raycast to place tower on map.
	public void PlaceTower(RaycastHit2D hit)
	{
		// If touch (click) is over a game object and tower button is pushed...
		if(!EventSystem.current.IsPointerOverGameObject() && towerBtnPressed != null)
		{
			// Instantiate tower based on which button is pushed. TowerObject from TowerBtn.cs.
			Tower newTower = Instantiate(towerBtnPressed.TowerObject);
			// Move TowerObject to raycasted position.
			newTower.transform.position = hit.transform.position;
			BuyTower(towerBtnPressed.TowerPrice);
			RegisterTower(newTower);
			DisableDragSprite();
		}
	}

	public void BuyTower(int price)
	{
		GameManager.Instance.RemoveMoney(price);
	}

	public void selectedTower(TowerBtn towerSelected)
	{
		if (towerSelected.TowerPrice <= GameManager.Instance.TotalMoney)
		{
			towerBtnPressed = towerSelected;
			EnableDragSprite(towerBtnPressed.DragSprite);
		}
	}

	public void FollowMouse()
	{
		transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		// Makes everything stay on the top of the screen.
		transform.position = new Vector2(transform.position.x, transform.position.y);
	}

	public void EnableDragSprite(Sprite sprite)
	{
		spriteRenderer.enabled = true;
		spriteRenderer.sprite = sprite;
	}

	public void DisableDragSprite()
	{
		spriteRenderer.enabled = false;
	}
}
