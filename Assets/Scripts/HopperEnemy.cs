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
	}
	
	void Update () {
		if (health > 0) {
			if (isVisible && enemyAI.currentState == BaseAI.State.ENGAGED) {
				attackPlayer();
			}
			grounded = false;
		} else {
			Destroy(gameObject);
		}

	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.collider.tag == "Player" || col.collider.tag == "Projectile") {
			rigidbody2D.velocity = new Vector2(Mathf.Sign(transform.position.x - col.transform.position.x), 1).normalized * 5;
			if (col.collider.tag == "Player") {
				int damage = Mathf.FloorToInt(baseAttackDamage * Game.playerSeparation);
				if (damage > maxAttackDamage) {
					damage = maxAttackDamage;
				} else if (damage < minAttackDamage) {
					damage = minAttackDamage;
				}
				Game.health -= damage;
			}
		}
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
