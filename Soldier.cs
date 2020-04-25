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
	public bool attackOnly = false;
	public bool orderedAttack = false;
    private Animator animator;

	public PlayerController playerController;

	void Start () {
		status = "idle";
		delayAttack = delayAttackMax;
		targetAttack = null;
        movement = GetComponent<Movable>();
		animator = GetComponent<Animator>();
		playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	}

	public void orderAttackOnDemonObjective() {
		GameObject objective = GameObject.FindGameObjectWithTag("DemonObjective");
		targetAttack = objective;
		targetPosition = objective.transform.position;
		status = "moveTo";
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
		if (attackOnly) {
			if (status == "idle") {
			// Look for ennemies to attack
				GameObject objective;
				if (orderedAttack) {
					objective = GameObject.FindGameObjectWithTag("DemonObjective");
				} else {
					objective = GameObject.FindGameObjectWithTag("Borderline");
				}
				targetAttack = objective;
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
					targetPosition = Vector3.zero; 
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
			if ((targetPosition - transform.position).magnitude < 0.20f) {
				movement.stop();
				targetPosition = Vector3.zero; 
				status = "idle";
			}
		} else if (status == "fighting") {
			if (targetAttack != null) {
				if (delayAttack <= 0) {
					startAttack();
				} else {
					delayAttack--;
				}
			} else {
				//Target is dead or destroyed
				status = "idle";
				targetAttack = null;
				targetPosition = Vector3.zero;
				movement.stop();
		        animator.SetBool("fighting", false);
			}

		}
	}

	public void idleSoldier() {
		if (targetDefend == null && !attackOnly) {
			targetDefend = GameObject.FindGameObjectWithTag("Player");
		}
		if (targetDefend != null && targetPosition == Vector3.zero && delayIdle <= 0) {
			targetPosition = new Vector3(Random.Range(targetDefend.transform.position.x - 2.0f, targetDefend.transform.position.x + 2.0f), targetDefend.transform.position.y);
			status = "moveTo";
			delayIdle = Random.Range(delayIdleMax - 200, delayIdleMax + 200);
		} else {
			delayIdle--;
			if (delayIdle <= 0) 
				delayIdle = 0;
		}
	}

	public void startAttack() {
		animator.SetBool("attacking", true);
	}

	public void attack() {
		if (targetAttack != null) {
			delayAttack = delayAttackMax;
			Attackable receiverAttackable = targetAttack.GetComponent<Attackable>();
			if (receiverAttackable != null) {
				receiverAttackable.receiveDamage(damage);
				animator.SetBool("attacking", false);
				if (gameObject.tag == "Soldier") {
					playerController.hitGuardianSound.Play();
				} else if (gameObject.tag == "Demon") {
					playerController.hitDemonSound.Play();
				}
			} else {
				delayAttack = delayAttackMax;
				animator.SetBool("attacking", false);
			}
		} else {
			delayAttack = delayAttackMax;
			animator.SetBool("attacking", false);
		}
	}

	public void engageEnemy() {
		movement.stop();
		status = "fighting";
        animator.SetBool("fighting", true);
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
