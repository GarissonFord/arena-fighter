using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour 
{
	public GameController gameController;
	public Rigidbody2D rb;
	public StageManager sm;

	public AudioSource deathSound;

	void Start()
	{
		//Get reference to game controller so we can call to restart the game
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) 
		{
			gameController = gameControllerObject.GetComponent<GameController>();
		}
		rb = GetComponent<Rigidbody2D> ();
		//sm = GameObject.Find("Stage").GetComponent<StageManager>();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		GameObject otherGO = other.gameObject;
		//Without this, the boundary would just disappear upon the first bullet fire
		if (otherGO.CompareTag ("Boundary")) {
			return;
		} else if (otherGO.CompareTag ("Block")) {
			//If bullet contacted shield
			bool deflected = DeflectBullet.DeflectCheck ();
			if (deflected) {
				Debug.Log ("Deflected");
				Vector2 velocity = rb.velocity;
				velocity.x *= -1;
				rb.velocity = velocity;
			} else {
				//Destroy shield and bullet if not deflected in time
				Destroy (other.gameObject);
				Destroy (gameObject);
			}
			return;
		} else if (otherGO.CompareTag ("Player")) {
			//If the bullet hit the player
			//Call restart function which has a delay of about 2f seconds
			deathSound.Play ();
			//gameController.GameOver ();
		} else if (otherGO.CompareTag ("Enemy")) {
			//gameController.enemiesDestroyed++;
		}
        else if(otherGO.CompareTag("TurretTrigger"))
        {
            return;
        }

		//Always destroys the bullet and the collided object
		Destroy (other.gameObject);
		Destroy (gameObject);
	}
}
