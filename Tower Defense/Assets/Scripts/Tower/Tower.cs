using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tower : MonoBehaviour 
{
	[SerializeField] private float attackSpeed;
	[SerializeField] private float attackRange;
	[SerializeField] private Projectile projectile;

	private Enemy targetEnemy = null;
	private float attackSpeedTimer;
	private bool isAttacking = false;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		attackSpeedTimer -= Time.deltaTime;
		if (targetEnemy == null)
		{
			Enemy nearestEnemy = GetNearestEnemyInRange();
			if (nearestEnemy != null && Vector2.Distance(transform.localPosition, nearestEnemy.transform.localPosition) <= attackRange)
			{
				targetEnemy = GetNearestEnemyInRange();
			}
		}
		else
		{
			if (attackSpeedTimer <= 0)
			{
				isAttacking = true;
				attackSpeedTimer = attackSpeed;
			}
			else
			{
				isAttacking = false;
			}
			if (Vector2.Distance(transform.localPosition, targetEnemy.transform.localPosition) > attackRange)
			{
				targetEnemy = null;
			}
		}
	}

	void FixedUpdate()
	{
		if (isAttacking)
		{
			Attack();
		}
	}

	private List<Enemy> GetEnemiesInRange()
	{
		List<Enemy> enemiesInRange = new List<Enemy>();
		foreach (Enemy enemy in GameManager.Instance.EnemyList)
		{
			if (Vector2.Distance(transform.localPosition, enemy.transform.localPosition) <= attackRange)
			{
				enemiesInRange.Add(enemy);
			}
		}
		return enemiesInRange;
	}

	public void Attack()
	{
		isAttacking = false;
		Projectile newProjectile = Instantiate(projectile) as Projectile;
		newProjectile.transform.localPosition = transform.localPosition;
		if (targetEnemy == null)
		{
			Destroy(newProjectile);
		}
		else
		{
			StartCoroutine(MoveProjectile(newProjectile));
		}
	}

	IEnumerator MoveProjectile(Projectile projectile)
	{
		while(GetTargetDistance(targetEnemy) > 0.20f && projectile != null && targetEnemy != null)
		{
			var dir = targetEnemy.transform.localPosition - transform.localPosition;
			var angleDirection = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

			projectile.transform.rotation = Quaternion.AngleAxis(angleDirection, Vector3.forward);
			projectile.transform.localPosition = Vector2.MoveTowards(projectile.transform.localPosition, targetEnemy.transform.localPosition, 5f * Time.deltaTime);
			yield return null;
		}
		if (projectile != null || targetEnemy == null)
		{
			Destroy(projectile);
		}
	}

	private float GetTargetDistance(Enemy enemy)
	{
		if (enemy == null)
		{
			enemy = GetNearestEnemyInRange();
			if (enemy == null)
			{
				return 0f;
			}
		}
		return Mathf.Abs(Vector2.Distance(transform.localPosition, enemy.transform.localPosition));
	}

	private Enemy GetNearestEnemyInRange()
	{
		Enemy nearestEnemy = null;
		float closestEnemy = float.PositiveInfinity;
		foreach (Enemy enemy in GetEnemiesInRange())
		{
			if (Vector2.Distance(transform.localPosition, enemy.transform.localPosition) < closestEnemy)
			{
				closestEnemy = Vector2.Distance(transform.localPosition, enemy.transform.localPosition);
				nearestEnemy = enemy;
			}
		}
		return nearestEnemy;
	}
}
