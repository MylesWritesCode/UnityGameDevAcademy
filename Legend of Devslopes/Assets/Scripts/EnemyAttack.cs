using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour 
{
	[SerializeField] private float range = 3f;
	[SerializeField] private float attackSpeed = 1f;

	private Animator anim;
	private GameObject player;
	private bool playerInRange;
	private BoxCollider[] weaponColliders;
	private EnemyHealth enemyHealth;

	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator>();
		player = GameManager.instance.Player;
		weaponColliders = GetComponentsInChildren<BoxCollider>();
		enemyHealth = GetComponent<EnemyHealth>();
		StartCoroutine(attack());
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Vector3.Distance(transform.position, player.transform.position) < range && enemyHealth.IsAlive)
		{
			playerInRange = true;
			// Debug.Log("Player In Range");
		}
		else
		{
			playerInRange = false;
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

	public void EnemyBeginAttack()
	{
		foreach (var weapon in weaponColliders)
		{
			weapon.enabled = true;
		}
	}

	public void EnemyEndAttack()
	{
		foreach (var weapon in weaponColliders)
		{
			weapon.enabled = false;
		}
	}
}
