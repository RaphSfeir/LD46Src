using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour {

    private Animator animator;
	public bool titleScreenDone = false; 
	
	public void Start() {
		animator = GetComponent<Animator>();
	}

	void OnTriggerEnter2D(Collider2D collision) {
		Debug.Log("ENTER TITLE SCREEN !!");
		if (collision.gameObject.tag == "Player" && !titleScreenDone) {
	        animator.SetBool("appear", true);
		}
	}
	void endFadeIn() {
		Debug.Log("end title screen");
		titleScreenDone = true;
        animator.SetBool("appear", false);
		Destroy(gameObject);
	}
}
