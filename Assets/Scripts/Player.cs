using UnityEngine;
using System.Collections;

public class Player : GameBehaviour {
	public int health = 100;

	void Start () {
		int playerLayer = LayerMask.NameToLayer("Player");
		Physics2D.IgnoreLayerCollision(playerLayer, playerLayer);
	}
}