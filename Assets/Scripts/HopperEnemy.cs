using UnityEngine;
using System.Collections;
using CollisionExtentions;

public class HopperEnemy : Enemy {

	private bool grounded;
	private Vector2 debugLastNormal = new Vector2(0, 1);

	void Start () {
		rigidbody2D.fixedAngle = true;
		enemyAI = GetComponent<BaseAI>();
		if (!enemyAI) {
			enemyAI = gameObject.AddComponent<BaseAI>();
		}
		health = 10;
		attackDamage = 10;
		movementSpeed = 10f;
	}
	
	void Update () {
		if (isVisible && enemyAI.currentState == BaseAI.State.ENGAGED) {
			attackPlayer();
		}
		grounded = false;
	}

	void OnCollisionStay2D(Collision2D col) {
		if (col.collider.tag != "Player") {
			Vector2 collisionAverageNormal = col.contacts.AverageNormal ();
			debugLastNormal = collisionAverageNormal;
			if (Vector2.Dot (collisionAverageNormal, Vector2.up) > 0.9f) {
				grounded = true;
			}
		}
	}

	void attackPlayer() {
		if (grounded) {
			rigidbody2D.velocity = new Vector2(Mathf.Sign(enemyAI.targetPlayer.transform.position.x - transform.position.x) * movementSpeed, movementSpeed);
		}
	}

	void onTouchPlayer(GameObject go) {

	}
}
