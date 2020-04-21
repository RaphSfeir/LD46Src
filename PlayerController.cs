using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	// Use this for initialization

    public int spiritCount;

    public List<PickUpSpirit> carryingSpirits;
    public Movable movement;
    public AudioSource music;
    public AudioSource ambientSound;

	void Awake () {
        movement = GetComponent<Movable>();
	}
	
	// Update is called once per frame
	void Update () {
        controlInputs();
	}

    void Start() {
        music.Play();
        ambientSound.Play();
    }

    void controlInputs () {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                movement.goLeft();
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                movement.goRight();
            }
            else
            {
                movement.stop();
            }
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) 
            {
                movement.run();
            }
            if (Input.GetKeyDown(KeyCode.Space)) {
                if (canDropSpirit()) {
                    dropSpirit();
                }
            }
    }

    public bool canDropSpirit() {
        return (spiritCount > 0 && carryingSpirits.Count > 0);
    }

    void dropSpirit() {
        Debug.Log("drop spirit !");
        //GameObject newSpirit = carryingSpirits[0].gameObject;
        carryingSpirits[0].mutateToFallingSpirit();
        carryingSpirits.Remove(carryingSpirits[0]);
        /* GameObject newSpirit = Instantiate(spiritDrop, transform.position, transform.rotation);
        Rigidbody2D rbNewSpirit = newSpirit.GetComponent<Rigidbody2D>();
        rbNewSpirit.AddForce(Vector2.up); */
        spiritCount--;
    }
}
