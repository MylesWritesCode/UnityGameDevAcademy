using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimationManager : MonoBehaviour {

	[SerializeField] private List<GameObject> cameras = new List<GameObject>();

	public static AnimationManager instance = null;

	// Use this for initialization
	void Start () {
		if (instance == null)	{
			instance = this;
		}	else if (instance != this) {
			Destroy(gameObject);
		}
	}

	void StartNextCamera() {
		if (cameras.Count > 0) {
			cameras[0].SetActive(true);
		}
	}

	void DestroyCamera(GameObject camera)	{
		// Remove the first camera from the list.
		cameras.RemoveAt(0);
		Destroy(camera);
		StartNextCamera();
	}
}
