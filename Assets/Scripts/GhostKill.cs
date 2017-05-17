using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostKill : MonoBehaviour 
{
	public float vulnerableLength = 5.0f;	
	private PacmanMove pacman = null;
	private Animator animator = null;
	private GhostMovement movement = null;

	private bool isVulnerable = false;
	// Use this for initialization
	void Awake () 
	{
		pacman = FindObjectOfType<PacmanMove>();
		animator = GetComponent<Animator>();
		movement = GetComponent<GhostMovement>();
	}//Awake

	public void MakeVulnerable()
	{
		isVulnerable = true;
		animator.SetBool("isVulnerable", true);
		movement.InvertDirection();
		movement.Scatter(true);
		Invoke("MakeNormal", vulnerableLength);
	}//MakeVulnerable

	public void MakeNormal()
	{
		isVulnerable = false;
		animator.SetBool("isVulnerable", false);
		movement.InvertDirection();
		movement.Scatter(false);
	}//

	void KillGhost()
	{
		MakeNormal();
		movement.ReturnToStart();
	}//KillGhost



	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject == pacman.gameObject)
		{
			if (isVulnerable)
				KillGhost();
			else
				pacman.SendMessage("Die", SendMessageOptions.DontRequireReceiver);
		}//if
	}//OnTriggerEnter2D
	
	// Update is called once per frame
	void Update () 
	{
		
	}//Update
}//
