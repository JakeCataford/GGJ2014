using UnityEngine;
using System.Collections;

public class GameBehaviour : MonoBehaviour {
	public GameManager Game {
		get {
			return GameManager.Instance;
		}
		private set {}
	}
}
