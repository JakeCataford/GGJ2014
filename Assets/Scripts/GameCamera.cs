using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {

	// Assignable Target, Leave null to user player tag...
	public Transform target;

	//Tweakables
	public float movementLagFactor = 0.1f;
	public float maximumZoom = 1.1f;
	public float zoomSpeedThreshold = 0.1f;

	public void Start() {
		if(target == null) {
			try {
				target = GameObject.FindWithTag("Player").transform;
			} catch {
				Debug.LogError("Nothing in this scene has a player tag, and the target of the camera has not been assigned");
			}
		}
	}

	public void FixedUpdate() {
		//Quadratic interpolation of camera position.
		transform.position = Vector3.Lerp(transform.position, target.position, movementLagFactor);
		transform.position = new Vector3 (transform.position.x, transform.position.y, -10);
	}

	public void OnDrawGizmos() {
	}
}
