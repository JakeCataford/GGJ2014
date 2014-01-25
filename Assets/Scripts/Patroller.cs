using UnityEngine;
using System.Collections;
using CollisionExtentions;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Patroller : Enemy {

	public bool flipped = false;
	public float patrolSpeed = 1.0f;
	public bool collidedWithWall = false;

	void Start() {
		rigidbody2D.fixedAngle = true;
	}

	void FixedUpdate() {
		if (isVisible) {
			Vector2 vel = rigidbody2D.velocity;
			vel.x = (Vector2.right * transform.localScale.x) * patrolSpeed;
			rigidbody2D.velocity = vel;
			RaycastHit2D hit = Physics2D.Raycast((Vector2) transform.position + (Vector2.right * 1.2f * transform.localScale.x), Vector2.up, 1.0f);
			if(collidedWithWall || hit) {
				Flip();
			}
			collidedWithWall = false;
		}
	}

	void Flip() {
		transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);
		flipped = !flipped;
	}

	void OnCollisionStay2D(Collision2D col) {
		Vector2 normal = col.contacts.AverageNormal ();
		//Debug.Log (normal);
		//Debug.Log (Vector2.Dot (normal, Vector2.right));

		if (Vector2.Dot (normal, Vector2.right) > 0.9f) {
			collidedWithWall = true;
		}
	}
}
