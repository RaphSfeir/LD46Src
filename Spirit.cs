using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spirit : MonoBehaviour {

	// Use this for initialization

	public SpriteRenderer sprite;
	void Start () {
		this.sprite = this.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		/* this.sprite.color = new Color(1, 1, 1, Mathf.Cos(Time.deltaTime));
		Debug.Log((Time.deltaTime % 180));
		Debug.Log(Mathf.Cos(Time.deltaTime % 180));*/
	}
}
