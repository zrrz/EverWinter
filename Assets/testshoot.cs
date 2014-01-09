using UnityEngine;
using System.Collections;

public class testshoot : MonoBehaviour {

	public Transform shootPoint;
	public LayerMask ignoredLayers;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)) {
			RaycastHit hit;
			if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, ignoredLayers)) {
				Vector3 shootDir = (hit.point - shootPoint.position);
				shootDir.Normalize();

				Debug.DrawLine(Camera.main.transform.position, hit.point, Color.cyan, 5.0f);
				Debug.DrawLine(shootPoint.position, shootDir, Color.blue, 3.0f);
				BulletManager.instance.Shoot(true, BulletType.normal, shootPoint.position, shootDir);
			} else {
				Vector3 shootTarget = Camera.main.transform.position + (Camera.main.transform.forward * 20.0f);
				Vector3 shootDir = (shootTarget - shootPoint.transform.position);
				shootDir.Normalize();

				Debug.DrawLine(shootPoint.position, shootDir, Color.red, 3.0f);
				BulletManager.instance.Shoot(true, BulletType.normal, shootPoint.position, shootDir);
			}
		}
	}
}
