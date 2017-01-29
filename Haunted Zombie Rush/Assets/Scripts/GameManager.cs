using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour 
{
	public static GameManager instance = null;

	[SerializeField] private GameObject mainMenu;
	[SerializeField] private GameObject endMenu;

	private bool playerActive = false;
	private bool gameOver = false;
	// Was a public for some reason. I don't know why. There's an accessor here.
	private bool gameStarted = false;
	private bool gameRestarted = false;

	public bool PlayerActive { get { return playerActive; }	}
	public bool GameOver {	get { return gameOver; } }
	public bool GameStarted { get { return gameStarted; } }
	public bool GameRestarted { get { return gameRestarted; } }

	void Awake() 
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}
		// Persistance among all scenes.
		DontDestroyOnLoad(gameObject);

		Assert.IsNotNull(mainMenu);
		Assert.IsNotNull(endMenu);
	}

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void PlayerCollided()
	{
		gameOver = true;
		endMenu.SetActive(true);
	}

	public void PlayerStartedGame()
	{
		playerActive = true;
	}

	public void EnterGame()
	{
		mainMenu.SetActive(false);
		endMenu.SetActive(false);
		gameStarted = true;
	}

	public void ShowMainMenu()
	{
		endMenu.SetActive(false);
		mainMenu.SetActive(true);
	}

	public void ResetStage()
	{
		gameRestarted = true;
		gameOver = false;
		playerActive = false;
		EnterGame();
	}

	public void SetResetFalse()
	{
		gameRestarted = false;
	}
}
