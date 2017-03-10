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

	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator>();
		player = GameManager.instance.Player;
		weaponColliders = GetComponentsInChildren<BoxCollider>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
