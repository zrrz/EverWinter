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
	Vector3 direction;
	float lifeTimer = 0.0f;
	int damage = 1;

	// Use this for initialization
	void Awake () {
		thisRigidbody = GetComponent<Rigidbody>();
		thisTransform = this.transform;

		type = BulletType.normal;
	}

	public virtual void Shoot(bool isPlayerBullet, Vector3 shootPos, Vector3 dir) {
		if(isPlayerBullet)
			thisTransform.tag = "PlayerBullet";
		else
			thisTransform.tag = "EnemyBullet";

		thisTransform.position = shootPos;
		direction = dir;
	}

	void Update () {
		if(lifeTimer >= lifeTime) {
			lifeTimer = 0.0f;
			gameObject.SetActive(false);
			return;
		}

		thisTransform.position += direction * speed * Time.deltaTime;

		lifeTimer += Time.deltaTime;
	}

	void OnTriggerEnter(Collider col) {
		if(thisTransform.tag == "PlayerBullet" && col.tag.Contains("Enemy"))
			col.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
		else if(thisTransform.tag == "EnemyBullet" && col.tag == "Player")
			col.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
	}
}


