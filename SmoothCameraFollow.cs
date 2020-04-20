using UnityEngine;
using System.Collections;

public class SmoothCameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;

    void Update()
    {
        // Define a target position above and behind the target transform
        //Vector3 targetPosition = target.TransformPoint(new Vector3(0, -0.26f, -10));

        // Smoothly move the camera towards that target position
        //transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

	void LateUpdate()
	{
		transform.position = Vector3.Lerp(
			new Vector3(transform.position.x, transform.position.y, -10f), 
			new Vector3(target.transform.position.x, transform.position.y, -10f), 
			3f * Time.deltaTime
			);
	}
}