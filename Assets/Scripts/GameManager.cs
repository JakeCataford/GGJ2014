using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager> {

	public Room[][] rooms;
	public Enemy[] enemies;

	protected GameManager() {}

	public void Start() {

	}

	public void SpawnAllEnemies() {
		foreach (Room[] roomes in rooms) {
			foreach (Room room in roomes) {
				if (room && !room.solid) {
					room.SpawnEnemies ();
				}
			}
		}
	}
}
