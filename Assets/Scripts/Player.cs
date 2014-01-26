using UnityEngine;
using System.Collections;

public class Player : GameBehaviour {

	void Start () {
		int playerLayer = LayerMask.NameToLayer("Player");
		Physics2D.IgnoreLayerCollision(playerLayer, playerLayer);
	}
}