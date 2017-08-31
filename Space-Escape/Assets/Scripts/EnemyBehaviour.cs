using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour 
{
	public float health = 150;

	void OnTriggerEnter2D(Collider2D collider)
	{
		//Debug.Log(collider);
		Laser missile = collider.gameObject.GetComponent<Laser>();

		if(missile)
		{
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
