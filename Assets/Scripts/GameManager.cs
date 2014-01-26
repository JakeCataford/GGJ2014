using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager> {

	const float turd = 30f;

	public Room[][] rooms;
	public Enemy[] enemies;
	public GameObject[] players;
	public float playerSeparation = 0;
	public int health = 100;

	protected GameManager() {}

	private Vector2 p1Pos;
	private Vector2 p2Pos;

	public void Start() {
		int playerLayer = LayerMask.NameToLayer("Player");
		Physics2D.IgnoreLayerCollision(playerLayer, playerLayer);

		int enemyLayer = LayerMask.NameToLayer("Enemy");
		Physics2D.IgnoreLayerCollision(enemyLayer, enemyLayer);
	}

	public void Update() {
		if (playerSeparation > turd) {
			health -= 1;
		}

		players = GameObject.FindGameObjectsWithTag("Player");
		if (players.Length > 1) {
			p1Pos = players[0].transform.position;
			p2Pos = players[1].transform.position;
			playerSeparation = Mathf.Abs(p1Pos.x - p2Pos.x) + Mathf.Abs(p1Pos.y - p2Pos.y);
		}
	}

	public void SpawnAllEnemies() {
		foreach (Room[] roomes in rooms) {
			foreach (Room room in roomes) {
				//dont....
			}
		}
	}
}
