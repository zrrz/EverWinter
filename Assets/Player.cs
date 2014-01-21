using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	BaseInput input;

	public float moveSpeed = 3.0f;
	public float sprintSpeedMod = 1.8f;
	public float turnSpeed = 0.6f;

	public Animator topAnimator;
	public Animator bottomAnimator;

	public bool grounded;

	const float GROUND_CHECK_DISTANCE = 1.0f;

	public LayerMask mask;

	public float jumpStrength = 3.0f;

	float snowLevel = 0.0f;

	public float snowAccumSpeed = 0.2f;

	bool moved;

	public GameObject enemy;

	public Material mat;

	[System.NonSerialized]					
	public float lookWeight;					// the amount to transition when using head look	
	
	public float lookSmoother = 3f;				// a smoothing setting for camera motion

	public Transform cameraObj;
	
	void Start () {
		input = GetComponent<BaseInput>();
	}

	void Update () {
		//-------------------Snow stuff---------------------//
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

		//---------------------------------------------------//
		//---------------------Movement----------------------//
		float speedMod = input.sprint ? sprintSpeedMod : 1.0f;
		rigidbody.angularVelocity = Vector3.zero;

		if (input.dir.magnitude > 0.0f) {
			rigidbody.MovePosition(transform.position + transform.forward * moveSpeed * speedMod * Time.deltaTime);

			Vector3 lookDir = Vector3.zero;
			if(Mathf.Abs(input.dir.z) > 0.0f)
				lookDir += (transform.position - cameraObj.transform.position).normalized * Mathf.Sign(input.dir.z);
			if(Mathf.Abs(input.dir.x) > 0.0f)
				lookDir += Vector3.Cross(transform.up, (transform.position - cameraObj.transform.position).normalized) * Mathf.Sign(input.dir.x);
			lookDir.Normalize();
			lookDir.y = 0.0f;

			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation (lookDir), turnSpeed);
		}

		bottomAnimator.SetBool("Running", input.dir.magnitude != 0.0f);
		bottomAnimator.SetBool("Sprinting", input.sprint);

		topAnimator.SetBool("Running", input.dir.magnitude != 0.0f);
		topAnimator.SetBool("Sprinting", input.sprint);

		grounded = CheckGrounded();
		bottomAnimator.SetBool("Grounded", grounded);
		topAnimator.SetBool("Grounded", grounded);

		bottomAnimator.SetBool("Jump", false);
		topAnimator.SetBool("Jump", false);
		if (grounded) {
			if(input.jump) {
				rigidbody.AddForce(Vector3.up * jumpStrength);
				bottomAnimator.SetBool("Jump", true);
				topAnimator.SetBool("Jump", true);
			}
		}
		//--------------------------------------------------//

		if(Input.GetButton("Fire2"))
		{
			// ...set a position to look at with the head, and use Lerp to smooth the look weight from animation to IK (see line 54)
			topAnimator.SetLookAtPosition(enemy.transform.position);
			lookWeight = Mathf.Lerp(lookWeight,1f,Time.deltaTime*lookSmoother);
		
		}
		// else, return to using animation for the head by lerping back to 0 for look at weight
		else
		{
			lookWeight = Mathf.Lerp(lookWeight,0f,Time.deltaTime*lookSmoother);
		}
		topAnimator.SetLookAtWeight(lookWeight);
	}

	bool CheckGrounded() {
		Debug.DrawRay (transform.position + (Vector3.up * 0.1f), Vector3.down, Color.red, 1.0f);
		return Physics.Raycast (transform.position + Vector3.up, Vector3.down, GROUND_CHECK_DISTANCE, mask);
	}

	void OnGUI() {
		GUI.Box(new Rect(0.0f, 0.0f, 100.0f, 60.0f), "WASD to move\nShift to sprint\nT to restart"); 
	}
}
