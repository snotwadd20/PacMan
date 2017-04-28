using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacDot : MonoBehaviour 
{
	public static int Count = 0;
	// Use this for initialization
	void Awake () 
	{
		
	}//Awake

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.name == "pacman")
		{
			//print("DOT GET");
			Count --;
			Destroy(gameObject);
		}//if
	}//OnTriggerEnter2D
	
	// Update is called once per frame
	void Update () 
	{
		
	}//Update
}//
