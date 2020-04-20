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
		if (collision.gameObject.tag == "Player" && !titleScreenDone) {
	        animator.SetBool("appear", true);
		}
	}
	void endFadeIn() {
		titleScreenDone = true;
        animator.SetBool("appear", false);
		Destroy(gameObject);
	}
}
