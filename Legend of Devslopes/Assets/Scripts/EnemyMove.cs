using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour 
{
	[SerializeField] Transform player;
	private NavMeshAgent nav;
	private Animator anim;
	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator>();
		nav = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
