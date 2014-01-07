using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	BaseInput input;

	public float moveSpeed = 3.0f;
	public float turnSpeed = 20.0f;

	public Animator topAnimator;
	public Animator bottomAnimator;

	void Start () {
		input = GetComponent<BaseInput>();
	}

	void Update () {
		float speedMod = input.sprint ? 2.0f : 1.0f;
		transform.position += transform.TransformDirection(new Vector3(0.0f, 0.0f, input.dir.z)) * moveSpeed * speedMod * Time.deltaTime;
		transform.Rotate (new Vector3(0.0f, input.dir.x, 0.0f) * turnSpeed * Time.deltaTime);

		bottomAnimator.SetBool("Running", input.dir.z != 0.0f);
		bottomAnimator.SetBool("Sprinting", input.sprint);

	}

	void OnGUI() {
		GUI.Box(new Rect(0.0f, 0.0f, 100.0f, 40.0f), "WASD to move\nShift to sprint"); 
	}
}
