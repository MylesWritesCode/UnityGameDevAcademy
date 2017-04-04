using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class CharacterControl : MonoBehaviour {

	[SerializeField] int moveSpeed;
	[SerializeField] int jumpHeight;

	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 moveChar = new Vector3( CrossPlatformInputManager.GetAxis("Horizontal"), 
																		0, 
																		CrossPlatformInputManager.GetAxis("Vertical") );
		
		if (moveChar != Vector3.zero) {
			anim.SetBool("isWalking", true);
			Quaternion targetRotation = Quaternion.LookRotation(moveChar, Vector3.up);
			transform.rotation = targetRotation;
		} else {
			anim.SetBool("isWalking", false);
		}

		transform.position += moveChar * Time.deltaTime * moveSpeed;

		if (GameManager.Instance.IsJumping) {
			anim.SetTrigger("Jump");
			transform.Translate(Vector3.up * jumpHeight * Time.deltaTime, Space.World);
			GameManager.Instance.IsJumping = false;
		}

		if (GameManager.Instance.IsPunching) {
			anim.SetTrigger("Punch");
			ModifyTerrain.Instance.DestroyBlock(10f, (byte) TextureType.air.GetHashCode());
			GameManager.Instance.IsPunching = false;
		}

		if (GameManager.Instance.IsBuilding) {
			anim.SetTrigger("Punch");
			ModifyTerrain.Instance.AddBlock(10f, (byte) TextureType.rock.GetHashCode());
			GameManager.Instance.IsBuilding = false;
		}
	}
}
