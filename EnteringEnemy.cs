using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnteringEnemy : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.tag == "Player") {
			Debug.Log("Player DECLARED WAR !!");
			GameObject[] allSoldiers = GameObject.FindGameObjectsWithTag("Soldier");
			List<Soldier> attackSoldiers = new List<Soldier>();
			foreach (GameObject cSoldier in allSoldiers) {
				Soldier cSoldierScript = cSoldier.GetComponent<Soldier>();
				if (cSoldierScript.attackOnly) {
					attackSoldiers.Add(cSoldierScript);
				}
			}
			foreach (Soldier solScript in attackSoldiers) {
				solScript.orderAttackOnDemonObjective();
				solScript.orderedAttack = true;
			}
		}
	}
}
