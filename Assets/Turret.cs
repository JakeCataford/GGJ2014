using UnityEngine;
using System.Collections;

public class Turret : Enemy {

	public Transform targ;
	public Projectile bullet;

	private float reloadTimar = 0.0f;

	void Start() {
		//rigidbody2D.fixedAngle = true;
		enemyAI = GetComponent<BaseAI>();
		if (!enemyAI) {
			enemyAI = gameObject.AddComponent<BaseAI>();
		}
		enemyAI.visionRange = 20;
	}

	// Update is called once per frame
	void Update () {
		if (isVisible && enemyAI.currentState == BaseAI.State.ENGAGED) {
			if(!targ) {
				targ = Game.players[Mathf.FloorToInt(Random.value * Game.players.Length)].transform;
			}
			transform.right = Vector2.Lerp (transform.right, targ.position - transform.position, 0.1f);
			if(reloadTimar < 0.0f) {
				reloadTimar = 0.5f;
				Transform t = ((GameObject) Instantiate(bullet.gameObject)).transform;
				t.position = transform.position + (targ.position - transform.position).normalized * 1.0f;
				t.GetComponent<Projectile>().direction = (targ.position - transform.position);
			}

			reloadTimar -= Time.deltaTime;
		} else {
			targ = null;
		}
	}
}
