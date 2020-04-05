using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Jobs;

public class DestroyOnCollision : MonoBehaviour
{



	void OnCollisionEnter(Collision collision)
	{
		Destroy(collision.collider.gameObject);
		Destroy(gameObject);
	}



}
