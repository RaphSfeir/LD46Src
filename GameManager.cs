using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public int factoryCount;

	public int spiritSpawnDelayMax;
	public int spiritSpawnDelay;

	public GameObject pickUpLuciole ;
	public GameObject mainObjective;

	public int stepGame;

	// Use this for initialization
	void Start () {
		factoryCount = 0;
		stepGame = 0;
		spiritSpawnDelay = spiritSpawnDelayMax;
	}
	
	// Update is called once per frame
	void Update () {
		if (stepGame >= 1) { 
			if (spiritSpawnDelay <= 0 && GameObject.FindGameObjectsWithTag("PickUpSpirit").Length <= 5) {
				spawnSpirit();
			} else {
				spiritSpawnDelay--;
			}
		}
		if (factoryCount >= 1 && stepGame == 0) {
			stepGame++;
		}
		if (factoryCount >= 5 && stepGame == 1) {
			stepGame++;
		}
		factoryCount = GameObject.FindGameObjectsWithTag("Factory").Length;
	}

	void spawnSpirit() {
		Instantiate(pickUpLuciole, new Vector3(mainObjective.transform.position.x - Random.Range(-7.0f, 7.0f), 2.0f, 0), transform.rotation);
		spiritSpawnDelay = spiritSpawnDelayMax;
	}
	

	public GameObject getPlayer() {
		return GameObject.FindGameObjectWithTag("Player");
	}
}
