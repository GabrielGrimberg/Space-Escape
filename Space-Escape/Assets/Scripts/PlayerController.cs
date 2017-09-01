using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
	public float speed = 15.0f;

	//Fixes the ship movement display.
	public float padding = 1f;

	public GameObject Laser;

	public float laserSpeed = 5f;

	public float firingRate = 0.2f;

	public float health  = 250f;

	float xMin;
	float xMax;

	// Use this for initialization
	void Start () 
	{
		float distance = transform.position.z - Camera.main.transform.position.z;

		Vector3 leftMost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance) );

		Vector3 rightMost = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distance) );

		xMin = leftMost.x + padding;
		xMax = rightMost.x - padding;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Space) )
		{
			InvokeRepeating("Fire", 0.000001f, firingRate);
		}
		if(Input.GetKeyUp(KeyCode.Space) )
		{
			CancelInvoke("Fire");
		}

		if (Input.GetKey(KeyCode.LeftArrow) )
		{
			//transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
			transform.position += Vector3.left * speed * Time.deltaTime;
		}

		else if (Input.GetKey(KeyCode.RightArrow))
		{
			//transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
			transform.position += Vector3.right * speed * Time.deltaTime;
		}

		//Restrict the player to the gamespace.
		float newX = Mathf.Clamp(transform.position.x, xMin, xMax);
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);
		
	}

	void Fire()
	{
		Vector3 offset = new Vector3(0, 1, 0);
		GameObject laserAway = Instantiate(Laser, transform.position + offset, Quaternion.identity) as GameObject;
		laserAway.GetComponent<Rigidbody2D>().velocity = new Vector3(0, laserSpeed, 0);
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		Laser missile = collider.gameObject.GetComponent<Laser>();

		if(missile)
		{
			Debug.Log("Enemy hitting");
			health -= missile.getDamage();

			missile.Hit();

			if(health <= 0)
			{
				Destroy(gameObject);
			}
			//Debug.Log("Hit by a laser");

		}
	}
}
