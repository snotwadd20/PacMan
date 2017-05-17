using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNodes : MonoBehaviour 
{
	public static PathNodes self = null;

	private SpriteRenderer spriteRenderer = null;

	public bool[,] nodeSpots = null;

	[HideInInspector]
	public int width = 0;

	[HideInInspector]
	public int height = 0;

	// Use this for initialization
	void Awake () 
	{
		self = this;

		spriteRenderer = GetComponent<SpriteRenderer>();
		Bounds bounds = spriteRenderer.bounds;

		width = (int)bounds.extents.x * 2;
		height = (int)bounds.extents.y * 2;

		nodeSpots = new bool[width, height];

		for (int y = 0; y < height; y++)
		{
			for (int x = 0; x < width; x++)
			{
				Collider2D coll = Physics2D.OverlapPoint(new Vector2(x, y));

				if (coll != null && coll.gameObject.layer == LayerMask.NameToLayer("Walls"))
					nodeSpots[x, y] = false;
				else
					nodeSpots[x,y] = true;
			}//for
		}//for

		//Debug.LogFormat("W:{0} , H:{1}", width, height);
	}//Awake

	public bool InBounds(int x, int y)
	{
		return (x >= 0 && x < width && y >= 0 && y < height);
	}//InBounds

	void Start()
	{
		
	}//Start

	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;

		if (nodeSpots == null)
		{
			Bounds bounds = GetComponent<SpriteRenderer>().bounds;

			for (int y = 1; y < bounds.extents.y * 2; y++)
			{
				for (int x = 1; x < bounds.extents.x * 2; x++)
				{
					Collider2D coll = Physics2D.OverlapPoint(new Vector2(x, y));

					if(!(coll != null && coll.gameObject.layer ==  LayerMask.NameToLayer("Walls")))
						Gizmos.DrawWireSphere(new Vector3(x, y), 0.25f);
				}//for
			}//for
		}//if
		else
		{
			Bounds bounds = spriteRenderer.bounds;

			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					if(nodeSpots[x,y])
						Gizmos.DrawWireSphere(new Vector3(x, y), 0.25f);
				}//for
			}//for
		}//else
	}//OnDrawGizmos
	
	// Update is called once per frame
	void Update () 
	{
		
	}//Update

}//
