using UnityEngine;
using System.Collections;
using CollisionExtentions;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Patroller : Enemy {

	public bool flipped = false;
	public float patrolSpeed = 1.0f;
	public bool collidedWithWall = false;

	public float timeOut = 0;

	void Start() {

		rigidbody2D.fixedAngle = true;
	}

	void FixedUpdate() {
		if (isVisible) {
			Vector2 vel = rigidbody2D.velocity;
			vel.x = transform.localScale.x * patrolSpeed;
			rigidbody2D.velocity = vel;
			RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 2.1f, ~ (1 << LayerMask.NameToLayer("Enemy")));
			if(collidedWithWall || hit && timeOut < 0) {
				Flip();
				timeOut = 1f;
			}
			collidedWithWall = false;
			timeOut -= Time.deltaTime;
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

		if (Vector2.Dot (normal, Vector2.right) > 0.5f) {
			collidedWithWall = true;
		}
	}
}
