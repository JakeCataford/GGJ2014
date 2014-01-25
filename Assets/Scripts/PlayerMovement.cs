using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovement : MonoBehaviour {

	[HideInInspector]
	public bool facingRight = true;			// For determining which way the player is currently facing.
	[HideInInspector]
	public bool jump = false;				// Condition for whether the player should jump.
	[HideInInspector]
	public bool boost = false;
	[HideInInspector]
	public bool hang = false;
	
	public float moveDecelerationLerpTime = 1f;	// Amount of force added to move the player left and right.
	public float speed = 2f;				// The fastest the player can travel in the x axis.
	public float jumpForce = 250f;			// Amount of force added when the player jumps.

	public bool hasPowerBooster = false;
	public bool hasPowerWallClimb = false;

	private bool grounded = false;			// Whether or not the player is grounded.
	private bool leftCollision = false;
	private bool rightCollision = false;

	void Update()
	{
		grounded = Physics2D.Raycast(transform.position, -Vector2.up, ((BoxCollider2D)collider2D).size.y / 1.9f, 1 << LayerMask.NameToLayer("World"));
		leftCollision = Physics2D.Raycast(transform.position, -Vector2.right, ((BoxCollider2D)collider2D).size.y / 1.9f, 1 << LayerMask.NameToLayer("World"));
		rightCollision = Physics2D.Raycast(transform.position, Vector2.right, ((BoxCollider2D)collider2D).size.y / 1.9f, 1 << LayerMask.NameToLayer("World"));

		if(Input.GetButtonDown("Jump") && grounded) {
			jump = true;
		}

		if (Input.GetButton("Boost") && grounded) {
			boost = true;
		}
	}
	
	
	void FixedUpdate ()
	{
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis ("Vertical");
		
		if (h == 0) {
			rigidbody2D.velocity = new Vector2(Mathf.Lerp(rigidbody2D.velocity.x, 0f, moveDecelerationLerpTime), rigidbody2D.velocity.y);
		} else {
			if ((h > 0 && !rightCollision) || (h < 0 && !leftCollision)) {
				rigidbody2D.velocity = new Vector2(Mathf.Sign(h) * speed, rigidbody2D.velocity.y);
			} else {
				rigidbody2D.velocity = new Vector2(0f, rigidbody2D.velocity.y);
			}
		}
		
		if (h > 0 && !facingRight) {
			Flip();
		} else if (h < 0 && facingRight) {
			Flip();
		}
		
		if (jump) {
			rigidbody2D.AddForce(new Vector2(0f, jumpForce));			
			jump = false;
		}
	}
	
	
	void Flip ()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void Boost (Vector2 vector)
	{

	}
}
