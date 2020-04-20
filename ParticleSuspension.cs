using UnityEngine;
using System.Collections;

public class ParticleSuspension : MonoBehaviour {

	public float vibrationValue = 0.1f; 

	public Vector3 initialPosition ;

	public bool isSand; 

	public void Start () {
		this.initialPosition = this.transform.position;
	}

	void Update () {
		float translateX = Random.Range(-this.vibrationValue, this.vibrationValue); 
		float translateY =  Random.Range(-this.vibrationValue, this.vibrationValue); 
		if ((transform.position - initialPosition).magnitude > 0.2f) {
			translateX *= -1.0f * Mathf.Sign(transform.position.x - initialPosition.x);
			translateY *= -1.0f * Mathf.Sign(transform.position.y - initialPosition.y);
		}
		this.transform.Translate(new Vector3(translateX * Time.deltaTime, translateY * Time.deltaTime, 0)); 
	}
}
