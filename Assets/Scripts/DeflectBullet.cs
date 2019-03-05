using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeflectBullet : MonoBehaviour 
{
	//Time when shield was initialized
	public static float initTime;
	public static float shieldDeflectTime = 0.3f;

	void Awake()
	{
		initTime = Time.time;
	}

	//Static method so any bullet can call on this
	public static bool DeflectCheck()
	{
		//If the time since the shield spawned is greater 
		if (Time.time - initTime < shieldDeflectTime) 
		{
			return true;
		} else 
		{
			return false;
		}
	}
}
