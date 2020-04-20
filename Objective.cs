using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour {

	// Use this for initialization
	public GameObject firstLuciole ;
	public GameManager _gameManager;
	void Start () {
		_gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.tag == "Player" && _gameManager.stepGame == 0) {
			firstLuciole.SetActive(true);
		}
	}

}
