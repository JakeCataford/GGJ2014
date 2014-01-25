using UnityEngine;
using System.Collections;

public class ParalaxingObject : MonoBehaviour {
	
	public float factor = 0.9f;
	private Vector3 startingPosition;
	
	void Start() {
		startingPosition = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(Camera.main.transform.position.x - ((Camera.main.transform.position.x - startingPosition.x) * factor), transform.position.y, transform.position.z); 
	}
}
