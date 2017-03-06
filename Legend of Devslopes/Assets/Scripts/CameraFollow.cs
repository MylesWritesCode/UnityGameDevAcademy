using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CameraFollow : MonoBehaviour 
{
	[SerializeField] Transform target;
	// Smooth camera movement.
	[SerializeField] float smoothing = 5f;
	Vector3 offset;

	void Awake()
	{
		Assert.IsNotNull(target);
	}

	// Use this for initialization
	void Start () 
	{
		offset = transform.position - target.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 targetCameraPosition = target.position + offset;
		// Linear interpolation between two vectors.
		transform.position = Vector3.Lerp(transform.position, targetCameraPosition, smoothing * Time.deltaTime);
	}
}
