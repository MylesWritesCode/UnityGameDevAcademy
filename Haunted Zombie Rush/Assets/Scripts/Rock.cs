using System.Collections;
using UnityEngine;

public class Rock : Platform 
{
	[SerializeField] Vector3 topPosition;
	[SerializeField] Vector3 bottomPosition;
	[SerializeField] float rockSpeed = 10f;
	[SerializeField] float rotationSpeed = 100f;

	// Use this for initialization
	void Start () 
	{
		if (transform.localPosition.y >= 7.5)
		{
			StartCoroutine(Move(bottomPosition));
		}
		else
		{
			StartCoroutine(Move(topPosition));
		}
	}

	protected override void Update()
	{
		if (GameManager.instance.PlayerActive)
		{
			base.Update();
			// Putting this in update because it's not important how fast these guys spin.
			transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
		}
	}

	IEnumerator Move(Vector3 target)
	{
		while (Mathf.Abs((target - transform.localPosition).y) > 0.20f)
		{
			Vector3 direction = target.y == topPosition.y ? Vector3.up : Vector3.down;
			transform.localPosition += direction * (rockSpeed * Time.deltaTime);

			yield return null;
		}
		yield return new WaitForSeconds(0.5f);

		Vector3 newTarget = target.y == topPosition.y ? bottomPosition : topPosition;
		StartCoroutine(Move(newTarget));
	}
}
