using UnityEngine;
using System.Collections;
namespace CollisionExtentions {
public static class CollisionExtensions {
		public static Vector2 AverageNormal(this ContactPoint2D[] contacts) {
			Vector2 finalNormal = new Vector2 (0, 0);

			foreach (ContactPoint2D contact in contacts) {
				finalNormal += contact.normal;
			}

			return finalNormal.normalized;
		}

	}
}
