using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
	//Used for the Flip() function
	public bool facingRight = true;

	//Tells us when the player is airborne 
	public bool jump;

	//So we can decide if a player can double jump
	public bool doubleJump;

	//Prefabs for shield and where to spawn it
	public GameObject block;
	public GameObject shieldSpawn;

	//i.e. Speed
	public float moveSpeed;
	//public float maxSpeed;

	//Power of a jump
	public float jumpForce;

	//Rigidbody2D reference
	private Rigidbody2D rb;

	//Reference to the bullet prefab and where to spawn it
	public GameObject bullet;
	public GameObject bulletSpawn;

	//How fast a bullet can be fired
	public float fireRate;
	private float nextFire = 0.25f;
	public float timeSinceLastShot;
	public float timeOfLastShot;

	//To check when the player is touching the ground
	public Transform groundCheck;
	public bool grounded;

	//Animator reference
	//public Animator animator;

    public bool crouching;
    //How much force is applied to the Rigidbody when we slide
    public float slideForce;

	//Our GetAxis value for movement
	public float h;

	void Awake()
	{
		rb = GetComponent<Rigidbody2D> ();
		timeSinceLastShot = 0.0f;
	}

	void Update()
	{	
		//Uses a vertical linecast to see if the player is touching the ground
		grounded = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground"));
		//Sets tsls to the current time subtracted by the time of the last shot
		timeSinceLastShot = Time.time - timeOfLastShot;

		if (Input.GetButtonDown ("Jump") && grounded && !crouching) 
		{
			grounded = false;
			jump = true;
		}
			
		//Taken from SpaceShooter tutorial
		if (Input.GetButtonDown ("Fire1") && Time.time > nextFire) 
		{
			timeOfLastShot = Time.time;
			nextFire = Time.time + fireRate;
			Instantiate(bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
		}

		//"Block" is right mouse button
		if (Input.GetButtonDown ("Block")) 
		{
			Instantiate (block, shieldSpawn.transform.position, shieldSpawn.transform.rotation, this.transform);
		}
		//Destroys shield once right mouse button is released
		if (Input.GetButtonUp ("Block"))
		{
			Destroy (GameObject.FindWithTag ("Block"));
		}
        
        if (Input.GetAxis("Vertical") < 0.0f && grounded)
        {
            crouching = true;
            transform.localScale = new Vector3(transform.localScale.x, 1.25f, 1.616569f);
        }
        else if (Input.GetAxis("Vertical") >= 0.0f && grounded)
        {
            crouching = false;
            transform.localScale = new Vector3(transform.localScale.x, 2.5f, 1.616569f);
        }
        
        if(crouching && grounded && Input.GetButtonDown("Jump"))
        {
            //Slide
            if (facingRight)
            {
                rb.AddForce(transform.right * slideForce);
            }
            else if (!facingRight)
            {
                rb.AddForce(transform.right * -slideForce);
            }
        }
    }

	void FixedUpdate()
	{
		h = Input.GetAxis ("Horizontal");

        if (h != 0.0f && !crouching)
            rb.velocity = new Vector2(h * moveSpeed, rb.velocity.y);

        /*
		//The following two conditionals create a speed cap
		if (h * rb.velocity.x < maxSpeed && !crouching) 
		{
			rb.AddForce (Vector2.right * h * moveForce);
		}

		if (Mathf.Abs (rb.velocity.x) > maxSpeed) 
		{
			//Mathf.Sign returns -1 or 1 depending on the sign of the input
			rb.velocity = new Vector2 (Mathf.Sign (rb.velocity.x) * maxSpeed, rb.velocity.y);
		}
        */

        if (h > 0 && !facingRight)
        {
            //Flips when hitting 'right' and facing left
            Flip();
        }
        else if (h < 0 && facingRight)
        {
            //Flips when hitting 'left' and facing right
            Flip();
        }

		if(grounded)
		{
			doubleJump = false;
		}

		if (jump) 
		{
			rb.velocity = new Vector2(rb.velocity.x, jumpForce);
			jump = false;
		}

		//Double Jump
		if (Input.GetButtonDown("Jump") && !grounded && !doubleJump) 
		{
			rb.velocity = new Vector2 (rb.velocity.x, jumpForce);
			doubleJump = true;
		}
	}

	//Changes rotation of the player
	void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.CompareTag("TurretTrigger"))
        {
            bool isActivated = coll.gameObject.GetComponentInParent<FireBullet>().activated;
            if(!isActivated)
                coll.gameObject.SendMessageUpwards("Activate");
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if(coll.gameObject.CompareTag("TurretTrigger"))
        {
            coll.gameObject.SendMessageUpwards("Deactivate");
        }
    }
} 
