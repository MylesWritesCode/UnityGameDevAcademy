using UnityEngine;

public class Loader : MonoBehaviour 
{
	void Awake()
	{

	}

	void Start()
	{
		PrintValue("Myles");
	}

	public void PrintValue(string value)
	{
		Debug.Log("Value: " + value);
	}
}
