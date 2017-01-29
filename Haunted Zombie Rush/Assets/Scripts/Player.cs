using UnityEngine;
using UnityEngine.Assertions;

public class Player : MonoBehaviour 
{
	[SerializeField] private float jumpForce = 100f;
	[SerializeField] private AudioClip sfxJump;
	[SerializeField] private AudioClip sfxDeath;
	// [SerializeField]private Vector3 startPosition;

	private Animator anim;
	private Rigidbody rigidBody;
	private bool jump = false;
	private AudioSource audioSource;
	

	void Awake()
	{
		Assert.IsNotNull(sfxJump);
		Assert.IsNotNull(sfxDeath);
	}

	// Use this for initialization
	void Start () 
	{
		// Move the player back into the starting position. I think I need to do this in the GameManager, but it looks like it'll work better here. 
		// transform.position = new Vector3(0.5f, 6f, 12f);
		// On start: looks for animator on object this script is attached to.
		anim = GetComponent<Animator>();
		// On start: looks for rigidbody on object this script is attached to.
		rigidBody = GetComponent<Rigidbody>();
		// On start: looks for audio source on object this script is attached to.
		audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!GameManager.instance.GameOver && GameManager.instance.GameStarted)
		{
			if (Input.GetMouseButtonDown(0))
			{
				// Talk to GameManager and tell it that the player started the game.
				GameManager.instance.PlayerStartedGame();
				// Call jump animation.
				anim.Play("Jump");
				// Play sfxJump.
				audioSource.PlayOneShot(sfxJump);
				// Set gravity on rb for player to true. It's default false so the player floats before gameplay.
				rigidBody.useGravity = true;
				// When the button is clicked, set jump to true to call the method below.
				jump = true;
			}
		}
	}

	void FixedUpdate()
	{
		if (jump == true)
		{
			// Immediately set jump back to false. This is so the player can press jump again.
			jump = false;
			// Set the velocity to 0m/s so that the player can jump the same height every time.
			rigidBody.velocity = new Vector2(0,0);
			// Add jump force to player object.
			rigidBody.AddForce(new Vector2(0, jumpForce), ForceMode.Impulse);
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Obstacle")
		{
			rigidBody.AddForce(new Vector2(-50, 20), ForceMode.Impulse);
			rigidBody.detectCollisions = false;
			audioSource.PlayOneShot(sfxDeath);
			GameManager.instance.PlayerCollided();
		}
	}
}
