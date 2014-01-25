using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovement : MonoBehaviour {

	[HideInInspector]
	public bool facingRight = true;
	[HideInInspector]
	public bool jump = false;
	[HideInInspector]
	public bool boost = false;
	[HideInInspector]
	public bool hang = false;

	public float boostDecelerationLerpTime = 0.02f;
	public float moveDecelerationLerpTime = 1f;	
	public float playerSpeed = 2f;
	public float jumpForce = 250f;
	public float boostForce = 500f;

	public bool hasPowerBooster = false;
	public bool hasPowerWallClimb = false;

	private bool grounded = false;
	private bool leftCollision = false;
	private bool rightCollision = false;
	private bool powerBoosterUsed = false;

	void Update()
	{
		grounded = Physics2D.Raycast(transform.position, -Vector2.up, ((BoxCollider2D)collider2D).size.y / 1.9f, 1 << LayerMask.NameToLayer("World"));
		leftCollision = Physics2D.Raycast(transform.position, -Vector2.right, ((BoxCollider2D)collider2D).size.y / 1.9f, 1 << LayerMask.NameToLayer("World"));
		rightCollision = Physics2D.Raycast(transform.position, Vector2.right, ((BoxCollider2D)collider2D).size.y / 1.9f, 1 << LayerMask.NameToLayer("World"));

		if (grounded) {
			powerBoosterUsed = false;
		}

		if(Input.GetButtonDown("Jump") && grounded) {
			jump = true;
		} else if (Input.GetButton("Fire2") && hasPowerBooster && powerBoosterUsed == false) {
				boost = true;
		}
	}
	
	
	void FixedUpdate ()
	{
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis ("Vertical");

//		Horizontal movement
		if (powerBoosterUsed) {
			rigidbody2D.velocity = new Vector2(Mathf.Lerp(rigidbody2D.velocity.x, 0f, boostDecelerationLerpTime), rigidbody2D.velocity.y);
		} else {
			if (h == 0) {
				rigidbody2D.velocity = new Vector2(Mathf.Lerp(rigidbody2D.velocity.x, 0f, moveDecelerationLerpTime), rigidbody2D.velocity.y);
			} else {
				if ((h > 0 && !rightCollision) || (h < 0 && !leftCollision)) {
					rigidbody2D.velocity = new Vector2(Mathf.Sign(h) * playerSpeed, rigidbody2D.velocity.y);
				} else {
					rigidbody2D.velocity = new Vector2(0f, rigidbody2D.velocity.y);
				}
			}
		}

		if (h > 0 && !facingRight) {
			Flip();
		} else if (h < 0 && facingRight) {
			Flip();
		}
		
		if (jump) {
			Jump ();
		}

		if (boost) {
			Boost (h, v);
		}
	}
	
	
	void Flip ()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void Jump ()
	{
		rigidbody2D.AddForce(new Vector2(0f, jumpForce));			
		jump = false;
	}

	void Boost (float xInput, float yInput)
	{
		float boostX = 0, boostY = 0;

		if (yInput == 0 && xInput == 0) {
			boostY = 1;
		} else {
			if (xInput != 0) {
				boostX = xInput > 0 ? 1 : -1;
			}
			if (yInput != 0) {
				boostY = yInput > 0 ? 1 : -1;
			}
		}
		
		rigidbody2D.AddForce(new Vector2(boostX, boostY).normalized * boostForce);
		boost = false;
		powerBoosterUsed = true;
	}
}
