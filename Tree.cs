using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour {

	// Use this for initialization

	public bool growing = true;
	public bool mature = false;

    public float delayToProduction = 0;
    public float delayToProductionCurrent = 100;

public GameObject fallingSoldierSpirit ;
    private Animator animator;
void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Mature", mature);
        if (mature) {
            delayToProductionCurrent--;
        }
        if (mature && delayToProductionCurrent == 0) {
            createSoldierSpirit();
            delayToProductionCurrent = delayToProduction;
        }
    }

    void createSoldierSpirit() {
        GameObject newSpirit = Instantiate(fallingSoldierSpirit, transform.position + new Vector3(0, 0.3f, 0), transform.rotation);
        Rigidbody2D rbNewSpirit = newSpirit.GetComponent<Rigidbody2D>();
        hasParentSpirit parentSpirit = newSpirit.GetComponent<hasParentSpirit>();
        parentSpirit.parent = gameObject;
        Vector2 direction; 
        if (Random.Range(0, 2) >= 1) {
            direction = Vector2.left;
        } else {
            direction = Vector2.right;
        }
        rbNewSpirit.AddForce(direction * Random.Range(0.3f, 0.9f));
    }
}
