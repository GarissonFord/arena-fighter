using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour {

	public float nextFire;
	public float fireRate;
	public GameObject shot;
	public GameObject barrelEnd;

    public ParticleSystem sparksEffect;
    SpriteRenderer sr;

    public Color activatedColor = new Color(0.0f, 1.0f, 0.0f, 0.0f);
    public Color deactivatedColor = new Color(0.0f, 0.2f, 0.0f, 1f);

	public GameObject turret;
	//public int roundNo;

    public bool activated;
    public bool firstTimeActivated = true;

    public AudioSource audio;

	// Use this for initialization
	void Start () 
	{
		turret = gameObject;
		nextFire = 1f;
		fireRate = 2f;
		//roundNo = 1;
        activated = false;
        sparksEffect = GetComponentInChildren<ParticleSystem>();
        sr = GetComponent<SpriteRenderer>();
        sr.color = deactivatedColor;
        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () 
	{
        if (activated)
        {
            if (Time.time > nextFire)
            {
               nextFire = Time.time + fireRate;
               Instantiate(shot, barrelEnd.transform.position, barrelEnd.transform.rotation); //as GameObject;
            }
        }
	}

    /*
     * Remnants of the original version of this from Crosby's class
	void NextRound()
	{
		if(roundNo < 5)
			roundNo++;

		if (roundNo > 2)
			fireRate = 1f;
	}
    */

    public void Activate()
    {
        if(firstTimeActivated)
        {
            sparksEffect.Play();
            audio.Play();
            firstTimeActivated = false;
        }
        StartCoroutine(FadeToColor());
        StartCoroutine(Activation());
    }

    IEnumerator Activation()
    {       
        yield return new WaitForSeconds(2.0f/*sparksEffect.main.duration*/);
        activated = true;
    }

    IEnumerator FadeToBlack()
    {
        for(float i = 1.0f; i >= 0.2f; i -= 0.1f)
        {
            sr.color = new Color(0.0f, i, 0.0f, 1.0f);
            yield return null;
        }
    }

    IEnumerator FadeToColor()
    {
        for (float i = 0.2f; i <= 1.0f; i += 0.1f)
        {
            sr.color = new Color(0.0f, i, 0.0f, 1.0f);
            yield return null;
        }
    }

    public void Deactivate()
    {
        StartCoroutine(FadeToBlack());
        activated = false;
    }
}
