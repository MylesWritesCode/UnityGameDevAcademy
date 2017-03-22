using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerAttack : MonoBehaviour 
{
	[SerializeField] private float range = 3f;
	[SerializeField] private float attackSpeed = 1f;
	[SerializeField] Transform fireLocation;

	private Animator anim;
	private GameObject player;
	private bool playerInRange;
	private EnemyHealth enemyHealth;
	private GameObject arrow;

	// Use this for initialization
	void Start () 
	{
		arrow = GameManager.instance.Arrow;
		anim = GetComponent<Animator>();
		player = GameManager.instance.Player;
		enemyHealth = GetComponent<EnemyHealth>();
		StartCoroutine(attack());
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Vector3.Distance(transform.position, player.transform.position) < range && enemyHealth.IsAlive)
		{
			playerInRange = true;
			anim.SetBool("PlayerInRange", true);
			rotateTowards(player.transform);
		}
		else
		{
			playerInRange = false;
			anim.SetBool("PlayerInRange", false);
		}
	}

	// Coroutine is a function that is able to work in code blocks.
	// We can say "wait a few seconds, then move on" and stuff like that.
	IEnumerator attack()
	{
		if (playerInRange && !GameManager.instance.GameOver)
		{
			anim.Play("Attack");
			yield return new WaitForSeconds(attackSpeed);
		}
		yield return null;
		// Recursive.
		StartCoroutine(attack());
	}

	private void rotateTowards(Transform player) 
	{
		// Create a direction towards player.
		Vector3 direction = (player.position - transform.position).normalized;
		// Find rotation towards player.
		Quaternion lookRotation = Quaternion.LookRotation(direction);
		// Perform actual rotation.
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
	}

	public void FireArrow()
	{
		// Load arrow into game.
		GameObject newArrow = Instantiate(arrow) as GameObject;

	}
}
