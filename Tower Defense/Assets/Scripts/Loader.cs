using UnityEngine;

public class Loader : MonoBehaviour 
{
	// Just loads the GameManager from the Main Camera.
	public GameObject gameManager;

	void Awake()
	{
		if (GameManager.instance == null)
		{
			Instantiate(gameManager);
		}
	}
}
