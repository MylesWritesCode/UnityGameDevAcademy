using UnityEngine;

public class Platform : MonoBehaviour 
{
	[SerializeField] private float objectSpeed = 4;
	[SerializeField] private float resetPosition = 51.0f;
	[SerializeField] private float startPosition = -53f;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	protected virtual void Update () 
	{
		if (!GameManager.instance.GameOver && GameManager.instance.PlayerActive)
		{
			// Set relative to Space.World so that it doesn't translate the object elsewhere besides along the X axis due to rotation.
			transform.Translate((objectSpeed * Time.deltaTime), 0, 0, Space.World);

			if (transform.localPosition.x >= resetPosition)
			{
				Vector3 newPos = new Vector3(startPosition, transform.position.y, transform.position.z);
				transform.position = newPos;
			}
		}
	}
}
