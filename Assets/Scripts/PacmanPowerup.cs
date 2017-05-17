using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanPowerup : MonoBehaviour 
{
	public GhostKill[] ghosts = null;

	void Awake()
	{
		ghosts = FindObjectsOfType<GhostKill>();
	}//Awake

	public void Powerup()
	{
		//print("POWER UP!");
		for (int i = 0; i < ghosts.Length; i++)
		{
			ghosts[i].MakeFrightened();
		}//for
	}//Powerup
}//
