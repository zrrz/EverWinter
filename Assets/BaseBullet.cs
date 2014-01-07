using UnityEngine;
using System.Collections;

public enum BulletType {normal, incendiary, freeze, knockback}

[RequireComponent(typeof(Rigidbody))]
public class BaseBullet : MonoBehaviour {

	public BulletType type;
	public float speed = 200.0f;
	public float lifeTime = 3.0f;

	Rigidbody thisRigidbody;
	Transform thisTransform;
	float lifeTimer = 0.0f;

	// Use this for initialization
	void Awake () {
		thisRigidbody = GetComponent<Rigidbody>();
		thisTransform = this.transform;

		type = BulletType.normal;
	}

	public void Shoot(bool isPlayerBullet, Vector3 shootPos, Vector3 dir) {
		if(isPlayerBullet)
			thisTransform.tag = "PlayerBullet";
		else
			thisTransform.tag = "EnemyBullet";

		thisTransform.position = shootPos;
		thisRigidbody.AddForce(dir * speed);
	}
	

	void Update () {
		if(lifeTimer >= lifeTime) {
			lifeTimer = 0.0f;
			gameObject.SetActive(false);
			return;
		}

		lifeTimer += Time.deltaTime;
	}
}


