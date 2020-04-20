using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour {

	// Use this for initialization

	public bool growing = true;
	public bool mature = false;
    public bool linkedToMegaTree = false;

    public float delayToProduction = 500;
    public float delayToProductionCurrent = 500;
    public GameObject guardian ; 
    public bool canProduce;

    public GameObject fallingSoldierSpirit ;
    public GameObject megaTreePrefab;
    private Animator animator;
void Start()
    {
        animator = GetComponent<Animator>();
        canProduce = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (animator != null) {
            animator.SetBool("Mature", mature);
        }
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

    void turnTreeMature() {
        Debug.Log("TURN TREE MATURE");
        mature = true;
        checkMegaTreeProduce();
    }

    void checkMegaTreeProduce() {
        GameObject[] allTrees = GameObject.FindGameObjectsWithTag("Factory");
        List<Tree> availableTrees = new List<Tree>();
        int countMega = 0;
        if (!linkedToMegaTree) {
            availableTrees.Add(this);
        }
        foreach(GameObject treeGO in allTrees) {
            Tree _tree = treeGO.GetComponent<Tree>();
            if (!_tree.linkedToMegaTree) { 
                countMega++;
                availableTrees.Add(_tree);
            }
        }
        if (countMega >= 3) {
            Debug.Log("Can build MEGA tree !");
            float spawnX = (availableTrees[0].gameObject.transform.position.x + availableTrees[1].gameObject.transform.position.x + availableTrees[2].gameObject.transform.position.x) / 3.0f;
            Instantiate(megaTreePrefab, new Vector3(spawnX, -0.57f, transform.position.z), transform.rotation);
            foreach (Tree builtTree in availableTrees) {
                builtTree.linkedToMegaTree = true;
            }
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
