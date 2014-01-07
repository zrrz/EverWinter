using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public GameObject target;
	public Vector3 offset;

	const float CAMERA_TURN_SPEED = 0.1f;
	const float CAMERA_FOLLOW_SPEED = 0.1f;

	public bool DEBUG_LERP_ON = false;

	void Start () {
	
	}

	void Update () {
		if (DEBUG_LERP_ON) {
			if (Vector3.Distance (transform.position, target.transform.position + target.transform.TransformDirection (offset)) > 0.2f) {
				transform.position = Vector3.Lerp (transform.position, target.transform.position + target.transform.TransformDirection (offset), Time.deltaTime);
			}
			transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (target.transform.position - transform.position), CAMERA_TURN_SPEED);
		} else {
			transform.position = target.transform.position + target.transform.TransformDirection (offset);
			transform.rotation = Quaternion.LookRotation (target.transform.position - transform.position);
		}
	}
}
