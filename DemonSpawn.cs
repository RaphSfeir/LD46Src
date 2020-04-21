using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonSpawn : MonoBehaviour {

	public GameManager _gameManager;
	public int demonPerfactory = 3;
	public int demonSpawnDelayMax;
	public int demonSpawnDelay;

	public GameObject demonObject;

	public bool demonSpawnBlocked = true;

	// Use this for initialization
	void Start () {
		_gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
		demonSpawnDelay = demonSpawnDelayMax;
	}
	
	// Update is called once per frame
	void Update () {
		if (!demonSpawnBlocked && _gameManager.stepGame >= 1) {
			if (demonSpawnDelay <= 0 ) {
				if (GameObject.FindGameObjectsWithTag("Demon").Length <= _gameManager.factoryCount * demonPerfactory) { 
					spawnWaveDemon();
					demonSpawnBlocked = true;
				}
			} else {
				demonSpawnDelay--;
			}
		} else {
			demonSpawnBlocked = GameObject.FindGameObjectsWithTag("Demon").Length >= 2 || _gameManager.stepGame < 1;
		}
	}

	void spawnWaveDemon() {
		for (int i = 0; (i <= _gameManager.factoryCount * demonPerfactory + 2 * _gameManager.stepGame); i++)  {
			Instantiate(demonObject, new Vector3(this.transform.position.x - Random.Range(-2.0f, 2.0f), -0.94f, 0), this.transform.rotation);
		}
		demonSpawnDelay = demonSpawnDelayMax;
	}
}
