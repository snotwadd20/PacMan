using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanMove : MonoBehaviour 
{
	public float speed = 0.4f;
	private Rigidbody2D rb2d = null;
	Vector2 dest = Vector2.zero;
	Animator anim = null;
	Collider2D coll = null;
	Vector2 lastDir = Vector2.zero;

	void Awake () 
	{
		rb2d = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		coll = GetComponent<Collider2D>();
	}//Awake

	void Start()
	{
		dest = transform.position;
		lastDir = Vector2.zero;
	}//Start

	void FixedUpdate()
	{
		//HAve we reached the destination?
		if ((Vector2)transform.position == dest)
		{
			//If we readched it, is there input?
			Vector2 inputDir = GetInputDir();
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
					lastDir = Vector2.zero;
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

	void FixedUpdate2 () 
	{
		rb2d.MovePosition(Vector2.MoveTowards(transform.position, dest, speed));

		if ((Vector2)transform.position == dest)
		{
			
			if (Input.GetKey(KeyCode.UpArrow) && IsValid(Vector2.up))
				dest = (Vector2)transform.position + Vector2.up;
			if (Input.GetKey(KeyCode.DownArrow) && IsValid(Vector2.down))
				dest = (Vector2)transform.position + Vector2.down;
			if (Input.GetKey(KeyCode.RightArrow) && IsValid(Vector2.right))
				dest = (Vector2)transform.position + Vector2.right;
			if (Input.GetKey(KeyCode.LeftArrow) && IsValid(Vector2.left))
				dest = (Vector2)transform.position + Vector2.left;
			
		}//if

		Vector2 dir = dest - (Vector2)transform.position;
		anim.SetFloat("DirX", dir.x);
		anim.SetFloat("DirY", dir.y);

	}//Update

	bool IsValid(Vector2 dir)
	{
		RaycastHit2D[] hits = Physics2D.LinecastAll(transform.position, (Vector2)transform.position + dir);

		bool isValid = true;

		for (int i = 0; i < hits.Length; i++)
		{
			if (!hits[i].collider.isTrigger && hits[i].collider != coll)
				isValid = false;
		}//for

		return isValid;
	}//IsValid
}//
