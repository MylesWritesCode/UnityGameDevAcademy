// Class controls all enemies. 
using UnityEngine;

public class Enemy : MonoBehaviour 
{
	[SerializeField] private Transform exitPoint;
	[SerializeField] private Transform[] waypoints;
	[SerializeField] private float navigationUpdate;
	[SerializeField] private float healthPoints;
	[SerializeField] private int rewardAmount;

	private int target = 0;
	private Transform enemy;
	private float navigationTime = 0;
	private bool isDead = false;
	private Collider2D enemyCollider;
	private Animator anim;

	public bool IsDead
	{
		get
		{
			return isDead;
		}
	}

	// Use this for initialization
	void Start () 
	{
		enemy = GetComponent<Transform>();
		GameManager.Instance.RegisterEnemy(this);
		enemyCollider = GetComponent<Collider2D>();
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (waypoints != null && !isDead)
		{
			navigationTime += Time.deltaTime;
			if (navigationTime > navigationUpdate)
			{
				// [Updated from OnTriggerEnter]
				// Figure out how many waypoints are in script, and if target is less than that...
				if (target < waypoints.Length) 
				{
					// MoveTowards target
					enemy.position = Vector2.MoveTowards(enemy.position, waypoints[target].position, navigationTime);
				}
				else 
				{
					// No new target exists, MoveTowards exitPoint
					enemy.position = Vector2.MoveTowards(enemy.position, exitPoint.position, navigationTime);
				}
				navigationTime = 0;
			}
		}
	}

	// Triger enter for waypoints.
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Checkpoint")
		{
			// Increment int target for Update().
			target++;
		}
		else if (other.tag == "Finish")
		{
			// Add total enemies who escaped through all rounds. From GameManager.cs.
			GameManager.Instance.TotalEscaped++;
			// Add total enemies who escaped through a single round. From GameManager.cs.
			GameManager.Instance.RoundEscaped++;
			// Removes enemy from List<Enemy> register.
			GameManager.Instance.UnregisterEnemy(this);
			GameManager.Instance.IsWaveOver();
		}
		else if (other.tag == "Projectiles")
		{
			Projectile newProjectile = other.gameObject.GetComponent<Projectile>();
			projectileHit(newProjectile.AttackDmg);
			// Removes projectile gameObject.
			Destroy(other.gameObject);
		}
	}

	public void projectileHit(int attackDamage)
	{
		if ((healthPoints - attackDamage) > 0)
		{
			healthPoints -= attackDamage;
			// Hurt animation
			anim.Play("Hurt");
		}
		else
		{
			// Die animation
			anim.SetTrigger("didDie");
			Die();
			// Remove collider so that towers stop shooting them.
		}
	}

	public void Die()
	{
		isDead = true;
		enemyCollider.enabled = false;
		// If enemy dies, add 1 to integer totalKilled in GameManager.cs.
		GameManager.Instance.TotalKilled++;
		GameManager.Instance.AddMoney(rewardAmount);
		GameManager.Instance.IsWaveOver();
	}
}
