using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class DirectionRaycasting2DCollider : MonoBehaviour
{
	
	//-------------------------------
	//          fields
	//-------------------------------
	public bool collisionUp;
	public bool collisionDown;
	public bool collisionLeft;
	public bool collisionRight;
	
	//show rays in debug
	public bool showRays = false;
	
	//ray cast fields
	public float rayDistance;
	
	//the ray that hit something
	public RaycastHit2D TileHit;
	
	//raycast related
	public List<GameObject> rayPoints;
	public List<Ray2D> rays;
	
	public List<Ray2D> raysUp;
	public List<Ray2D> raysDown;
	public List<Ray2D> raysLeft;
	public List<Ray2D> raysRight;
	
	//-------------------------------
	//          Unity
	//-------------------------------
	void Start()
	{
		//acquire the ray point origins
		rayPoints = new List<GameObject>();
		getRays();
	}
	
	
	void Update()
	{
		//check collision on all sides
		checkCollision();
		
		//debug
		if (showRays)
			drawRaycast();
	}
	
	//-------------------------------
	//          Functions
	//-------------------------------
	
	void getRays()
	{
		//get the object named Raycasting
		List<GameObject> children = gameObject.GetChildren();
		
		//get the children inside Raycasting
		List<GameObject> children2 = new List<GameObject>();
		
		//check inside raycasting object for the children (children are inside the raycasting folder)
		for (int i = 0; i < children.Count; i++)
		{
			if (children[i].name == "RayCasting")
				children2 = children[i].GetChildren();
		}
		
		for (int i = 0; i < children2.Count; i++)
		{
			//Debug.Log(i + " " + children2[i].gameObject.name);
			rayPoints.Add(children2[i]);
		}
	}
	
	
	void checkCollision()
	{
		//-------------------------------
		//          init rays list
		//-------------------------------
		List<Ray2D> raysUp = new List<Ray2D>();
		List<Ray2D> raysDown = new List<Ray2D>();
		List<Ray2D> raysLeft = new List<Ray2D>();
		List<Ray2D> raysRight = new List<Ray2D>();
		
		TileHit = new RaycastHit2D();
		
		//assign rays to list
		for (int i = 0; i < rayPoints.Count; i++)
		{
			
			//up
			if (rayPoints[i].gameObject.name == "up")
			{
				raysUp.Add(new Ray2D(new Vector2(rayPoints[i].gameObject.transform.position.x, rayPoints[i].gameObject.transform.position.y), Vector2.up));
			}
			
			//down
			if (rayPoints[i].gameObject.name == "down")
			{
				raysDown.Add(new Ray2D(new Vector2(rayPoints[i].gameObject.transform.position.x, rayPoints[i].gameObject.transform.position.y), -Vector2.up));
			}
			
			
			//left
			if (rayPoints[i].gameObject.name == "left")
			{
				raysLeft.Add(new Ray2D(new Vector2(rayPoints[i].gameObject.transform.position.x, rayPoints[i].gameObject.transform.position.y), -Vector2.right));
			}
			
			//right
			if (rayPoints[i].gameObject.name == "right")
			{
				raysRight.Add(new Ray2D(new Vector2(rayPoints[i].gameObject.transform.position.x, rayPoints[i].gameObject.transform.position.y), Vector2.right));
			}
		}
		
		//-------------------------------
		//          check collisions
		//-------------------------------
		collisionDown = checkCollision(raysDown);
		collisionUp = checkCollision(raysUp);
		collisionLeft = checkCollision(raysLeft);
		collisionRight = checkCollision(raysRight);
		
	}
	
	
	//-------------------------------
	//          Functions Debug
	//-------------------------------
	void drawRaycast()
	{
		//draw all rays in list
		for (int i = 0; i < rayPoints.Count; i++)
		{
			
			//draw up
			if (rayPoints[i].gameObject.name == "up")
				Debug.DrawLine(rayPoints[i].gameObject.transform.position, new Vector3(rayPoints[i].gameObject.transform.position.x, rayPoints[i].gameObject.transform.position.y + rayDistance, rayPoints[i].gameObject.transform.position.z), Color.red);
			
			//draw down
			if (rayPoints[i].gameObject.name == "down")
				Debug.DrawLine(rayPoints[i].gameObject.transform.position, new Vector3(rayPoints[i].gameObject.transform.position.x, rayPoints[i].gameObject.transform.position.y - rayDistance, rayPoints[i].gameObject.transform.position.z), Color.red);
			
			//draw left
			if (rayPoints[i].gameObject.name == "left")
				Debug.DrawLine(rayPoints[i].gameObject.transform.position, new Vector3(rayPoints[i].gameObject.transform.position.x - rayDistance, rayPoints[i].gameObject.transform.position.y, rayPoints[i].gameObject.transform.position.z), Color.red);
			
			//draw right
			if (rayPoints[i].gameObject.name == "right")
				Debug.DrawLine(rayPoints[i].gameObject.transform.position, new Vector3(rayPoints[i].gameObject.transform.position.x + rayDistance, rayPoints[i].gameObject.transform.position.y, rayPoints[i].gameObject.transform.position.z), Color.red);
			
		}
		
	}
	
	bool checkCollision(List<Ray2D> rayList)
	{
		for (int i = 0; i < rayList.Count; i++)
		{
			//check all rays
			TileHit = Physics2D.Raycast(rayList[i].origin, rayList[i].direction,rayDistance + .001f);

			if(TileHit != null && TileHit.collider != null)
			{
				return true;
			}
		}
		return false;
	} 
	
}