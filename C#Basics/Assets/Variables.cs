using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variables : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        string heroName = "Myles";
        string equippedWeapon = "Infinity Gauntlet";
        string favoriteFurniture = "Throne";

        string favoritePlanet;

        favoritePlanet = "Earth";

        Debug.Log(favoritePlanet);

        Debug.Log(heroName);
        Debug.Log(equippedWeapon);
        Debug.Log(favoriteFurniture);

        Debug.Log(equippedWeapon.ToUpper());

        int hp = 100;
        float shieldPower = 76.5f;
        int laserDamage = 30;
        double actualDamagePercent = 0.05;
        int actualDamage = (int)(laserDamage * actualDamagePercent);
        hp -= actualDamage;
        shieldPower -= (laserDamage - actualDamage);

        Debug.Log("HP: " + hp);
        Debug.Log("Shield: " + shieldPower);

        int slices = 10 / 5;
        Debug.Log(slices);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
