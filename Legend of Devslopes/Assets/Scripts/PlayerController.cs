﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerController : MonoBehaviour 
{
	[SerializeField] private float moveSpeed = 10f;
	[SerializeField] private LayerMask layerMask;

	private CharacterController characterController;
	private Vector3 currentLookTarget = Vector3.zero;
	private Animator anim;
	private BoxCollider[] swordColliders;
	private GameObject fireTrail;
	private ParticleSystem fireTrailParticles;

	void Awake()
	{

	}

	// Use this for initialization
	void Start () {
		// Look inside of the GameObject and get a Character Controller.
		characterController = GetComponent<CharacterController>();	
		// Look inside GameObject and get Animator.
		anim = GetComponent<Animator>();
		swordColliders = GetComponentsInChildren<BoxCollider>();
		// Fire trails for speed power up.
		fireTrail = GameObject.FindWithTag("Fire") as GameObject;
		fireTrail.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!GameManager.instance.GameOver)
		{
			// Get inputs from horizontal and vertical axis. 
			Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			characterController.SimpleMove(moveDirection * moveSpeed);

			if (moveDirection == Vector3.zero)
			{
				anim.SetBool("IsWalking", false);
			}
			else
			{
				anim.SetBool("IsWalking", true);
			}

			// If click...
			if (Input.GetMouseButtonDown(0))
			{
				// Basic attack.
				anim.Play("DoubleChop");
				// Some other code here to damage based on collider hit.
			}

			// If click...
			if (Input.GetMouseButtonDown(1))
			{
				// Spin attack animation.
				anim.Play("SpinAttack");
				// Some other code to damage based on collider hit.
			}
		}
	}

	void FixedUpdate()
	{
		if (!GameManager.instance.GameOver)
		{
			RaycastHit hit;
			// Create the ray from the camera to the mouse position.
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			// See the ray being cast. 
			Debug.DrawRay(ray.origin, ray.direction * 500, Color.blue);
			// Check raycast hits points, if there are other physics collider triggers, don't set them off. 
			if (Physics.Raycast(ray, out hit, 500, layerMask, QueryTriggerInteraction.Ignore))
			{
				// If camera isn't already looking at target...
				if (hit.point != currentLookTarget)
				{
					// Make the point that our mouse is looking the currentLookTarget.
					currentLookTarget = hit.point;
				}
				// Get the new target position.
				Vector3 targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
				// Get the angle of rotation from where it already is, to where it's supposed to be.
				Quaternion rotation = Quaternion.LookRotation(targetPosition - transform.position);
				// Initiate rotation between the angle it's currently rotated, the new rotation, by Time.deltaTime * 10f
				transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 10f);
			}
		}
	}

	public void BeginAttack()
	{
		foreach (var weapon in swordColliders)
		{
			weapon.enabled = true;
		}
	}

	public void EndAttack() {
		foreach (var weapon in swordColliders)
		{
			weapon.enabled = false;
		}
	}

	public void SpeedPowerUp() {
		StartCoroutine(fireTrailRoutine());
	}

	IEnumerator fireTrailRoutine() {
		fireTrail.SetActive(true);
		moveSpeed = 10f;
		yield return new WaitForSeconds(10f);
		moveSpeed = 6f;
		fireTrailParticles = fireTrail.GetComponent<ParticleSystem>();
		var em = fireTrailParticles.emission;
		em.enabled = false;
		yield return new WaitForSeconds(3f);
		em.enabled = true;
		fireTrail.SetActive(false);
	}
}
