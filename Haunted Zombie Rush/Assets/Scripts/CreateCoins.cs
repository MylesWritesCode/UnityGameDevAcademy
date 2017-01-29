using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCoins : MonoBehaviour {

	public GameObject coinPrefab;
	GameObject[] coins = new GameObject[10];
	// Use this for initialization
	void Start () 
	{
		// Instantiate coins at random heights.
		for (int i = 0; i < coins.Length; i++)
		{
			GameObject coin = Instantiate(coinPrefab);
			float yCoinPos = UnityEngine.Random.Range(3f, 20f);
			Vector3 startPosition = new Vector3((-5 * i) - 7, yCoinPos, 11.05f);
			coins[i] = coin;
			coin.transform.position = startPosition;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
