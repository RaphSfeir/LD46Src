using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Attackable : MonoBehaviour {

	// Use this for initialization
	public int hp; 

	public PlayerController playerController;

	void Start () {
		playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void receiveDamage(int damage) {
		hp -= damage;
		Animator animator = GetComponent<Animator>();
		if (animator != null) {
			animator.SetBool("receive_dmg", true);
		}
		if (hp <= 0) {
			die();
		}
	}

	public void endReceiveDamageAnimation() {
		Animator animator = GetComponent<Animator>();
		if (animator != null) {
			animator.SetBool("receive_dmg", false);
		}

	}

	public void die() {
		if (gameObject.tag == "Objective") {
			SceneManager.LoadScene("GameOver");
		}
		if (gameObject.tag == "DemonObjective") {
			SceneManager.LoadScene("GameSuccess");
		}
		if (gameObject.tag == "Factory" || gameObject.tag == "DemonFactory") {
			playerController.deathBuildingAudio.Play();
		}
		if (gameObject.tag == "Demon") {
			playerController.deathDemonAudio.Play();
		}
		if (gameObject.tag == "Soldier") {
			playerController.deathGuardianAudio.Play();
			Soldier _soldier = gameObject.GetComponent<Soldier>();
			if (_soldier.targetDefend) {
				Tree _tree = _soldier.targetDefend.GetComponent<Tree>();
				if (_tree) {
					_tree.canProduce = true;
					_tree.delayToProduction = 10000; 
				}
			}
		}

		if (gameObject.tag == "DemonFactory") {
			DemonSpawn DS = GameObject.FindGameObjectWithTag("DemonObjective").GetComponent<DemonSpawn>();
			GameManager GM = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
			int previosuDelay = DS.demonSpawnDelay ;
			DS.spawnWaveDemon(GM.factoryCount + 2);
			DS.demonSpawnDelay = previosuDelay + 800;
		}
		Destroy(gameObject);
	}

	void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.tag == "Soldier") {
			Soldier soldierContact = collision.gameObject.GetComponent<Soldier>();
			if (soldierContact.attackOnly && gameObject.tag == "Demon" || gameObject.tag == "DemonObjective" || gameObject.tag == "DemonFactory") 
			{
				soldierContact.targetAttack = gameObject;
				soldierContact.engageEnemy();
			}
			else if (soldierContact.targetAttack == gameObject)  
			{
				soldierContact.engageEnemy();
			}
		}
		if (gameObject.tag == "Demon" && (collision.gameObject.tag == "Soldier" || collision.gameObject.tag == "Factory" || collision.gameObject.tag == "Objective")) {
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
