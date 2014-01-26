using UnityEngine;
using System.Collections;

public class RotateAtSpeed : MonoBehaviour {

	public float x = 0.02f;
	public float y = 0.023f;
	public float z = 0.01f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (Vector3.right, x);
		transform.Rotate (Vector3.up, y);
		transform.Rotate (Vector3.forward, z);
	}
}