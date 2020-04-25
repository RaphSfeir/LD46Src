using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour {

	// Use this for initialization

	public GameObject instantiateTree;
	public GameObject instantiateSoldier;
	public GameObject instantiateAttackSoldier;

	public Objective _objective;
	void Start () {
		GameObject ObjectiveGO = GameObject.FindGameObjectWithTag("Objective");
		_objective = ObjectiveGO.GetComponent<Objective>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D collision) {
		GameObject collidedWith = collision.gameObject;
		if (collidedWith.tag == "Spirit") {
			//Check first spirit
			if (collidedWith.gameObject == _objective.firstLuciole) {
				Debug.Log("collided with first luciole !");
				_objective._text.enabled = false;
			}
			Instantiate(instantiateTree, collision.transform.position + new Vector3(0, 0.22f, 0), this.transform.rotation);
			Destroy(collision.gameObject);
		}

		if (collidedWith.tag == "SoldierSpirit") {
			GameObject parentSoldierSpirit = collidedWith.GetComponent<hasParentSpirit>().parent;
			GameObject newSoldierObject = Instantiate(instantiateSoldier, new Vector3(collision.transform.position.x, -0.897f, 0), this.transform.rotation);
			Soldier newSoldier = newSoldierObject.GetComponent<Soldier>();
			if (parentSoldierSpirit != null) {
				newSoldier.targetDefend = parentSoldierSpirit;
				parentSoldierSpirit.GetComponent<Tree>().guardian = newSoldierObject;
			}
			Movable newMove = newSoldier.GetComponent<Movable>();
			newMove.movementSpeed = newMove.movementSpeed + Random.Range(-0.05f, 0.05f);

			Destroy(collision.gameObject);
		}
		if (collidedWith.tag == "SoldierAttackSpirit") {
			GameObject parentSoldierSpirit = collidedWith.GetComponent<hasParentSpirit>().parent;
			GameObject newSoldierObject = Instantiate(instantiateAttackSoldier, new Vector3(collision.transform.position.x, -0.897f, 0), this.transform.rotation);
			Soldier newSoldier = newSoldierObject.GetComponent<Soldier>();
			if (parentSoldierSpirit != null) {
				newSoldier.targetDefend = parentSoldierSpirit;
				parentSoldierSpirit.GetComponent<Tree>().guardian = newSoldierObject;
			}
			newSoldier.targetAttack = GameObject.FindGameObjectWithTag("DemonObjective");
			Movable newMove = newSoldier.GetComponent<Movable>();
			newMove.movementSpeed = newMove.movementSpeed + Random.Range(-0.05f, 0.05f);
			Destroy(collision.gameObject);
		}
	}
}
