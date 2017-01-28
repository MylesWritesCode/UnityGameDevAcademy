using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListsAndArrays : MonoBehaviour
{
    // This is one way to declare all the players in the game. 
    //string player1 = "Myles";
    //string player2 = "Mel";
    //string player3 = "Eric";
    //string player4 = "Amanda";
    
    // This is a better way to initialize an array.
    string[] players = { "Myles", "Mel", "Eric", "Amanda" };

    public GameObject cubePrefab;
    GameObject[] cubes = new GameObject[5];
    

	// Use this for initialization
	void Start ()
    {
        // This is a dumb way to call on each person in the list.
        // Debug.Log("Welcome: " + player1);
        // Debug.Log("welcome: " + player2);
        // Debug.Log("Welcome: " + player3);
        // Debug.Log("Welcome: " + player4);

        // Better way to do the above.
        // Debug.Log("Player one: " + players[0]);

        for (int i = 0; i < cubes.Length; i++)
        {
            GameObject cube = Instantiate(cubePrefab);
            cubes[i] = cube;
            cube.transform.position = new Vector3(i, 2*i, i);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
