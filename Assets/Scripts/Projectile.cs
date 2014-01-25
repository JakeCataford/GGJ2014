﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Projectile : GameBehaviour {

	public Vector2 direction = Vector2.right;
	public int damage = 5;
	public bool friendly = true;

	void Start () {
		rigidbody2D.gravityScale = 0.0f;
		rigidbody2D.drag = 0.0f;
		rigidbody2D.velocity = direction;
	}
	
	void OnCollisionEnter2D(Collision2D col) {
		if (!friendly || col.collider.tag != "Player") {
			//Deal damage?
			Destroy (gameObject);
		}
	}
}