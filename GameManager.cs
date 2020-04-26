using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public int factoryCount;

	public int spiritSpawnDelayMax;
	public int spiritSpawnDelay;

	public GameObject pickUpLuciole ;
	public GameObject mainObjective;
	public GameObject mainCamera;
	public PlayerController playerControls;
	public Text _UIText;
	public GameObject UIBottomMessage;

	public int stepGame;
	public int stepScene;
	public int maxSpiritsCount; 

	// Use this for initialization
	void Start () {
		factoryCount = 0;
		stepGame = 0;
		spiritSpawnDelay = spiritSpawnDelayMax;
		UIBottomMessage = GameObject.FindGameObjectWithTag("BottomMessage");
		_UIText = UIBottomMessage.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		if (stepGame >= 1) { 
			if (spiritSpawnDelay <= 0 && GameObject.FindGameObjectsWithTag("PickUpSpirit").Length <= maxSpiritsCount && factoryCount <= 19) {
				spawnSpirit();
			} else {
				spiritSpawnDelay--;
			}
		}
		if (factoryCount >= 1 && stepGame == 0) {
			stepGame++;
		}
		if (factoryCount >= 2 && stepGame == 1) {
			cutscene1();
			stepGame++;
		}
		if (factoryCount >= 5 && stepGame == 2) {
			stepGame++;
		}
		if (factoryCount >= 9 && stepGame == 3) {
			stepGame++;
			stepGame++;
		}
		factoryCount = GameObject.FindGameObjectsWithTag("Factory").Length;
	}

	void cutscene1() {
		mainCamera.GetComponent<SmoothCameraFollow>().target = GameObject.FindGameObjectWithTag("DemonObjective").transform;
		_UIText.enabled = true;
		_UIText.text = mainObjective.GetComponent<Objective>().messageHelp1;
		playerControls.movement.stop();
		playerControls.cutscene = true;
	}

	public void nextStepCustscene() {
		if (stepScene == 0) {
			_UIText.text = mainObjective.GetComponent<Objective>().messageHelp3;
			stepScene++;
		} else if (stepScene == 1) {
			_UIText.text = mainObjective.GetComponent<Objective>().messageHelp4;
			stepScene++;
		} else {
			stopCutscene1();
			stepScene++;
		}
	}

	public void stopCutscene1() {
		mainCamera.GetComponent<SmoothCameraFollow>().target = playerControls.gameObject.transform;
		_UIText.enabled = true;
		_UIText.text = "";
		playerControls.cutscene = false;
	}

	void spawnSpirit() {
		Instantiate(pickUpLuciole, new Vector3(mainObjective.transform.position.x + Random.Range(-3.0f, 7.0f), 2.0f, 0), transform.rotation);
		spiritSpawnDelay = spiritSpawnDelayMax;
	}
	

	public GameObject getPlayer() {
		return GameObject.FindGameObjectWithTag("Player");
	}
}
