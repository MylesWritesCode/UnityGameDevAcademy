using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : Platform 
{
	[SerializeField] private Vector3 topPosition = new Vector3(0f, 10f, 0f);
	[SerializeField] private Vector3 bottomPosition = new Vector3(0f, 0f, 0f);
	[SerializeField] private float rotationSpeed = 100f;
	[SerializeField] private float ySpeed = 2.5f;

	private GameObject coin;
	// Use this for initialization
	void Start () 
	{
		if (transform.localPosition.y <= 5)
		{
			StartCoroutine(Move(topPosition));
		}
		else
		{
			StartCoroutine(Move(bottomPosition));
		}
	}
	
	// Update is called once per frame
	protected override void Update () 
	{
		base.Update();
		transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));

		// Instantiate coins at random heights.
	}

	IEnumerator Move(Vector3 target)
	{
		while (Mathf.Abs((target - transform.localPosition).y) > 0.20f)
		{
			Vector3 direction = target.y == topPosition.y ? Vector3.up : Vector3.down;
			transform.localPosition += direction * (ySpeed * Time.deltaTime);

			yield return null;
		}
		yield return new WaitForSeconds(0.5f);

		Vector3 newTarget = target.y == topPosition.y ? bottomPosition : topPosition;
		StartCoroutine(Move(newTarget));
	}
}
