using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Objective : MonoBehaviour {

	// Use this for initialization
	public GameObject firstLuciole ;
	public PickUpSpirit firstLucioleSpirit ;
	public GameManager _gameManager;
	public GameObject UIBottomMessage;
	public Text _text;

	public string messageHelp1;
	public string messageHelp2;
	void Start () {
		_gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
		UIBottomMessage = GameObject.FindGameObjectWithTag("BottomMessage");
		_text = UIBottomMessage.GetComponent<Text>();
		firstLucioleSpirit = firstLuciole.GetComponent<PickUpSpirit>();
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.tag == "Player" && _gameManager.stepGame == 0) {
			firstLuciole.SetActive(true);
			_text.enabled = true;
		}
	}

	void OnTriggerExit2D(Collider2D collision) {
		if (collision.gameObject.tag == "Player") {
			_text.enabled = false;
		}
	}

	void Update() {
		if (firstLucioleSpirit != null && firstLucioleSpirit.following) {
			_text.text = messageHelp2;
			_text.enabled = true;
		} 	
		}

}
