using UnityEngine;
using UnityEngine.EventSystems;

public class TowerManager : Singleton<TowerManager> 
{
	// Type of TowerBtn from /Towers/TowerBtn.cs
	public TowerBtn towerBtnPressed {get; set;}
	private SpriteRenderer spriteRenderer;
	// Tower cost

	// Use this for initialization
	void Start () 
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
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
				// Changes collider tag so we can't build another tower there.
				hit.collider.tag = "BuildSiteFull";
				// Run PlaceTower() on hit (pos of click).
				PlaceTower(hit);
			}
		}
		if(spriteRenderer.enabled)
		{
			FollowMouse();
		}
	}

	// Uses Raycast to place tower on map.
	public void PlaceTower(RaycastHit2D hit)
	{
		// If touch (click) is over a game object and tower button is pushed...
		if(!EventSystem.current.IsPointerOverGameObject() && towerBtnPressed != null)
		{
			// Instantiate tower based on which button is pushed. TowerObject from TowerBtn.cs.
			GameObject newTower = Instantiate(towerBtnPressed.TowerObject);
			// Move TowerObject to raycasted position.
			newTower.transform.position = hit.transform.position;
			DisableDragSprite();
		}
	}

	public void selectedTower(TowerBtn towerSelected)
	{
		towerBtnPressed = towerSelected;
		EnableDragSprite(towerBtnPressed.DragSprite);
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
