using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attackable : MonoBehaviour {

	// Use this for initialization
	public int hp; 
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void receiveDamage(int damage) {
		hp -= damage;
		if (hp <= 0) {
			die();
		}
	}

	public void die() {
		if (gameObject.tag == "Objective") {
			Debug.Log("You lose");
			//Game Over
		}
		if (gameObject.tag == "Soldier") {
			Soldier _soldier = gameObject.GetComponent<Soldier>();
			if (_soldier.targetDefend) {
				Tree _tree = _soldier.targetDefend.GetComponent<Tree>();
				_tree.canProduce = true;
				_tree.delayToProduction = 10000; 
			}
		}
		Destroy(gameObject);
	}

	void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.tag == "Soldier") {
			Soldier soldierContact = collision.gameObject.GetComponent<Soldier>();
			if (soldierContact.targetAttack == gameObject) {
				soldierContact.engageEnemy();
			}

		}
		if (gameObject.tag == "Demon" && (collision.gameObject.tag == "Soldier" || collision.gameObject.tag == "Factory")) {
			if (collision.gameObject.tag == "Soldier") {
				Soldier soldierContact = collision.gameObject.GetComponent<Soldier>();
				soldierContact.targetAttack = gameObject;
				soldierContact.engageEnemy();
			}
			Soldier demonSoldier = gameObject.GetComponent<Soldier>();
			demonSoldier.targetAttack = collision.gameObject;
			demonSoldier.engageEnemy();
		}
	}

	void OnTriggerStay2D(Collider2D collision) {
		Soldier soldierContact = collision.gameObject.GetComponent<Soldier>();
		if (soldierContact != null) {
			if (soldierContact.status != "fighting") {
				OnTriggerEnter2D(collision);
			}
		}
	}

}
