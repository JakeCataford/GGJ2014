﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovement : MonoBehaviour {

	[HideInInspector]
	public bool facingRight = true;			// For determining which way the player is currently facing.
	[HideInInspector]
	public bool jump = false;				// Condition for whether the player should jump.
	
	public float moveDeceleration = 100f;	// Amount of force added to move the player left and right.
	public float maxSpeed = 0.05f;			// The fastest the player can travel in the x axis.
	public float jumpForce = 250f;			// Amount of force added when the player jumps.
	
	private bool grounded = false;			// Whether or not the player is grounded.
	private bool leftCollision = false;
	private bool rightCollision = false;
	
	void Update()
	{
		// The player is grounded if a raycast from the transform position collides with anything
		grounded = Physics2D.Raycast(transform.position, -Vector2.up, ((BoxCollider2D)collider2D).size.y / 1.5f, 1 << LayerMask.NameToLayer("World"));
		leftCollision = Physics2D.Raycast(transform.position, -Vector2.right, ((BoxCollider2D)collider2D).size.y / 1.5f, 1 << LayerMask.NameToLayer("World"));
		rightCollision = Physics2D.Raycast(transform.position, -Vector2.right, ((BoxCollider2D)collider2D).size.y / 1.5f, 1 << LayerMask.NameToLayer("World"));

		// If the jump button is pressed and the player is grounded then the player should jump.
		if(Input.GetButtonDown("Jump") && grounded)
			jump = true;
	}
	
	
	void FixedUpdate ()
	{
		// Cache the horizontal input.
		float h = Input.GetAxis("Horizontal");
		
		// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
		if(h * rigidbody2D.velocity.x < maxSpeed)
			// ... add a force to the player.
			rigidbody2D.AddForce(Vector2.right * h * moveForce);
		
		// If the player's horizontal velocity is greater than the maxSpeed...
		if(Mathf.Abs(rigidbody2D.velocity.x) > maxSpeed)
			// ... set the player's velocity to the maxSpeed in the x axis.
			rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);
		
		// If the input is moving the player right and the player is facing left...
		if(h > 0 && !facingRight)
			// ... flip the player.
			Flip();
		// Otherwise if the input is moving the player left and the player is facing right...
		else if(h < 0 && facingRight)
			// ... flip the player.
			Flip();
		
		// If the player should jump...
		if(jump)
		{
			// Add a vertical force to the player.
			rigidbody2D.AddForce(new Vector2(0f, jumpForce));
			
			// Make sure the player can't jump again until the jump conditions from Update are satisfied.
			jump = false;
		}
	}
	
	
	void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

}
