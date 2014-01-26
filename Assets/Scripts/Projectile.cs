using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Projectile : GameBehaviour {

	public Vector2 direction = Vector2.right;
	public int baseDamage = 20;
	public int maxDamage = 10;
	public int minDamage = 4;
	public bool friendly = true;
	public float speed = 10f; 

	private int damage;

	void Start () {
		if (friendly) {
			gameObject.layer = LayerMask.NameToLayer("Player");
		} else {
			gameObject.layer = LayerMask.NameToLayer("Enemy");
		}

		damage = Mathf.FloorToInt(baseDamage / Game.playerSeparation);
		if (damage < minDamage) {
			damage = minDamage;
		} else if (damage > maxDamage) {
			damage = maxDamage;
		}

		rigidbody2D.gravityScale = 0.0f;
		rigidbody2D.drag = 0.0f;
	}

	void FixedUpdate() {
		rigidbody2D.velocity = direction * speed;
	}
	
	void OnCollisionEnter2D(Collision2D col) {
		if (col.collider.tag != "Player") {
			if (col.collider.tag == "Enemy") {
				col.collider.gameObject.GetComponent<Enemy>().health -= damage;
			}
			Destroy (gameObject);
		}
	}
}
