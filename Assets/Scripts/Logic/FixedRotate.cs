﻿using UnityEngine;
using System.Collections;

[AddComponentMenu("Scripts/Game/FixedRotate")]
public class FixedRotate : MonoBehaviour 
{
    public float speedRotation = 45.0f;
	
	void Update () 
    {
        transform.Rotate(Vector3.up, speedRotation * Time.deltaTime);
	}
}
