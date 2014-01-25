using UnityEngine;
using System.Collections;

public class RotateAtSpeed : MonoBehaviour {

	public float speed = 0.1f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (Vector3.forward, speed);
	}
}