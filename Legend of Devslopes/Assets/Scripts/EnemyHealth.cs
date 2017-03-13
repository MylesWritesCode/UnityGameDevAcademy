using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour 
{
	[SerializeField] private int startingHealth;
	[SerializeField] private float timeSinceLastHit = 0.5f;
	[SerializeField] private float disappearSpeed = 2f;

	private AudioSource audio;
	private float timer = 0f;
	private Animator anim;
	private NavMeshAgent nav;
	private bool isAlive;
	private Rigidbody rigidBody;
	private CapsuleCollider capsuleCollider;
	private bool disappearEnemy = false;
	private int currentHealth;

	public bool IsAlive
	{ 
		get { return isAlive; }
	}

	// Use this for initialization
	void Start () 
	{
		rigidBody = GetComponent<Rigidbody>();
		capsuleCollider = GetComponent<CapsuleCollider>();
		nav = GetComponent<NavMeshAgent>();
		anim = GetComponent<Animator>();
		audio = GetComponent<AudioSource>();
		isAlive = true;
		currentHealth = startingHealth;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
