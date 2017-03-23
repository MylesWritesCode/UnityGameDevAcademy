using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour 
{
	void OnCollisionEnter(Collision collider)
	{
		Destroy(gameObject);
	}
}
