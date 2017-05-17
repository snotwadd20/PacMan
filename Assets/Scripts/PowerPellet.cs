using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPellet : MonoBehaviour 
{

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.GetComponent<PacmanMove>() != null)
		{
			coll.SendMessage("Powerup", SendMessageOptions.DontRequireReceiver);
			Destroy(gameObject);
		}//if
	}//

}//
