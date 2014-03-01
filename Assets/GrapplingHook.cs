using UnityEngine;
using System.Collections;

public class GrapplingHook : MonoBehaviour {

	bool grappling = false;
	BaseInput input;
	bool hooked = false;
	SpringJoint joint;

	// Use this for initialization
	void Start () {
		input = GetComponent<BaseInput>();
		joint = GetComponent<SpringJoint>();
	}
	
	// Update is called once per frame
	void Update () {
		joint.spring = 0;
		if(Input.GetKey(KeyCode.V)) {
			joint.spring = 200;
			//GetComponent<SpringJoint>() = cloneJoint; //How the fuck do I turn a join on and off?
		}
		if(input.grapple) {
			if(!grappling) {
			//	ShootHook();
			} else {
				if(hooked) {

				}
			}
		}
	}
}
