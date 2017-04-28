using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacDot : MonoBehaviour 
{

	// Use this for initialization
	void Awake () 
	{
		
	}//Awake

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.name == "pacman")
			Destroy(gameObject);
	}//OnTriggerEnter2D
	
	// Update is called once per frame
	void Update () 
	{
		
	}//Update
}//
