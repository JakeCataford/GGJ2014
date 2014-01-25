﻿using UnityEngine;
using System.Collections;

public class Enemy : GameBehaviour {

	protected BaseAI enemyAI;
	public int health;
	public int attackDamage;
	public float movementSpeed;

	protected bool isVisible;

	void OnBecameVisible() {
		isVisible = true;
	}

	void OnBecameInvisible() {
		isVisible = false;
	}
}
