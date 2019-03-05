using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour {

	Rigidbody2D rb;
	public float speed;

	void Start()
	{
		rb = GetComponent<Rigidbody2D> ();
		rb.isKinematic = true;
		rb.velocity = transform.up * speed;
	}
}
