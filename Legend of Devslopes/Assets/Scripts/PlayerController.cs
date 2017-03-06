using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
	[SerializeField] private float moveSpeed = 10f;

	private CharacterController characterController;

	// Use this for initialization
	void Start () 
	{
		// Look inside of the GameObject and get a Character Controller.
		characterController = GetComponent<CharacterController>();	
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Get inputs from horizontal and vertical axis. 
		Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		characterController.SimpleMove(moveDirection * moveSpeed);
	}
}
