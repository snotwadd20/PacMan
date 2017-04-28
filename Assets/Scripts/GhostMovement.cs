using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour 
{
							//red  //pink	  //blue	//orange
	public enum GhostType {Chaser, Ambusher, Whimsical, Tricky}
	public GhostType type = GhostType.Chaser;
	public Vector2 lastDir = Vector2.zero;

	public bool test = false;
	Vector2 dest = Vector2.zero;

	private PacmanMove pacman = null;
	float speed = 0;

	public static GhostMovement chaseGhost = null;
	// Use this for initialization
	void Awake () 
	{
		pacman = FindObjectOfType<PacmanMove>();
		speed = pacman.speed;

		if (type == GhostType.Chaser) //store this so blue ghost can access it
			chaseGhost = this;
	}//Awake

	void Start()
	{
		dest = transform.position;
		lastDir = Vector2.zero;
	}//Start

	Vector2 GetTargetPos()
	{
		//TODO, chase mode VS scatter mode
		Vector2 thePos = Vector2.zero;

		if (type == GhostType.Chaser)
		{
			thePos = (Vector2)pacman.transform.position;
		}//if
		else if (type == GhostType.Ambusher)
		{
			thePos = pacman.GetAmbushPos(4);
		}//else if
		else if (type == GhostType.Whimsical)
		{
			Vector2 redToPacAmbush = (pacman.GetAmbushPos(2) - chaseGhost.dest) * 2;
			thePos = chaseGhost.dest + redToPacAmbush;
		}//else if
		else if (type == GhostType.Tricky)
		{
			if (Vector2.Distance(dest, pacman.dest) >= 8)
				thePos = (Vector2)pacman.transform.position;
			else
				thePos = new Vector2(0,-2);

		}//else if

		return thePos;
	}//GetTargetPos

	void OnDrawGizmos()
	{
		if (pacman == null)
			return;

		float sizeMult = 1.0f;

		if (type == GhostType.Chaser)
		{
			Gizmos.color = Color.red;
			sizeMult = 1.5f;
		}
		if (type == GhostType.Ambusher)
			Gizmos.color = new Color(1.0f, 0.41f, 0.70f);
		if (type == GhostType.Whimsical)
			Gizmos.color = Color.cyan;
		if (type == GhostType.Tricky)
			Gizmos.color = new Color(1.0f, 0.64f, 0);
		
		Gizmos.DrawWireSphere(GetTargetPos(), 0.5f * sizeMult);
	}//

	void FixedUpdate()
	{
		transform.position = Vector2.MoveTowards(transform.position, dest, speed);

		if ((Vector2)transform.position == dest)
		{
			dest = ChooseNewDestination();
			lastDir = (dest - (Vector2)transform.position).normalized;
		}//if
	}//
	
	// Update is called once per frame
	void Update () 
	{
		if (test)
		{
			List<Vector2> options = GetValidDestinations((int)transform.position.x, (int)transform.position.y);
			Debug.LogFormat("CurPos: {0},{1}", (int)transform.position.x, (int)transform.position.y);
			for (int i = 0; i < options.Count; i++)
			{
				Debug.LogFormat("Option{0}: {1},{2}", i, options[i].x, options[i].y);
			}//

			Vector2 targetPos = GetTargetPos();
			Vector2 chosenPos = ChooseNewDestination();
			Debug.LogFormat("Closest to target (@{0},{1}): {2},{3}", targetPos.x, targetPos.y, chosenPos.x, chosenPos.y );
			test = false;
		}//
	}//Update



	List<Vector2> GetValidDestinations(int curX, int curY)
	{
		List<Vector2> validDests = new List<Vector2>();
		//Look up, down, left, right (can't go backwards)

		//UP
		if(lastDir != Vector2.down && PathNodes.self.InBounds(curX, curY +1) && PathNodes.self.nodeSpots[curX, curY+1])
		{
			validDests.Add(new Vector2(curX, curY+1));
		}//if

		//DOWN
		if(lastDir != Vector2.up && PathNodes.self.InBounds(curX, curY -1) && PathNodes.self.nodeSpots[curX, curY-1])
		{
			validDests.Add(new Vector2(curX, curY-1));
		}//if

		//LEFT
		if(lastDir != Vector2.right && PathNodes.self.InBounds(curX-1, curY) && PathNodes.self.nodeSpots[curX-1, curY])
		{
			validDests.Add(new Vector2(curX-1, curY));
		}//if

		//RIGHT
		if(lastDir != Vector2.left && PathNodes.self.InBounds(curX+1, curY) && PathNodes.self.nodeSpots[curX+1, curY])
		{
			validDests.Add(new Vector2(curX+1, curY));
		}//if

		return validDests;
	}//GetValidDestinations

	Vector2 ChooseNewDestination()
	{
		List<Vector2> options = GetValidDestinations((int)transform.position.x, (int)transform.position.y);

		//From the options, choose the one closest to the target position as the crow flies
		return GetClosest(options);
	}//ChooseNewDestination

	Vector2 GetClosest(List<Vector2> options)
	{
		Vector2 choice = new Vector2(int.MaxValue, int.MaxValue);//Furthest point possible to start
		Vector2 targetPos = GetTargetPos();

		for (int i = 0; i < options.Count; i++)
		{
			//Debug.Log("DIST:" + i + " = " +Vector2.Distance(options[i], targetPos));
			if (Vector2.Distance(options[i], targetPos) < Vector2.Distance(choice, targetPos))
				choice = options[i];
		}//for

		return choice;
	}//GetClosest

}//GhostMovement
