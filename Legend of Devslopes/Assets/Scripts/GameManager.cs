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
