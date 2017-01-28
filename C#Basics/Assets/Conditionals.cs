using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conditionals : MonoBehaviour
{
    int attackerTowersRemaining = 2;
    bool attackerMainTowerDestroyed = false;
    int enemyTowersRemaining = 2;
    bool enemyMainTowerDestroyed = false;
    float timer = 200;
    bool gameOver = false;
	// Use this for initialization
	void Start ()
    {
        if (attackerMainTowerDestroyed || enemyMainTowerDestroyed)
        {
            if (attackerMainTowerDestroyed)
            {
                Debug.Log("Defender Wins by Main Tower Kill");
            } 
            else
            {
                Debug.Log("Attacker Wins by Main Tower Kill");
            }
        }
        else if (timer <= 0)
        {
            gameOver = true;
        }

        if (gameOver)
        {
            if (attackerTowersRemaining > enemyTowersRemaining)
            {
                Debug.Log("Attacker Wins.");
            }
            else
            {
                Debug.Log("Defender Wins.");
            }
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
