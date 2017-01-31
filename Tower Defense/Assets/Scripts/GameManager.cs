using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager> 
{
	[SerializeField] private GameObject spawnPoint;
	[SerializeField] private GameObject[] enemies;
	[SerializeField] private int maxEnemiesOnScreen;
	[SerializeField] private int totalEnemies;
	[SerializeField] private int enemiesPerSpawn;

	private int enemiesOnScreen = 0;
	const float spawnDelay = 0.5f;

	// Use this for initialization
	void Start () 
	{
		StartCoroutine(Spawn());
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	IEnumerator Spawn()
	{
		for(int i = 0; i < enemiesPerSpawn; i++)
		{
			// Check if the enemies on screen is less than the desired amount of enemies.
			if (enemiesOnScreen < maxEnemiesOnScreen)
			{
				// Instantiate as GameObject, not as Object.
				GameObject newEnemy = Instantiate(enemies[0]) as GameObject;
				// Move GameObject newEnemy to starting position.
				newEnemy.transform.position = spawnPoint.transform.position;
				// Increment int enemiesOnScreen (so the if statement above will stop calling this method). 
				enemiesOnScreen++;
			}
		}
		yield return new WaitForSeconds(spawnDelay);
		StartCoroutine(Spawn());
	}

	public void RemoveEnemyFromScreen()
	{
		if (enemiesOnScreen > 0)
		{
			// Called from another Class where if the enemy goes past the finish line, this method is called to decrease the enemiesOnScreen value. 
			enemiesOnScreen--;
		}
	}
}
