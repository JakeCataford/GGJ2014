using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {
	public RenderTexture texture;
	// Assignable Target, Leave null to user player tag...
	public Transform target;

	//Tweakables
	public float movementLagFactor = 0.1f;

	public RenderTexture rt;


	public void Start() {

		rt = new RenderTexture (300,(int) (300 * camera.aspect), -1);
		rt.antiAliasing = 1;
		rt.anisoLevel = 1;
		rt.filterMode = FilterMode.Point;
		camera.targetTexture = rt;

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
		if (target) {
			transform.position = Vector3.Lerp (transform.position, target.position, movementLagFactor);
			transform.position = new Vector3 (transform.position.x, transform.position.y, -10);
		}

		GetComponent<TwirlEffect> ().angle = Mathf.Lerp (GetComponent<TwirlEffect> ().angle, 0, 0.03f);

	}

	public void DoDamage() {
		GetComponent<TwirlEffect> ().angle = 5f;
	}

	public void OnGUI() {
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), rt);
	}
}
