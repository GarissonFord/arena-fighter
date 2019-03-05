using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour 
{
	//Inspector-friendly int for how many seconds to wait between 
	//TRYING to spawn a turret
	public int timeBetweenTurretSpawns = 1;

	//Create an array of turret spawn points (spawnPoints)
	public Transform[] spawnPoints;
	public bool[] isSpawned;
	public GameObject turret;
	public GameObject[] turrets;
	int num;
	//int numTurrets;

	void Awake()
	{
		//Number of possible spawn points
		//numTurrets = 0;
		isSpawned = new bool[spawnPoints.Length];
		turrets = new GameObject[spawnPoints.Length];
		num = turrets.Length;

		//Make all isSpawned elements false at first
		for(int i = 0; i < num - 1; i++)
		{
			isSpawned[i] = false;
		}
		//Debug.Log (numTurrets);
	}

	void Spawn()
	{ 	
		CheckTurrets();
		//Get a random index of the spawnPoints array
		int i = Random.Range(0, turrets.Length);
		if (! isSpawned [i]) 
		{
			turrets[i] = Instantiate (turret, spawnPoints [i].position, spawnPoints [i].rotation);
			isSpawned [i] = true;
			//numTurrets++;
			//Debug.Log (numTurrets);
		} 

		Invoke ("Spawn", timeBetweenTurretSpawns);
	}

	void CheckTurrets()
	{
		for (int i = 0; i < turrets.Length; i++) 
		{
			//If a turret was destroyed,
			if (turrets [i] == null)
				//... mark its spawn point to be available
				isSpawned [i] = false;
		}
	}

	/*
	public void TurretDestroyed()
	{
		if (numTurrets == 1) 
		{
			numTurrets--;
			StageClear ();
			return;
		}
		numTurrets--;
		Debug.Log (numTurrets);
	}
	*/

	public void StageClear()
	{
		//Some sort of visual feedback
		Debug.Log("Stage Complete");
		//Spawn another platform
		//Invoke("Spawn", 3f);
	}
}
