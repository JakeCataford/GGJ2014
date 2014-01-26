using UnityEngine;
using System.Collections;

public class Enemy : GameBehaviour {

	protected BaseAI enemyAI;
	public int health = 20;
	public float baseAttackDamage = 0.5f;
	public int maxAttackDamage = 10;
	public int minAttackDamage = 2;
	public float movementSpeed = 10f;

	protected bool isVisible;

	void Start() {
		tag = "Enemy";
		gameObject.layer = LayerMask.NameToLayer("Enemy");
	}

	void Update() {
		if (health < 0) {
			GameObject go = (GameObject)Instantiate (Game.enemyDeathPs.gameObject);
			go.transform.position = transform.position;
			go.particleSystem.Emit(40);
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

	void OnBecameVisible() {
		isVisible = true;
	}

	void OnBecameInvisible() {
		isVisible = false;
	}
}
