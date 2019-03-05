using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevel : MonoBehaviour 
{
	public StageManager sm;
	public BoxCollider2D collider;
	public bool switchedOn = false;
	public AudioSource alarm;

	void Awake()
	{
		sm = GetComponentInParent<StageManager> ();
		collider = GetComponent<BoxCollider2D> ();
		alarm = GetComponent<AudioSource> ();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("Player") && !(switchedOn))
		{
			alarm.Play ();
			Debug.Log("Switched On");
			sm.Invoke("Spawn", 3f);
			switchedOn = true;

			Color c = GetComponent<SpriteRenderer>().color;
			c = Color.red;
			c.a = 1;
			GetComponent<SpriteRenderer>().color = c;
			this.gameObject.SetActive (false);
		}
	}
}
