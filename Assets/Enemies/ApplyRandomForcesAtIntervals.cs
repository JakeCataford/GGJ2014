using UnityEngine;
using System.Collections;

public class ApplyRandomForcesAtIntervals : Enemy {

	void Start () {
		StartCoroutine (RandomForceTime ());
	}
	
	IEnumerator RandomForceTime() {
		rigidbody2D.AddForce (Random.insideUnitCircle.normalized * 1000f);
		yield return new WaitForSeconds(Random.value);
		StartCoroutine (RandomForceTime ());
	}
}
