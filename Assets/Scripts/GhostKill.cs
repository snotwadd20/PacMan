using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostKill : MonoBehaviour 
{
	private PacmanMove pacman = null;

	// Use this for initialization
	void Awake () 
	{
		pacman = FindObjectOfType<PacmanMove>();

	}//Awake

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject == pacman.gameObject)
		{
			pacman.SendMessage("Die", SendMessageOptions.DontRequireReceiver);
		}//if
	}//OnTriggerEnter2D
	
	// Update is called once per frame
	void Update () 
	{
		
	}//Update
}//
