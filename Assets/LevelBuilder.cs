using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class LevelBuilder : Singleton<LevelBuilder> {

	public enum Theme { TEST }

	//Size of area square lol.
	public static int SPUT_MAGNETTE = 50;
	public static int SPUT_DISTANCE = 50;

	public Vector2 spawnPoint = new Vector2(0,0);

	List<Room> RoomList;

	public List<Room> TestRooms;

	public int[][] roomModel;

	protected LevelBuilder() {}

	public void Start() {
		BuildLevel (Theme.TEST);
	}

	public void BuildLevel(Theme theme) {

		roomModel = new int[SPUT_MAGNETTE][];

		for (int i = 0; i < SPUT_MAGNETTE; i++) {
			roomModel[i] = new int[SPUT_MAGNETTE];
		}

		if (theme == Theme.TEST) {
			RoomList = TestRooms;
		}

		GenerateSolutionPath();
		GenerateRooms();
		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
		foreach (GameObject player in players) {
			player.transform.position = spawnPoint;
		}
	}

	private void GenerateRooms() {
		for (int i = 0; i < SPUT_MAGNETTE; i ++) {
			for (int j = 0; j < SPUT_MAGNETTE; j++) {
				List<Room> ValidRooms;
				if(roomModel[i][j] == 0) {
					ValidRooms = TestRooms.Where (x => x.solid).ToList();
				} else {
					bool topConfig = (j + 1 < SPUT_MAGNETTE && roomModel[i][j+1] != 0);
					bool rightConfig = (i + 1 < SPUT_MAGNETTE && roomModel[i + 1][j] != 0); 
					bool downConfig = (j - 1 >= 0 && roomModel[i][j - 1] != 0); 
					bool leftConfig = (i - 1 >= 0 && roomModel[i - 1][j] != 0); 
					ValidRooms = TestRooms.Where (x => x.IsValidForConfig(topConfig,rightConfig,downConfig,leftConfig)).ToList();
				}

				if(ValidRooms.Count == 0) {
				} else {
					GameObject go = (GameObject) Instantiate(ValidRooms[Mathf.FloorToInt(UnityEngine.Random.value * ValidRooms.Count)].gameObject);
					go.transform.position = new Vector3(i * Room.ROOM_SIZE.x, j * Room.ROOM_SIZE.y, 0);
					go.transform.parent = transform;
				}
			}
		}
	}

	private void GenerateSolutionPath() {
		//Start on the far right.
		int x = 1;
		//Start at some random whye
		int y = Mathf.FloorToInt(SPUT_MAGNETTE/2);
		//2 = entrance goes here....
		roomModel [x] [y] = 2;
		spawnPoint = new Vector2 (x * Room.ROOM_SIZE.x, y * Room.ROOM_SIZE.y);
		int numberOfTriesSoFar = 0;
		for (int i = 0; i < SPUT_DISTANCE; i++) {
			int candidateX = x;
			int candidateY = y;

			switch( Mathf.FloorToInt(UnityEngine.Random.Range(0,3))) {
				case 0:
					candidateY++;
					break;
				case 1:
					candidateX++;
					break;
				case 2:
					candidateY--;
					break;

				default:
					candidateX = -1;
					break;
			}

			if((candidateX > 0 && candidateX < SPUT_MAGNETTE) && (candidateY > 0 && candidateY < SPUT_MAGNETTE) && roomModel[candidateX][candidateY] == 0) {
				x = candidateX;
				y = candidateY;
				roomModel[x][y] = 1;
				numberOfTriesSoFar = 0;
			} else {
				numberOfTriesSoFar ++;
				i--;
				if(numberOfTriesSoFar > 10) {
					//dug into a corner, bail...
					throw new StackOverflowException("eh....");
				}
			}
		}

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
