using UnityEngine;
using System.Collections;

public class DestroyAfterSeconds : MonoBehaviour {

	public float seconds = 5.0f;

	void Start () {
		StartCoroutine (DoCountdown ());
	}

	IEnumerator DoCountdown() {
		yield return new WaitForSeconds(seconds);
		Destroy (gameObject);
	}
}
