using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	BaseInput input;

	public float moveSpeed = 3.0f;
	public float turnSpeed = 20.0f;

	public Animator topAnimator;
	public Animator bottomAnimator;

	public bool grounded;

	const float GROUND_CHECK_DISTANCE = 1.0f;

	public LayerMask mask;

	public float jumpStrength = 3.0f;

	public Material snowMat;

	float snowLevel = 0.0f;

	public float snowAccumSpeed = 0.2f;

	bool moved;

	public Material mat;

	void Start () {
		input = GetComponent<BaseInput>();
	}

	void Update () {
		if (moved) {
			snowLevel -= snowAccumSpeed * 5.0f * Time.deltaTime;
			if(snowLevel < 0.1f) {
				snowLevel = 0.1f;
				moved = false;
			}
		} else {
			snowLevel += snowAccumSpeed * Time.deltaTime;
			if (snowLevel > 1.0f)
				snowLevel = 1.0f;
			if (rigidbody.velocity.sqrMagnitude > 0.05f || input.dir.magnitude > 0.0f) 
				moved = true;
		}
		mat.SetFloat ("_Snow", snowLevel);

		bottomAnimator.SetFloat ("SnowLevel", snowLevel);
		topAnimator.SetFloat ("SnowLevel", snowLevel);

		float speedMod = input.sprint ? 2.0f : 1.0f;
		rigidbody.MovePosition(transform.position + transform.TransformDirection(new Vector3(0.0f, 0.0f, input.dir.z)) * moveSpeed * speedMod * Time.deltaTime);
		rigidbody.MoveRotation (Quaternion.Euler(transform.eulerAngles + new Vector3 (0.0f, input.dir.x, 0.0f) * turnSpeed * Time.deltaTime));
		//transform.Rotate (new Vector3(0.0f, input.dir.x, 0.0f) * turnSpeed * Time.deltaTime);

		bottomAnimator.SetBool("Running", input.dir.z != 0.0f);
		bottomAnimator.SetBool("Sprinting", input.sprint);

		grounded = CheckGrounded();
		bottomAnimator.SetBool("Grounded", grounded);

		bottomAnimator.SetBool("Jump", false);
		if (grounded) {
			if(input.jump) {
				rigidbody.AddForce(Vector3.up * jumpStrength);
				bottomAnimator.SetBool("Jump", true);
			}
		}
	}

	bool CheckGrounded() {
		Debug.DrawRay (transform.position + (Vector3.up * 0.1f), Vector3.down, Color.red, 1.0f);
		return Physics.Raycast (transform.position + Vector3.up, Vector3.down, GROUND_CHECK_DISTANCE, mask);
	}

	void OnGUI() {
		GUI.Box(new Rect(0.0f, 0.0f, 100.0f, 40.0f), "WASD to move\nShift to sprint"); 
	}
}
