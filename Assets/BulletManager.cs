using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletManager : MonoBehaviour {

	public static BulletManager instance;
	
	List<BaseBullet> bullets;

	public GameObject normalBullet;
	public GameObject incendiaryBullet;
	public GameObject freezeBullet;
	public GameObject knockbackBullets;

	public float numNormalBullets;
	public float numIncendiaryBullets;
	public float numFreezeBullets;
	public float numKnockBackBullets;

	void Awake() {
		if(instance == null) 
			instance = this;
	}

	void Start() {
		InitializeList();
	}
	
	void InitializeList () {
		bullets = new List<BaseBullet>();

		for(int i = 0; i < numNormalBullets; i++) {
			GameObject temp = (GameObject)Instantiate(normalBullet, Vector3.zero, Quaternion.identity);
			temp.transform.parent = this.transform;
			bullets.Add(temp.GetComponent<BaseBullet>());
			temp.SetActive(false);
		}
		for(int i = 0; i < numIncendiaryBullets; i++) {
			GameObject temp = (GameObject)Instantiate(normalBullet, Vector3.zero, Quaternion.identity);
			temp.transform.parent = this.transform;
			bullets.Add(temp.GetComponent<BaseBullet>());
			temp.SetActive(false);
		}
		for(int i = 0; i < numFreezeBullets; i++) {
			GameObject temp = (GameObject)Instantiate(normalBullet, Vector3.zero, Quaternion.identity);
			temp.transform.parent = this.transform;
			bullets.Add(temp.GetComponent<BaseBullet>());
			temp.SetActive(false);
		}
		for(int i = 0; i < numKnockBackBullets; i++) {
			GameObject temp = (GameObject)Instantiate(normalBullet, Vector3.zero, Quaternion.identity);
			temp.transform.parent = this.transform;
			bullets.Add(temp.GetComponent<BaseBullet>());
			temp.SetActive(false);
		}
	}

	public void Shoot(bool isPlayerBullet, BulletType type, Vector3 shootPos, Vector3 dir) {
		BaseBullet temp = FindBullet(type);
		if(temp != null)
			temp.Shoot(isPlayerBullet, shootPos, dir);
	}

	BaseBullet FindBullet(BulletType type) {
		foreach(BaseBullet temp in bullets) {
			if(temp.gameObject.activeSelf == false && temp.type == type) {
				temp.gameObject.SetActive(true);
				return temp;
			}
		}

		return null;
	}
}
