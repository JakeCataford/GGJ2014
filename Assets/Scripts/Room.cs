using UnityEngine;
using System.Collections;

public class Room : GameBehaviour {

	public Transform Start;
	public Transform End;

	public Transform[] spawnPoint;


	public void SpawnEnemies() {
		GameObject go = (GameObject) Instantiate (Game.enemies [Mathf.FloorToInt (Random.value * Game.enemies.Length)].gameObject);
		go.transform.position = transform.position;
	}

	public void PositionStartPointAt(Vector3 pos) {
		transform.position = pos + (transform.position - transform.TransformPoint(Start.position));
	}
}
