using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour 
{
	public GameController GC;

	public GameObject player;
	public Transform playerSpawn;
	Vector2 spawnPosition;
	private Quaternion spawnRotation; 

	public Text scoreText;
	public int enemiesDestroyed;
	public Text gameOverText;

	public Text timerText;
	public float timeLeft = 60;

	public GameObject turret;

	public AudioClip firstAudioClip;
	public AudioClip secondAudioClip;
	public AudioSource [] audioBG;

	void Start()
	{
		GC = this;
		gameOverText.text = "";
		spawnPosition = new Vector2(playerSpawn.position.x, playerSpawn.position.y);
		//SpawnPlayers ();
		DisplayScore ();
		DisplayTimeLeft ();

		audioBG = GetComponents<AudioSource> ();
		Debug.Log (audioBG[0] + " " + audioBG[1]);
	}

	void Update()
	{
		DisplayScore ();
		timeLeft -= Time.deltaTime;
		if (timeLeft < 0) 
		{
			turret.SendMessage ("NextRound");	
		}
		DisplayTimeLeft ();

		if (timeLeft <= 30) changeTrack ();
	}

	void SpawnPlayers()
	{ 
		Instantiate (player, spawnPosition, spawnRotation);
	}

	void changeTrack()
	{
		//AudioClip.PlayOneShot (secondAudioClip, 1.0f);
	}

	//Accessible by objects that destroy the player and end the game
	public void GameOver()
	{
		gameOverText.text = "Game Over";
		Invoke("ReloadGame", 5f);
	}

	void ReloadGame()
	{
		SceneManager.LoadScene ("Testing");
	}

	void DisplayScore()
	{
		scoreText.text = enemiesDestroyed.ToString();
	}

	void DisplayTimeLeft()
	{
		timerText.text = "0:" + timeLeft.ToString ();
	}
}
