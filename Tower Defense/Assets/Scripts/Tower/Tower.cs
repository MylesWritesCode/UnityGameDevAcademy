using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tower : MonoBehaviour 
{
	[SerializeField] private float attackSpeed;
	[SerializeField] private float attackRange;
	
	private Projectile projectile;
	private Enemy targetEnemy = null;
	private float attackCounter;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	private List<Enemy> GetEnemiesInRange()
	{
		List<Enemy> enemiesInRange = new List<Enemy>();
		foreach(Enemy enemy in GameManager.Instance.EnemyList)
		{
			if(Vector2.Distance(transform.position, enemy.transform.position) <= attackRange)
			{
				enemiesInRange.Add(enemy);
			}
		}
		return enemiesInRange;
	}

	private Enemy GetNearestEnemyInRange()
	{
		Enemy nearestEnemy = null;
		float closestEnemy = float.PositiveInfinity;
		foreach(Enemy enemy in GetEnemiesInRange())
		{
			if(Vector2.Distance(transform.position, enemy.transform.position) < closestEnemy)
			{
				closestEnemy = Vector2.Distance(transform.position, enemy.transform.position);
				nearestEnemy = enemy;
			}
		}
		return nearestEnemy;
	}
}
