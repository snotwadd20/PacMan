using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceDots : MonoBehaviour 
{
	public PacDot dotPrefab = null;

	void Start () 
	{
		for (int y = 0; y < PathNodes.self.height; y++)
		{
			for (int x = 0; x < PathNodes.self.width; x++)
			{
				if (PathNodes.self.nodeSpots[x, y])
				{
					PacDot pacDot = Instantiate<PacDot>(dotPrefab);
					pacDot.transform.position = new Vector3(x, y);
					pacDot.gameObject.SetActive(true);
				}//if
			}//for
		}//for
	}//Awake
	
	void Update () 
	{
		
	}//Update
}//
