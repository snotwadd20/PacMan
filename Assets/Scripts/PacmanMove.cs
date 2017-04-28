using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanMove : MonoBehaviour 
{
	public float speed = 0.4f;
	private Rigidbody2D rb2d = null;
	[HideInInspector]
	public Vector2 dest = Vector2.zero;
	Animator anim = null;
	//Collider2D coll = null;
	Vector2 lastDir = Vector2.right;
	public bool emulatePacmanBugs = true;
	void Awake () 
	{
		rb2d = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		//coll = GetComponent<Collider2D>();
	}//Awake

	void Start()
	{
		dest = transform.position;
		lastDir = Vector2.right;
	}//Start

	Vector2 inputDir = Vector2.zero;
	void Update()
	{
		inputDir = GetInputDir();
	}//

	void FixedUpdate()
	{
		//HAve we reached the destination?
		if ((Vector2)transform.position == dest)
		{
			//If we readched it, is there input?
			if(inputDir == Vector2.zero)
			{
				//If not, can we keep movin in the same direction?
				if(IsValid(lastDir))
				{
					//If so, set the new destination
					dest += lastDir;
				}//if
				else
				{
					//If not, stop pac-man
					dest = transform.position;
					//lastDir = Vector2.zero;
				}//else
			}//if
			else	
			{
				//If so, use the input
				dest += inputDir;
				lastDir = inputDir;
			}//else

		}//if

		rb2d.MovePosition(Vector2.MoveTowards(transform.position, dest, speed));

		anim.SetFloat("DirX", lastDir.x);
		anim.SetFloat("DirY", lastDir.y);

	}//FixedUPdate

	public Vector2 GetAmbushPos(int offset)
	{
		//Get pos 4 tiles in front of pacman
		Vector2 ambushOffset = Vector2.zero;
		if (lastDir == Vector2.up)
		{
			if (emulatePacmanBugs) //Emulate the bug in the original pacman
					ambushOffset.x -= offset;
				
			ambushOffset.y += offset;
		}
		else if (lastDir == Vector2.down)
		{
			ambushOffset.y -= offset;
		}//
		else if (lastDir == Vector2.left)
		{
			ambushOffset.x -= offset;
		}//
		else if (lastDir == Vector2.right)
		{
			ambushOffset.x += offset;
		
		}//else if

		Vector2 ambushPos = dest + ambushOffset;
		return ambushPos;
	}//

	Vector2 GetInputDir()
	{
		if (Input.GetKey(KeyCode.UpArrow) && IsValid(Vector2.up) && lastDir != Vector2.up)
			return Vector2.up;
		
		if (Input.GetKey(KeyCode.DownArrow) && IsValid(Vector2.down) && lastDir != Vector2.down)
			return Vector2.down;
		
		if (Input.GetKey(KeyCode.RightArrow) && IsValid(Vector2.right) && lastDir != Vector2.right)
			return Vector2.right;
		
		if (Input.GetKey(KeyCode.LeftArrow) && IsValid(Vector2.left) && lastDir != Vector2.left)
			return Vector2.left;

		return Vector2.zero;
	}//GetInputDir

	bool IsValid(Vector2 dir)
	{
		Vector2 pos = new Vector2((int)transform.position.x, (int)transform.position.y);
		Vector2 next = pos + dir;
		//RaycastHit2D[] hits = Physics2D.LinecastAll(pos, (Vector2)transform.position + dir);

		//bool isValid = true;

		//for (int i = 0; i < hits.Length; i++)
		//{
			//if (!hits[i].collider.isTrigger && hits[i].collider != coll)
				//isValid = false;
		//}//for

		return PathNodes.self.InBounds((int)next.x, (int)next.y) && PathNodes.self.nodeSpots[(int)next.x, (int)next.y];
	}//IsValid
}//
