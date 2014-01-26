﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Projectile : GameBehaviour {

	public Vector2 direction = Vector2.right;
	public int damage = 5;
	public bool friendly = true;
	public float speed = 10f; 

	void Start () {
		if (friendly) {
			gameObject.layer = LayerMask.NameToLayer("Player");
		} else {
			gameObject.layer = LayerMask.NameToLayer("Enemy");
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
