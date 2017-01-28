using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UptownFunctionYouUp : MonoBehaviour 
{

	int health = 100;
	bool shield = true;
	int shieldAmount = 15;
	int enemyAttackDamage = 25;

	void Start()
	{
		Debug.Log("Health: " + health);
	}

	public void Attack()
	{
		int damageToInflict = GetAttackDamage(shield, shieldAmount, enemyAttackDamage);
		health -= damageToInflict;
		Debug.Log("Health: " + health);
	}

	public void Heal()
	{
		int healAmount = GetRandomNumber();
		health += healAmount;
		Debug.Log("You were healed for " + healAmount);
		Debug.Log("Health: " + health);
		
	}

	private int GetAttackDamage(bool isShieldOn, int theShieldAmount, int enemyAttackDamage)
	{
		int damage = 0;

		if (isShieldOn)
		{
			damage = enemyAttackDamage - theShieldAmount;
		}
		else
		{
			damage = enemyAttackDamage;
		}

		return damage;
	}

	private int GetRandomNumber()
	{
		return Random.Range(2, 10);
	}
}
