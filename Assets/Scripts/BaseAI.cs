using UnityEngine;
using System.Collections.Generic;

public class BaseAI : GameBehaviour {
	public enum State { IDLE, ENGAGED, DEAD };
	
	//Interface Methods
	protected virtual void OnSpawn() {}
	protected virtual void OnSight() {}
	protected virtual void InSight() {}
	protected virtual void OnLoseSight() {}
	protected virtual void OnStartIdle() {}
	protected virtual void Idle() {}
	protected virtual void OnDamage(Vector2 source) {}
	protected virtual void OnDie(Vector2 damageSource) {}
	protected virtual void Always() {}
	protected virtual void OnTouchPlayer(GameObject player) {}
	
	public State currentState = State.IDLE;
	public float visionRange = 2f;
	public GameObject targetPlayer;
	public bool DebugAgent = false;
	
	void Start() {
		OnSpawn();
	}
	
	void Update() {
		List<GameObject> playersInSight = PlayersInSight();
		if (playersInSight.Count > 0) {
			targetPlayer = playersInSight[Mathf.FloorToInt(Random.value * playersInSight.Count)];

			if (currentState == State.IDLE) {
					currentState = State.ENGAGED;
					OnSight();
			} else if (currentState == State.ENGAGED) {
					InSight();
			}
		} else {
			if (currentState == State.ENGAGED) {
				currentState = State.IDLE;
				OnLoseSight();
				OnStartIdle();
			} else {
				Idle();
			}
		}
		
		Always();
	}
	
	void OnDrawGizmos() {
		if (DebugAgent) {
			if (currentState == State.IDLE) {
				Gizmos.color = Color.green;
			}
			
			if (currentState == State.ENGAGED) {
				Gizmos.color = Color.red;
			}
			
			if (currentState == State.DEAD) {
				Gizmos.color = Color.gray;
			}
			
			Gizmos.DrawWireCube(transform.position, Vector3.one);
			Gizmos.DrawWireSphere(transform.position, visionRange);
			if (currentState == State.ENGAGED) {
				RaycastHit2D hit = Physics2D.Raycast(this.transform.position, targetPlayer.transform.position - transform.position);
				if (hit) {
					Gizmos.DrawLine(this.transform.position, hit.point);
				}
			}
		}
	}
	
	void OnCollisionEnter2D(Collision2D col) {
		if (col.collider.tag == "Player") {
			OnTouchPlayer(col.collider.gameObject);
		}
	}
	
	private List<GameObject> PlayersInSight() {
		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, visionRange);
		List<GameObject> playersInSight = new List<GameObject> ();
		foreach (Collider2D collider in colliders) {
			RaycastHit2D hit = Physics2D.Linecast(transform.position, collider.transform.position, ~1 << LayerMask.NameToLayer("Default"));
			if(hit && hit.collider.tag == "Player") {
				playersInSight.Add(collider.gameObject);
			}
		}
		return playersInSight;
	}
}
