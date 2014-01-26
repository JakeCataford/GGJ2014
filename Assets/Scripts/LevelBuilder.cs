using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class LevelBuilder : Singleton<LevelBuilder> {

	public enum Theme { TEST }

	//Size of area square lol.
	public static int NUMBER_OF_ROOMS = 10;

	public Vector2 spawnPoint = new Vector2(0,0);

	List<Room> RoomList = new List<Room>();

	public Room spawnRoom;
	public Room endRoom;

	public List<Room> RoomTemplates;
	protected LevelBuilder() {}

	public void Start() {
		BuildLevel (Theme.TEST);
	}

	public void BuildLevel(Theme theme) {
		if (theme == Theme.TEST) {
			//RoomList = RoomTemplates;
		}

		GenerateRooms();
		//Game.SpawnAllEnemies ();
		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");

		foreach (GameObject player in players) {
			player.transform.position = spawnPoint;
		}
	}

	private void GenerateRooms() {
		GameObject spawn = (GameObject)Instantiate (spawnRoom.gameObject);
		spawnRoom = spawnRoom.GetComponent<Room> ();

		for (int i = 0; i < NUMBER_OF_ROOMS; i++) { 
			GameObject go = (GameObject) Instantiate(RoomTemplates[Mathf.FloorToInt(UnityEngine.Random.value * RoomTemplates.Count)].gameObject);
			Room room = go.GetComponent<Room>();
			RoomList.Add (room);
			if(i == 0) {
				room.PositionStartPointAt(spawnRoom.End.position);
			} else {
				room.PositionStartPointAt(RoomList[i-1].End.position);
			}
		}

		GameObject end = (GameObject) Instantiate (endRoom.gameObject);
		endRoom = end.GetComponent<Room> ();
		endRoom.GetComponent<Room> ().PositionStartPointAt (RoomList.Last ().End.position);
	}



	void OnDrawGizmos() {
		/*
		if(roomModel != null) {
			for (int i = 0; i < SPUT_MAGNETTE; i ++) {
				for (int j = 0; j < SPUT_MAGNETTE; j++) {
					Gizmos.color = Color.blue;
					if(roomModel[i][j] != 0) {
						Gizmos.DrawWireCube(new Vector3(i * Room.ROOM_SIZE.x, j * Room.ROOM_SIZE.y, 0) , new Vector3(Room.ROOM_SIZE.x,Room.ROOM_SIZE.y,0));
					} else {
						Gizmos.DrawCube(new Vector3(i * Room.ROOM_SIZE.x, j * Room.ROOM_SIZE.y, 0) , new Vector3(Room.ROOM_SIZE.x,Room.ROOM_SIZE.y,0));
					}
				}
			}
		}
		*/
	}




}
