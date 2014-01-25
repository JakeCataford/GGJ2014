using UnityEngine;
using System.Collections;

public class Room : GameBehaviour {

	public static Vector2 ROOM_SIZE = new Vector2 (10, 7);
	public static float WALL_SIZE = 1.0f;

	public bool exitTop = false;
	public bool exitRight = false;
	public bool exitDown = false;
	public bool exitLeft = false;

	public bool solid = false;

	void Start() {
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.magenta;
		Gizmos.DrawWireCube (Vector3.zero, ROOM_SIZE);
		Gizmos.color = Color.green;
		Gizmos.DrawWireCube (Vector3.zero, new Vector2(ROOM_SIZE.x - WALL_SIZE, ROOM_SIZE.y - WALL_SIZE));
	}

	public bool IsValidForConfig(bool top, bool right, bool bottom, bool left) {
		if (solid && !top && !right && !bottom && !left && !right) {
			return solid && !top && !right && !bottom && !left && !right;
		}
		return (top && exitTop) || (right && exitRight) || (bottom && exitDown) || (left && exitLeft);
	}
}
