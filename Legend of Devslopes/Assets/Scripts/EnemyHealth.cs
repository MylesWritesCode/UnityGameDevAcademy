using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour 
{
	[SerializeField] private int startingHealth;
	[SerializeField] private float timeSinceLastHit = 0.5f;
	[SerializeField] private float disappearSpeed = 2f;

	private AudioSource hitAudio;
	private float timer = 0f;
	private Animator anim;
	private NavMeshAgent nav;
	private bool isAlive;
	private Rigidbody rigidBody;
	private CapsuleCollider capsuleCollider;
	private bool disappearEnemy = false;
	private int currentHealth;
	private ParticleSystem orcBlood;

	public bool IsAlive
	{ 
		get { return isAlive; }
	}

	// Use this for initialization
	void Start () 
	{
		// Add this enemy to the list on spawn.
		GameManager.instance.RegisterEnemy(this);
		rigidBody = GetComponent<Rigidbody>();
		capsuleCollider = GetComponent<CapsuleCollider>();
		nav = GetComponent<NavMeshAgent>();
		anim = GetComponent<Animator>();
		hitAudio = GetComponent<AudioSource>();
		isAlive = true;
		currentHealth = startingHealth;
		orcBlood = GetComponentInChildren<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		timer += Time.deltaTime;

		if (disappearEnemy)
		{
			transform.Translate(-Vector3.up * disappearSpeed * Time.deltaTime);
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (timer >= timeSinceLastHit && !GameManager.instance.GameOver)
		{
			if (other.tag == "PlayerWeapon")
			{
				orcBlood.Play();
				takeHit();
				timer = 0f;
				hitAudio.PlayOneShot(hitAudio.clip);
			}
		}
	}

	void takeHit()
	{
		if (currentHealth > 0)
		{
			anim.Play("Hurt");
			currentHealth -= 10;
		}

		if (currentHealth <= 0)
		{
			isAlive = false;
			killEnemy();
		}
	}

	void killEnemy()
	{
		// When this enemy is killed, register on KilledEnemy list in GameManager.
		GameManager.instance.KilledEnemy(this);
		capsuleCollider.enabled = false;
		nav.enabled = false;
		anim.SetTrigger ("EnemyDie");
		rigidBody.isKinematic = true;
		StartCoroutine(removeEnemy());
	}

	IEnumerator removeEnemy()
	{
		yield return new WaitForSeconds(4f);
		disappearEnemy = true;
		yield return new WaitForSeconds(2f);
		Destroy(gameObject);
	}
}
