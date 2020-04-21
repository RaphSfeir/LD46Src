using UnityEngine;
using System.Collections;

public class ParallaxBackground : MonoBehaviour {

	public Vector3 initialPosition; 

	public float offset = 5f; 
	public float xRectify = 0; 
	public float limitXMin = -10 ; 
	public float limitXMax = 10 ; 
	public float widthRepeat ; 

	public bool followingCamera;
	public Transform camera; 

	void Start() {
		this.initialPosition = camera.position; 
	}


	// Update is called once per frame
	void Update () {
		if (this.followingCamera) {
			this.transform.position = new Vector3((this.camera.transform.position.x - initialPosition.x )/ this.offset + this.xRectify, this.transform.position.y, this.transform.position.z); 
		}
		else {this.transform.position = new Vector3(-(this.camera.transform.position.x - initialPosition.x )/ this.offset + this.xRectify, this.transform.position.y, this.transform.position.z); 

		}
		/*if (this.transform.localPosition.x < limitXMin) {
			Debug.Log ("LOCK 1 "); 
			Debug.Log (this.transform.localPosition.x); 
			this.transform.position = new Vector3 (this.transform.position.x + this.widthRepeat, this.transform.position.y, this.transform.position.z); 
		}

		if (this.transform.localPosition.x > limitXMax) {
			Debug.Log ("LOCK 2 "); 
			Debug.Log (this.transform.localPosition.x); 
			this.transform.position = new Vector3 (this.transform.position.x - this.widthRepeat, this.transform.position.y, this.transform.position.z); 
		}*/
	}
}
