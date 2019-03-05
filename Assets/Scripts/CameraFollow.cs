﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;

    // Update is called once per frame
    void Update()
    {
        if(target != null)
            transform.position = new Vector3(target.transform.position.x, transform.position.y, transform.position.z);
    }
}