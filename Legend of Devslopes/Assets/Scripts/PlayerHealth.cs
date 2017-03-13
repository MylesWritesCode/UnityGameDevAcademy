using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class PlayerHealth : MonoBehaviour 
{
	[SerializeField] int startingHealth = 100;
	[SerializeField] float timeSinceLastHit = 2f;
	[SerializeField] Slider healthSlider;

	private float timer = 0f;
	private CharacterController characterController;
	private Animator anim;
	private int currentHealth;
	private AudioSource hitAudio;

	void Awake()
	{
		Assert.IsNotNull(healthSlider);
	}

	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator>();
		characterController = GetComponent<CharacterController>();
		currentHealth = startingHealth;
		hitAudio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		timer += Time.deltaTime;
	}

	void OnTriggerEnter (Collider other)
	{
		if (timer >= timeSinceLastHit && !GameManager.instance.GameOver)
		{
			if (other.tag == "Weapon")
			{
				takeHit();
				timer = 0;
			}
		}
	}

	void takeHit()
	{
		if (currentHealth > 0)
		{
			GameManager.instance.PlayerHit(currentHealth);
			anim.Play("Hurt");
			currentHealth -= 10;
			healthSlider.value = currentHealth;
			hitAudio.PlayOneShot(hitAudio.clip);
		}

		if (currentHealth <= 0)
		{
			killPlayer();
		}
	}

	void killPlayer()
	{
		GameManager.instance.PlayerHit(currentHealth);
		anim.SetTrigger("HeroDie");
		characterController.enabled = false;
		hitAudio.PlayOneShot(hitAudio.clip);
	}
}
