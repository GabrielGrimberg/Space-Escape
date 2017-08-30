using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeySpawn : MonoBehaviour 
{
	public GameObject enemyPrefab;
	public float width = 10f;
	public float height = 5f;
	public float speed = 5f;

	private bool isMovingRight = true;
	private float xMax;
	private float xMin;

	// Use this for initialization
	void Start () 
	{
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0,0, distanceToCamera) );
		Vector3 rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1,0, distanceToCamera) );

		xMax = rightEdge.x;
		xMin = leftEdge.x;

		//Loop over every child object (Enemy).
		foreach(Transform child in transform)
		{
			GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
		}
		
	}

	public void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(transform.position, new Vector3(width, height) );
	}

	// Update is called once per frame
	void Update () 
	{
		if(isMovingRight)
		{
			transform.position += Vector3.right * speed * Time.deltaTime;
		}
		else
		{
			transform.position += Vector3.left * speed * Time.deltaTime;
		}

		float rightEdgeOfFormation = transform.position.x + (0.5f * width);
		float leftEdgeOfFormation = transform.position.x - (0.5f * width);

		if(leftEdgeOfFormation < xMin || rightEdgeOfFormation > xMax)
		{
			isMovingRight = !isMovingRight;
		}
	}
}
