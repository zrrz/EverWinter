﻿using UnityEngine;
using System.Collections;

public class PlayerInput : BaseInput {
	
	void Start () {
	
	}

	void Update () {
		dir = new Vector3 (Input.GetAxis ("Horizontal"), 0.0f, Input.GetAxis ("Vertical"));
	}
}
