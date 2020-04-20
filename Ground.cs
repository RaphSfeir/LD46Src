using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour {

	// Use this for initialization

	public GameObject instantiateTree;
	public GameObject instantiateSoldier;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D collision) {
		GameObject collidedWith = collision.gameObject;
		if (collidedWith.tag == "Spirit") {
			Instantiate(instantiateTree, collision.transform.position + new Vector3(0, 0.22f, 0), this.transform.rotation);
			Destroy(collision.gameObject);
		}

		if (collidedWith.tag == "SoldierSpirit") {
			GameObject parentSoldierSpirit = collidedWith.GetComponent<hasParentSpirit>().parent;
			GameObject newSoldierObject = Instantiate(instantiateSoldier, new Vector3(collision.transform.position.x, -0.897f, 0), this.transform.rotation);
			Soldier newSoldier = newSoldierObject.GetComponent<Soldier>();
			newSoldier.targetDefend = parentSoldierSpirit;
			parentSoldierSpirit.GetComponent<Tree>().guardian = newSoldierObject;

			Destroy(collision.gameObject);
		}
	}
}
