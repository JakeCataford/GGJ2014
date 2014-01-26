using UnityEngine;
using System.Collections;

public class Enemy : GameBehaviour {

	protected BaseAI enemyAI;
	public int health = 20;
	public int attackDamage = 5;
	public float movementSpeed = 10f;

	protected bool isVisible;

	void Start() {
		tag = "Enemy";
		gameObject.layer = LayerMask.NameToLayer("Enemy");
	}

	void OnBecameVisible() {
		isVisible = true;
	}

	void OnBecameInvisible() {
		isVisible = false;
	}
}
