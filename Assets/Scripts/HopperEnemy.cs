using UnityEngine;
using System.Collections;
using CollisionExtentions;

public class HopperEnemy : Enemy {

	private bool grounded;
	private Vector2 debugLastNormal;

	void Start () {
		enemyAI = new BaseAI();
		health = 10;
		attackDamage = 10;
		movementSpeed = 3f;
	}
	
	void Update () {
		if (isVisible && enemyAI.currentState == BaseAI.State.ENGAGED) {
			attackPlayer();
		}
	}

	void OnCollisionStay2D(Collision2D col) {
		Vector2 collisionAverageNormal = col.contacts.AverageNormal ();
		debugLastNormal = collisionAverageNormal;
		if (Vector2.Dot (collisionAverageNormal, Vector2.up) > 0.9f) {
			grounded = true;
		}
	}

	void attackPlayer() {
		if (grounded) {
			rigidbody2D.velocity = new Vector2(Mathf.Sign(transform.position.x - enemyAI.targetPlayer.transform.position.x) * movementSpeed, movementSpeed);
		}
	}

	void onTouchPlayer(GameObject go) {

	}
}
