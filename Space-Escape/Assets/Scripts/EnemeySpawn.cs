using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeySpawn : MonoBehaviour 
{
	public GameObject enemyPrefab;
	public float width = 10f;
	public float height = 5f;
	public float speed = 5f;
	public float spawnDelay = 0.5f;

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

		SpawnUntilFull();
	}

	//Method to load up enemies.
	void SpawnEnemies()
	{
		//Loop over every child object (Enemy).
		foreach(Transform child in transform)
		{
			GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
		}
	}

	void SpawnUntilFull()
	{
		Transform freePos = NextFreePosition();

		//If there is a position.
		if(freePos)
		{
			GameObject enemy = Instantiate(enemyPrefab, freePos.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = freePos;
		}

		//Timer to appear enemies. Only spawn an enemy if there is a free position.
		if(NextFreePosition() )
		{
			Invoke("SpawnUntilFull", spawnDelay);
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

		if(leftEdgeOfFormation < xMin)
		{
			isMovingRight = true;
		}
		else if(rightEdgeOfFormation > xMax)
		{
			isMovingRight = false;
		}

		if(AllMembersDead() )
		{
			Debug.Log("Empty Formation");
			SpawnUntilFull();
		}
	}

	Transform NextFreePosition()
	{
		foreach(Transform childPosGameObject in transform)
		{
			if(childPosGameObject.childCount == 0)
			{
				return childPosGameObject;
			}
		}
		return null;
	}

	//Check to see if all enemies are dead.
	bool AllMembersDead()
	{
		foreach(Transform childPosGameObject in transform)
		{
			if(childPosGameObject.childCount > 0)
			{
				return false;
			}
		}
		return true;
	}
}
