using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
	// This is to create a single instance of GameManager.cs
	public static GameManager instance = null;
	public GameObject spawnPoint;
	public GameObject[] enemies;
	public int maxEnemiesOnScreen;
	public int totalEnemies;
	public int enemiesPerSpawn;

	private int enemiesOnScreen = 0;

	void Awake()
	{
		// Makes sure that only one GameManager exists throughout the whole game.
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}
		// Object GameManager persists throughout loads.
		DontDestroyOnLoad(gameObject);
	}

	// Use this for initialization
	void Start () 
	{
		SpawnEnemy();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void SpawnEnemy()
	{
		// Check how much enemies to spawn and how many enemies are on screen.
		if (enemiesPerSpawn > 0 && enemiesOnScreen < totalEnemies)
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
		}
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
