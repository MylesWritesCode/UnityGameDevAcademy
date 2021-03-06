﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public enum gameStatus
{
	next, play, gameover, win
};

public class GameManager : Singleton<GameManager> 
{
	[SerializeField] private int totalWaves = 10;
	[SerializeField] private Text totalMoneyLbl;
	[SerializeField] private Text currentWaveLbl;
	[SerializeField] private Text playBtnLbl;
	[SerializeField] private Button playBtn;
	[SerializeField] private Text totalEscapedLbl;
	[SerializeField] private GameObject spawnPoint;
	[SerializeField] private Enemy[] enemies;
	// [SerializeField] private int maxEnemiesOnScreen;
	[SerializeField] private int totalEnemies = 3;
	[SerializeField] private int enemiesPerSpawn;

	public List<Enemy> EnemyList = new List<Enemy>();
	
	private int waveNumber = 0;
	private int totalMoney = 10;
	private int totalEscaped = 0;
	private int roundEscaped = 0;
	private int totalKilled = 0;
	private int whichEnemiesToSpawn = 0;
	private int enemiesToSpawn = 0;
	private gameStatus currentState = gameStatus.play;
	private AudioSource audioSource;
	// private int enemiesOnScreen = 0;
	const float spawnDelay = 0.5f;

	public int TotalEscaped 
	{
		get 
		{
			return totalEscaped;
		}
		set
		{
			totalEscaped = value;
		}
	}

	public int RoundEscaped 
	{
		get
		{
			return roundEscaped;
		}
		set
		{
			roundEscaped = value;
		}
	}

	public int TotalKilled
	{
		get
		{
			return totalKilled;
		}
		set 
		{
			totalKilled = value;
		}
	}

	public int TotalMoney
	{
		get
		{
			return totalMoney;
		}
		set
		{
			totalMoney = value;
			totalMoneyLbl.text = totalMoney.ToString();
		}
	}

	public AudioSource AudioSource
	{
		get
		{
			return audioSource;
		}
	}

	// Use this for initialization
	void Start () 
	{
		playBtn.gameObject.SetActive(false);
		audioSource = GetComponent<AudioSource>();
		ShowMenu();
		// StartCoroutine(Spawn());
	}
	
	// Update is called once per frame
	void Update () 
	{
		HandleEscape();
	}

	IEnumerator Spawn()
	{
		for (int i = 0; i < enemiesPerSpawn; i++)
		{
			// Check if the enemies on screen is less than the desired amount of enemies.
			if (EnemyList.Count < totalEnemies)
			{
				// Instantiate as GameObject, not as Object.
				Enemy newEnemy = Instantiate(enemies[Random.Range(0, enemiesToSpawn)]);
				// Move GameObject newEnemy to starting position.
				newEnemy.transform.position = spawnPoint.transform.position;
			}
		}
		yield return new WaitForSeconds(spawnDelay);
		StartCoroutine(Spawn());
	}

	public void RegisterEnemy(Enemy enemy)
	{
		EnemyList.Add(enemy);
	}

	public void UnregisterEnemy(Enemy enemy)
	{
		EnemyList.Remove(enemy);
		Destroy(enemy.gameObject);
	}

	public void DestroyAllEnemies()
	{
		foreach(Enemy enemy in EnemyList)
		{
			Destroy(enemy.gameObject);
		}
		EnemyList.Clear();
	}

	public void AddMoney(int amount)
	{
		TotalMoney += amount;
	}

	public void RemoveMoney(int amount)
	{
		TotalMoney -= amount;
	}

	public void IsWaveOver()
	{
		// UI Label for total enemies escaped.
		totalEscapedLbl.text = "Escaped " + TotalEscaped + "/10";
		if ((RoundEscaped + TotalKilled) == totalEnemies)
		{
			if (waveNumber <= enemies.Length)
			{
				// Picks the item in enemy array to spawn.
				enemiesToSpawn = waveNumber;
			}
			SetCurrentGameState();
			ShowMenu();
		}
	}

	// Method for calling game states from enumerator.
	public void SetCurrentGameState()
	{
		if (TotalEscaped >= 10)
		{
			currentState = gameStatus.gameover;
		}
		else if (waveNumber == 0 && (TotalKilled + RoundEscaped) == 0)
		{
			currentState = gameStatus.play;
		}
		else if (waveNumber >= totalWaves)
		{
			currentState = gameStatus.win;
		}
		else
		{
			currentState = gameStatus.next;
		}
	}

	public void ShowMenu()
	{
		switch(currentState)
		{
			case gameStatus.gameover:
				playBtnLbl.text = "Play Again";
				// Play gameover sound.
				GameManager.Instance.AudioSource.PlayOneShot(SoundManager.Instance.GameOver);
				break;

			case gameStatus.next:
				playBtnLbl.text = "Next Wave";
				break;

			case gameStatus.play:
				playBtnLbl.text = "Play";
				break;

			case gameStatus.win:
				playBtnLbl.text = "Play";
				break;
		}
		playBtn.gameObject.SetActive(true);
	}

	public void PlayBtnPressed()
	{
		switch (currentState)
		{
			// Case when play button is pressed for next wave.
			case gameStatus.next:
				waveNumber++;
				totalEnemies += waveNumber;
				break;
			// Reset when the game is first played or finished.
			default:
			totalEnemies = 3;
			TotalEscaped = 0;
			TotalMoney = 10;
			enemiesToSpawn = 0;
			TowerManager.Instance.DestroyAllTowers();
			TowerManager.Instance.RenameTagsBuildSite();
			totalMoneyLbl.text = TotalMoney.ToString();
			totalEscapedLbl.text = "Escaped " + TotalEscaped + "/10";
			audioSource.PlayOneShot(SoundManager.Instance.NewGame);
			break;
		}
		DestroyAllEnemies();
		TotalKilled = 0;
		RoundEscaped = 0;
		currentWaveLbl.text = "Wave " + (waveNumber + 1);
		StartCoroutine(Spawn());
		playBtn.gameObject.SetActive(false);
	}

	private void HandleEscape()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			TowerManager.Instance.DisableDragSprite();
			TowerManager.Instance.towerBtnPressed = null;
		}
	}
}
