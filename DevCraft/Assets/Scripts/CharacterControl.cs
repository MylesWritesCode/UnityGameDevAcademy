using UnityEngine;

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
		
	}
}
