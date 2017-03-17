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
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
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
}
