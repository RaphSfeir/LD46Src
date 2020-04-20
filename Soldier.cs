using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour {

	public float demonDistanceAttack = 5.0f;
	public GameObject targetAttack ;
	public Vector3 targetPosition ; 
	public GameObject targetDefend ;
    public Movable movement;
	public string status; 

	public int delayAttack = 0;
	public int delayAttackMax = 200;
	public int delayIdle = 0;
	public int delayIdleMax = 1000;
	public int damage = 1; 
	void Start () {
		status = "idle";
		delayAttack = delayAttackMax;
		targetAttack = null;
        movement = GetComponent<Movable>();
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.tag == "Demon") {
			if (status == "idle") {
			// Look for ennemies to attack
				GameObject objective = GameObject.FindGameObjectWithTag("Objective");
				targetPosition = objective.transform.position;
				status = "moveTo";
			} 
		}
		if (gameObject.tag == "Soldier" && status != "moveToAttack" && status != "fighting") {
			GameObject closestEnemy = findClosestDemon();
			if (closestEnemy != null) {
				// Found an enmy to attack
				targetAttack = closestEnemy;
				targetPosition = targetAttack.transform.position;
				status = "moveToAttack";
			} else {
				idleSoldier();
			}
		}
		if (status == "moveTo" || status == "moveToAttack") {
			if (status == "moveToAttack") {
				if (targetAttack == null) {
					status = "idle";
				}
			}
			if (targetPosition.x - transform.position.x > 0) {
				movement.goRight();
			}
			if (targetPosition.x - transform.position.x < 0) {
				movement.goLeft();
			}
			if ((targetPosition - transform.position).magnitude > 0.6f && !movement.running) {
				movement.run();
			} 
			Debug.Log((targetPosition - transform.position).magnitude);
			if ((targetPosition - transform.position).magnitude < 0.20f) {
				movement.stop();
				targetPosition = Vector3.zero; 
				status = "idle";
			}
		} else if (status == "fighting") {
			if (targetAttack != null) {
				if (delayAttack <= 0) {
					attack();
				} else {
					delayAttack--;
				}
			} else {
				//Target is dead or destroyed
				status = "idle";
				targetAttack = null;
				targetPosition = Vector3.zero;
				movement.stop();
			}

		}
	}

	public void idleSoldier() {
		if (targetDefend != null && targetPosition == Vector3.zero && delayIdle <= 0) {
			targetPosition = new Vector3(Random.Range(targetDefend.transform.position.x - 3.0f, targetDefend.transform.position.x + 3.0f), targetDefend.transform.position.y);
			status = "moveTo";
			delayIdle = Random.Range(delayIdleMax - 200, delayIdleMax + 200);
		} else {
			delayIdle--;
		}
	}

	public void attack() {
		delayAttack = delayAttackMax;
		Attackable receiverAttackable = targetAttack.GetComponent<Attackable>();
		receiverAttackable.receiveDamage(damage);
	}

	public void engageEnemy() {
		movement.stop();
		status = "fighting";
	}

	public GameObject findClosestDemon() {
		GameObject[] demonObjects = GameObject.FindGameObjectsWithTag("Demon");
		GameObject closest = null;
		float minDistance = Mathf.Infinity;

		foreach (GameObject demon in demonObjects) {
			float distance = (demon.transform.position - transform.position).sqrMagnitude;
			if (distance < minDistance) {
				minDistance = distance;
				closest = demon;
			}
		}
		if (minDistance <= demonDistanceAttack) {
			return closest;
		}
		else return null;
	}
}
