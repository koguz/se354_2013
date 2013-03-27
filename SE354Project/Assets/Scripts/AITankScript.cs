using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class AITankScript : MonoBehaviour {
	private int health = 100;
	private int armour = 50;
	private int puan = 0;
	
	private List<Weapon> weapons;
	private int currentWeapon;
	
	// Use this for initialization
	void Start () {
		weapons = new List<Weapon>();
		weapons.Add(new Weapon()); // default is machine gun
		currentWeapon = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)) Fire();
	}
	
	public void pickupItem(Weapon weapon) {
		bool iDontHaveThisWeapon = true;
		foreach (Weapon w in weapons) {
			if (w.name.Equals(weapon.name)) {
				iDontHaveThisWeapon = false;
				w.ammoCount = weapon.ammoCount;
			}
		}
		if(iDontHaveThisWeapon) {
			weapons.Add(weapon);
		}
	}
	
	public List<Weapon> getWeapons() { 
		return weapons; 
	}
	
	public void setCurrentWeapon(int index) {
		if(index >= weapons.Count) {
			Debug.LogError("Weapon can not be set - index out of range");
			return; 
		}
		currentWeapon = index;
	}
	
	public bool takeAHit(int damage) {
		health -= damage;
		//Debug.Log(health);
		if(health <= 0) {
			Destroy(gameObject);
			return true;
		} else return false;
	}
	
	public void increasePoints() {
		puan++;
		//Debug.Log ("hit!");
	}
	
	public void Fire() {
		if 
			( weapons[currentWeapon].ammoCount == 0 ||
			  (Time.time - weapons[currentWeapon].lastFired) < weapons[currentWeapon].ammoPerSec
			) {
			Debug.LogWarning("Can not fire... yet (check ammo or ammoPerSec)");
			return;
		}
		Vector3 direction = gameObject.transform.forward;
		GameObject mermi = (GameObject) GameObject.Instantiate(Resources.Load ("Bullet"));
		mermi.transform.position = gameObject.transform.position + (new Vector3(0.003f, 0.225f, 0.7f));
		mermi.GetComponent<Bullet>().damage = weapons[currentWeapon].damPerAmmo;
		mermi.GetComponent<Bullet>().parent = this;
		mermi.GetComponent<Bullet>().direction = direction;
		weapons[currentWeapon].lastFired = Time.time;
		if(!weapons[currentWeapon].name.Equals("Machine Gun")) {
			weapons[currentWeapon].ammoCount--;
		}
	}
	
}