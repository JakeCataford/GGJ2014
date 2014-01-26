using UnityEngine;
using System.Collections;

public class Door : GameBehaviour {

	public string transferScreen;

	void OnTriggerStay2D() {
		if (Input.GetAxis ("Vertical") > 0.5f) {
			if(transferScreen != null) {
				StartCoroutine(Exit ());
			}
		}
	}

	IEnumerator Exit() {
		Game.DoDamageFX();
		yield return new WaitForSeconds (0.2f);
		Application.LoadLevel (transferScreen);
	}
}
