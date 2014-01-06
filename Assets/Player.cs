using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	BaseInput input;

	public float moveSpeed = 3.0f;
	public float turnSpeed = 20.0f;

	void Start () {
		input = GetComponent<BaseInput>();
	}

	void Update () {
		transform.position += transform.TransformDirection(new Vector3(0.0f, 0.0f, input.dir.z)) * moveSpeed * Time.deltaTime;
		transform.Rotate (new Vector3(0.0f, input.dir.x, 0.0f) * turnSpeed * Time.deltaTime);
	}
}
