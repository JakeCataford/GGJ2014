using UnityEngine;
using System.Collections;

public class Enemy : GameBehaviour {

	protected BaseAI enemyAI;
	public int health = 20;
	public float baseAttackDamage = 0.5f;
	public int maxAttackDamage = 10;
	public int minAttackDamage = 2;
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
