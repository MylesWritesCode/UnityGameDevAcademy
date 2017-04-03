using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;

public class CharacterControl : MonoBehaviour {

	[SerializeField] int moveSpeed;
	[SerializeField] int jumpHeight;

	private Rigidbody charRigidbody;
	private Animator anim;

	// Use this for initialization
	void Start () {
		charRigidbody = GetComponent<Rigidbody>();
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 moveChar = new Vector3( CrossPlatformInputManager.GetAxis("Horizontal"), 
																		0, 
																		CrossPlatformInputManager.GetAxis("Vertical") );
		transform.position += moveChar * Time.deltaTime * moveSpeed;

		if (charRigidbody.velocity.magnitude == 0) {
			anim.SetBool("isWalking", false);
		} else {
			anim.SetBool("isWalking", true);
		}
	}
}
