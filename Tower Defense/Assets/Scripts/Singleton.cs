// Our generic class for GameManager and TowerManager
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	// The variable instance that we use in this script only.
	private static T instance;

	public static T Instance {
		get 
		{
			// If there is no instance of the script that is inheriting from Singleton.cs...
			if (instance == null)
			{
				// Set the instance to the script that is inheriting from Singleton.cs.
				instance = FindObjectOfType<T>();
			}
			// Else if the instance is not set to this...
			else if (instance != FindObjectOfType<T>())
			{
				// Destroy the running script because it should be already running.
				Destroy(FindObjectOfType<T>());
			}
			// Persistence throughout loads.
			DontDestroyOnLoad(FindObjectOfType<T>());
			// Return this instance to the child scripts.
			return instance;
		}
	}
}
