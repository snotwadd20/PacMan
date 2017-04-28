using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanMove : MonoBehaviour 
{
	public float speed = 0.4f;
	public bool emulatePacmanBugs = true;

	//[HideInInspector]
	public Vector2 dest = Vector2.zero;

	private Vector2 lastDir = Vector2.right;
	private Animator anim = null;
	private Rigidbody2D rb2d = null;
	private List<Vector2> inputDirs = new List<Vector2>();
	private const int MAX_INPUTS_SAVED = 10;
	void Awake () 
	{
		rb2d = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}//Awake

	void Start()
	{
		dest = transform.position;
		lastDir = Vector2.right;
	}//Start

	void Update()
	{
		inputDirs.Add(GetInputDir());

		if (inputDirs.Count > MAX_INPUTS_SAVED)
			inputDirs.RemoveAt(0);
	}//Update

	Vector2 FirstSavedInputDir()
	{
		Vector2 dir = Vector2.zero;

		while (inputDirs.Count > 0 && dir == Vector2.zero)
		{
			dir = inputDirs[0];
			inputDirs.RemoveAt(0);
		}//while
		return dir;
	}//

	void FixedUpdate()
	{
		//HAve we reached the destination?
		if ((Vector2)transform.position == dest)
		{
			Vector2 inputDir = FirstSavedInputDir();

			//If we readched it, is there input?
			if(inputDir == Vector2.zero)
			{
				//If not, can we keep movin in the same direction?
				if(IsValid(lastDir))
				{
					//If so, set the new destination
					dest += lastDir;
				}//if

			}//if
			else if (IsValid(inputDir))	
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

	void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere(dest, 0.5f);
	}//

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
		//int x = Mathf.RoundToInt(transform.position.x);
		//int y = Mathf.RoundToInt(transform.position.y);
		//Vector2 pos = new Vector2(x, y);


		int x = (int)dest.x + (int)dir.x;
		int y = (int) dest.y + (int)dir.y;
		//RaycastHit2D[] hits = Physics2D.LinecastAll(pos, (Vector2)transform.position + dir);

		//bool isValid = true;

		//for (int i = 0; i < hits.Length; i++)
		//{
			//if (!hits[i].collider.isTrigger && hits[i].collider != coll)
				//isValid = false;
		//}//for

		return PathNodes.self.InBounds(x, y) && PathNodes.self.nodeSpots[x, y];
	}//IsValid
}//
