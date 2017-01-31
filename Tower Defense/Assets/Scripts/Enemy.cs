using UnityEngine;

public class Enemy : MonoBehaviour 
{
	public int target = 0;
	public Transform exitPoint;
	public Transform[] waypoints;
	public float navigationUpdate;

	private Transform enemy;
	private float navigationTime = 0;
	// Use this for initialization
	void Start () 
	{
		enemy = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (waypoints != null)
		{
			navigationTime += Time.deltaTime;
			if (navigationTime > navigationUpdate)
			{
				// [Updated from OnTriggerEnter]
				// Figure out how many waypoints are in script, and if target is less than that...
				if (target < waypoints.Length) 
				{
					// MoveTowards target
					enemy.position = Vector2.MoveTowards(enemy.position, waypoints[target].position, navigationTime);
				}
				else 
				{
					// No new target exists, MoveTowards exitPoint
					enemy.position = Vector2.MoveTowards(enemy.position, exitPoint.position, navigationTime);
				}
				navigationTime = 0;
			}
		}
	}

	// Triger enter for waypoints.
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Checkpoint")
		{
			// Increment int target for Update().
			target++;
		}
		else if (other.tag == "Finish")
		{
			// Let GameManager know that there's one less enemy on screen.
			GameManager.Instance.RemoveEnemyFromScreen();
			// Destroy other gameObject.
			Destroy(gameObject);
		}
	}
}
