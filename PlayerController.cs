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
    public AudioSource step1;
    public AudioSource step2;
    public AudioSource pickupSpiritSound;
    public AudioSource hitGuardianSound;
    public AudioSource hitDemonSound;
	public AudioSource deathDemonAudio;
	public AudioSource deathBuildingAudio;
	public AudioSource deathGuardianAudio;

    public GameManager gameManager;

    public bool cutscene = false;

	void Awake () {
        movement = GetComponent<Movable>();
	}
	
	// Update is called once per frame
	void Update () {
        if (cutscene) {
            cutSceneControlInputs();
        } else {
            controlInputs();
        }
	}

    void Start() {
        music.Play();
        ambientSound.Play();
    }

    public void step1Sound() {
        step1.Play();
    }

    public void step2Sound() {
        step2.Play();
    }

    public void pickUpSound() {
        pickupSpiritSound.Play();
    }
    void cutSceneControlInputs () {
            //if (Input.GetAxis("Submit") > 0) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                gameManager.nextStepCustscene();
            }
    }

    void controlInputs () {
            //if (Input.GetKey(KeyCode.LeftArrow))
            if (Input.GetKey(KeyCode.Escape)) {
                Application.Quit();
            }
            if (Input.GetAxis("Horizontal") < 0)
            {
                movement.goLeft();
            }
            //else if (Input.GetKey(KeyCode.RightArrow))
            else if (Input.GetAxis("Horizontal") > 0)
            {
                movement.goRight();
            }
            else
            {
                movement.stop();
            }
            if (Input.GetAxis("Fire3") > 0) 
            {
                movement.run();
            }
            if (Input.GetKeyDown(KeyCode.Space)) {
            //if (Input.GetAxis("Submit") > 0) {
                if (canDropSpirit()) {
                    dropSpirit();
                }
            }
    }

    public bool canDropSpirit() {
        return (spiritCount > 0 && carryingSpirits.Count > 0 && isInGoodSoil());
    }

    public bool isInGoodSoil() {
        return (transform.position.x <= 15.0f );
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
