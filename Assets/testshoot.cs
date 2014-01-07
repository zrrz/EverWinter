using UnityEngine;
using System.Collections;

public class testshoot : MonoBehaviour {

	public Transform shootPoint;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)) {
			BulletManager.instance.Shoot(true, BulletType.normal, shootPoint.position, shootPoint.transform.forward);
		}
	}
}
