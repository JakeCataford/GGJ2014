using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager> {

	const float turd = 30f;

	public Room[][] rooms;
	public Enemy[] enemies;
	public GameObject[] players;
	public float playerSeparation = 0;
	public int health = 100;

	public ParticleSystem enemyDeathPs;
	public ParticleSystem particleDeathPs;
	public ParticleSystem playerDeathPs;

	public Door[] doors;
	public string[] levelNames;


	protected GameManager() {}

	private Vector2 p1Pos;
	private Vector2 p2Pos;



	public void Start() {
		DontDestroyOnLoad(gameObject);
		if (doors.Length != levelNames.Length) {
			Debug.LogError("Doors needs to match levels for initial scene...");
		}

		List<int> consumedDoors = new List<int> ();
		List<int> consumedLevels = new List<int> ();

		for (int i = 0; i < 3; i++) {
			doors[i].transferScreen = levelNames[i];
		}

		int playerLayer = LayerMask.NameToLayer("Player");
		Physics2D.IgnoreLayerCollision(playerLayer, playerLayer);

		int enemyLayer = LayerMask.NameToLayer("Enemy");
		Physics2D.IgnoreLayerCollision(enemyLayer, enemyLayer);

		DoDamageFX ();
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

		if (health < 0) {
			foreach(GameObject player in players) {
				GameObject go = (GameObject)Instantiate (Game.playerDeathPs.gameObject);
				go.transform.position = player.transform.position;
				go.particleSystem.Emit(100);
				DoDamageFX();
				Destroy(player);
				StartCoroutine(Respawn());
				health = 100;
			}
		}
	}

	IEnumerator Respawn() {
		yield return new WaitForSeconds (5);
		Application.LoadLevel ("Entry Room");
	}

	public void DoDamageFX() {
		Camera.main.GetComponent<GameCamera> ().DoDamage ();

	}

	public void SpawnAllEnemies() {
		foreach (Room[] roomes in rooms) {
			foreach (Room room in roomes) {
				//dont....
			}
		}
	}

	public void OnGUI() {

	}
}
