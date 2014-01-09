using UnityEngine;
using System.Collections;

public class testshoot : MonoBehaviour {

	public Transform shootPoint;
	public LayerMask rayCastLayers;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire1")) {
			RaycastHit hit;
			if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit,Mathf.Infinity, rayCastLayers)) {
				Vector3 shootDir = (hit.point - shootPoint.position);
				shootDir.Normalize();

				Debug.DrawLine(shootPoint.position, hit.point, Color.cyan, 5.0f);
				BulletManager.instance.Shoot(true, BulletType.normal, shootPoint.position, shootDir);
			} else {
				Vector3 shootTarget = Camera.main.transform.position + (Camera.main.transform.forward * 20.0f);
				Vector3 shootDir = (shootTarget - shootPoint.transform.position);
				shootDir.Normalize();

				BulletManager.instance.Shoot(true, BulletType.normal, shootPoint.position, shootDir);
			}
		}
	}
}
