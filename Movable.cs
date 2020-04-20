﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable : MonoBehaviour {

    public Rigidbody2D _rigibody ; 
    public FreeParallax parallax;
    private Vector3 initVelocity = Vector3.zero;
    public float parallaxSpeedFactor;
    public float movementSpeed ;
    public float vSmoothTime= 0.1F;
    public bool walking;
	public bool running; 
	public Vector3 targetVelocity = Vector2.zero; 
    private Animator animator;
	// Use this for initialization
	void Start () {
        walking = false;
		animator = GetComponent<Animator>();
		_rigibody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
            _rigibody.velocity = Vector3.SmoothDamp(_rigibody.velocity, targetVelocity, ref initVelocity, vSmoothTime);
            walking = Mathf.Abs(_rigibody.velocity.x) > 0.05f;
            //animator.SetBool("walking", walking);
			if (gameObject.tag == "Player") {
	            parallax.Speed = _rigibody.velocity.x * parallaxSpeedFactor * -1.0f;
			}
	}

	public void goRight() {
		targetVelocity = Vector2.right * movementSpeed;
 		transform.localScale = new Vector2(1, 1 );
	}

	public void goLeft(){
        targetVelocity = Vector2.left * movementSpeed;
		transform.localScale = new Vector2(-1, 1);
	}
	public void run() {
		running = true;
		targetVelocity = targetVelocity * 2.0f;
	}
	public void walk() {
		running = false;
		targetVelocity = targetVelocity / 2.0f; 
	}

	public void stop() {
		targetVelocity = Vector2.zero;
	}

}