using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour {

	// Use this for initialization

	public bool growing = true;
	public bool mature = false;

    public float delayToProduction = 500;
    public float delayToProductionCurrent = 500;
    public GameObject guardian ; 
    public bool canProduce;

public GameObject fallingSoldierSpirit ;
    private Animator animator;
void Start()
    {
        animator = GetComponent<Animator>();
        canProduce = true;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Mature", mature);
        if (mature && canProduce) {
            delayToProductionCurrent--;
            if (delayToProductionCurrent <= 0) {
                delayToProductionCurrent = 0;
            }
        }
        if (mature && delayToProductionCurrent == 0 && canProduce && guardian == null) {
            createSoldierSpirit();
            delayToProductionCurrent = delayToProduction;
            canProduce = false;
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
