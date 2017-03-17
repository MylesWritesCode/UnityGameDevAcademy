using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
	public static GameManager instance = null;

	[SerializeField] GameObject player;
	[SerializeField] GameObject[] spawnPoints;
	[SerializeField] GameObject tanker;
	[SerializeField] GameObject ranger;
	[SerializeField] GameObject soldier;
	[SerializeField] Text levelText;

	private bool gameOver = false;
	private int currentLevel;
	private float generatedSpawnTime = 1f;
	private float currentSpawnTime = 0f;
	private GameObject newEnemy;
	private List<EnemyHealth> enemies = new List<EnemyHealth>();
	private List<EnemyHealth> killedEnemies = new List<EnemyHealth>();

	public void RegisterEnemy(EnemyHealth enemy)
	{
		enemies.Add(enemy);
	}

	public void KilledEnemy(EnemyHealth enemy)
	{
		killedEnemies.Add(enemy);
	}

	public bool GameOver 
	{
		get { return gameOver; }
	}

	// Public getter for other scripts to get Player gameObject.
	public GameObject Player
	{
		get { return player; }
	}

	void Awake()
	{
		// Somewhat like a singleton from the last tutorial. This makes it so that you can't have more than one
		// GameManager open.
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
	}
	// Use this for initialization
	void Start () 
	{
		StartCoroutine(spawn());
		currentLevel = 1;
	}
	
	// Update is called once per frame
	void Update () 
	{
		currentSpawnTime += Time.deltaTime;
	}

	// Public getter for player hit to find out if the game is over or not.
	public void PlayerHit(int currentHP)
	{
		if (currentHP > 0)
		{
			gameOver = false;
		}
		else
		{
			gameOver = true;
		}
	}

	IEnumerator spawn()
	{
		Debug.Log("Spawn Coroutine Initiated.");
		Debug.Log("Enemy Register Count: " + enemies.Count);
		Debug.Log("Current Level: " + currentLevel);
		Debug.Log("Current Spawn Time: " + currentSpawnTime);
		Debug.Log("Generated Spawn Time: " + generatedSpawnTime);
		// Check that spawn time is greater than current time.
		if (currentSpawnTime > generatedSpawnTime)
		{
			currentSpawnTime = 0f;
			// If there are less enemies on screen than the current level...
			if (enemies.Count < currentLevel)
			{
				Debug.Log("Start Spawning...");
				int randomNumber = Random.Range(0,spawnPoints.Length - 1);
				// Randomly select a spawn point.
				GameObject spawnLocation = spawnPoints[randomNumber];
				// Spawn a random enemy.
				int randomEnemy = Random.Range(0, 3);
				if (randomEnemy == 0)
				{
					newEnemy = Instantiate(soldier) as GameObject;
				}
				else if (randomEnemy == 1)
				{
					newEnemy = Instantiate(ranger) as GameObject;
				}
				else if (randomEnemy == 2)
				{
					newEnemy = Instantiate(tanker) as GameObject;
				}
				// Move the new enemy to random spawn point.
				newEnemy.transform.position = spawnLocation.transform.position;
			}
			// If we killed the same number of enemies as the current level
			if (killedEnemies.Count == currentLevel)
			{
				// Clear out enemies and killedEnemies lists.
				enemies.Clear();
				killedEnemies.Clear();
				// Give us a breather instead of going straight into a new level.
				yield return new WaitForSeconds(3f);
				// Increment current level by 1.
				currentLevel++;
				levelText.text = "Level " + currentLevel;
			}
		}
		// Return null first before calling the function again, otherwise there's a crash.
		// It makes sense. If the IEnumerator doesn't return anything, it's just waiting, and if I decide
		// to call it again before I return anything, it's just going to be stuck in the first function spawn().
		yield return null;
		// Start spawn routine again.
		StartCoroutine(spawn());
	}
}
