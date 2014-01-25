using UnityEngine;
using System.Collections;
using CollisionExtentions;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(WeaponController))]
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
	public float jumpSpeed = 5f;
	public float boostSpeed = 10f;

	public bool hasPowerBooster = false;
	public bool hasPowerWallHang = false;

	public bool grounded = false;
	public bool leftCollision = false;
	private bool rightCollision = false;
	private bool powerBoosterUsed = false;
	private bool wallHanging = false;
	private Vector3 wallHangingPosition;

	private Vector2 debugLastNormal = new Vector2 (0, 1);

	private Animator animator;


	void Start() {
		rigidbody2D.fixedAngle = true;
		animator = GetComponent<Animator>();
	}

	void Update()
	{
		//grounded = Physics2D.Raycast(transform.position, -Vector2.up, ((BoxCollider2D)collider2D).size.y / 1.9f, 1 << LayerMask.NameToLayer("World"));
		//leftCollision = Physics2D.Raycast(transform.position, -Vector2.right, ((BoxCollider2D)collider2D).size.y / 1.9f, 1 << LayerMask.NameToLayer("World"));
		//rightCollision = Physics2D.Raycast(transform.position, Vector2.right, ((BoxCollider2D)collider2D).size.y / 1.9f, 1 << LayerMask.NameToLayer("World"));

		if (grounded || wallHanging) {
			powerBoosterUsed = false;
		}

		if ((leftCollision || rightCollision) && hasPowerWallHang && grounded == false && wallHanging == false) {
			wallHangingPosition = rigidbody2D.transform.position;
			rigidbody2D.gravityScale = 0;
			wallHanging = true;
		}

		if(Input.GetButtonDown("Jump") && (grounded || wallHanging)) {
			Debug.Log("Hop.");
			jump = true;
		} else if (Input.GetButton("Fire2") && hasPowerBooster && powerBoosterUsed == false && wallHanging == false) {
			boost = true;
		}
	}

	void OnCollisionStay2D(Collision2D col) {
		Vector2 collisionAverageNormal = col.contacts.AverageNormal ();
		debugLastNormal = collisionAverageNormal;
		if (Vector2.Dot (collisionAverageNormal, Vector2.up) > 0.9f) {
			grounded = true;
		}

		if (Vector2.Dot (collisionAverageNormal, -Vector2.right) > 0.9f) {
			leftCollision = true;
		} else if (Vector2.Dot (collisionAverageNormal, Vector2.right) > 0.9f) {
			rightCollision = true;
		}
	}

	
	
	void FixedUpdate ()
	{
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis ("Vertical");

//		Horizontal movement
		if (wallHanging) {
			rigidbody2D.transform.position = wallHangingPosition;
		} else {
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

		animator.SetFloat("Horizontal Speed", Mathf.Abs(rigidbody2D.velocity.x));
		animator.SetBool ("Grounded", grounded);

		grounded = false;
		leftCollision = false;
		rightCollision = false;
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
		if (wallHanging) {
			float x = leftCollision ? 1 : -1;
			rigidbody2D.velocity = new Vector2(x, 1).normalized * jumpSpeed;
			rigidbody2D.gravityScale = 1;
			wallHanging = false;
		} else {
			rigidbody2D.velocity = new Vector2(0f, jumpSpeed);
		}
			
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
		
//		rigidbody2D.AddForce(new Vector2(boostX, boostY).normalized * boostSpeed);
		rigidbody2D.velocity = new Vector2(boostX, boostY).normalized * boostSpeed;
		boost = false;
		powerBoosterUsed = true;
	}

	void OnDrawGizmos() {
		Gizmos.DrawRay (transform.position, debugLastNormal);
	}
}
