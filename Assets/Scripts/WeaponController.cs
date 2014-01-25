using UnityEngine;
using System.Collections;

public class WeaponController : GameBehaviour {
	public Projectile bullet;

	public void Update() {
		bool isVerticalShot = false;
		Vector2 shotDirection;
		if (Input.GetAxis ("Vertical") > 0.1f) {
			shotDirection = Vector2.up;
			isVerticalShot = true;
		} else if (Input.GetAxis ("Vertical") < -0.1f) {
			shotDirection = -Vector2.up;
			isVerticalShot = true;
		} else {
			shotDirection = (Vector2) transform.right;
		}

		if (Input.GetButtonDown ("Fire1")) {
			bullet.direction = shotDirection;
			GameObject go = (GameObject) Instantiate (bullet.gameObject);
			if (isVerticalShot) {
					go.transform.position = new Vector2 (transform.position.x, 
	                                    				transform.position.y + 
														Mathf.Sign (shotDirection.y) *
				                                     	(((BoxCollider2D)collider2D).size.y + 
														(((CircleCollider2D)bullet.collider2D).radius * 
														bullet.transform.localScale.x)));
			} else {
					go.transform.position = new Vector2 (transform.position.x + 
														(((BoxCollider2D)collider2D).size.x + 
														(((CircleCollider2D)bullet.collider2D).radius * 
														bullet.transform.localScale.x)), 
	                                     				transform.position.y);
			}
		}
	}





}
