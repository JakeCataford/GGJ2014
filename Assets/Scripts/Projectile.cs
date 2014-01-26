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
			tag = "Player";
			gameObject.layer = LayerMask.NameToLayer("Player");
		} else {
			tag = "Enemy";
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
		if (col.collider.tag == "Enemy" && friendly) {
			col.collider.GetComponent<Enemy> ().health -= damage;
		} else if( col.collider.tag == "Player" && !friendly) {
			Game.health -= damage;
		}

		GameObject go = (GameObject)Instantiate (Game.particleDeathPs.gameObject);
		go.transform.position = transform.position;
		go.particleSystem.Emit(10);
		Destroy (gameObject);
	}
}
