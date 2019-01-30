using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour {
	private float speed = 2.0f;
	private bool canRotate = false;
	protected int dir = 0;

	private float detectionRadius = 0.05f;
	private int mapSize = 4;

	void Start () {}
	
	void Update () {
	}

	protected void updatePosition() {
		transform.position += transform.forward * speed * Time.deltaTime;
		bool mi = meetsIntersection();
		if (canRotate && mi) {
			if (dir == 2) dir = 0;
			transform.eulerAngles += new Vector3(0, 90 * dir, 0);
			transform.position = new Vector3(
				Mathf.Round(transform.position.x),
				transform.position.y,
				Mathf.Round(transform.position.z)
			);
			canRotate = false;
			dir = 0;
		} else if (!canRotate && !mi) {
			canRotate = true;
		}
	}

	bool meetsIntersection() {
		int xint = (int) Mathf.Round(transform.position.x);
		int zint = (int) Mathf.Round(transform.position.z);

		float xdec = Mathf.Abs(transform.position.x % 1);
		float zdec = Mathf.Abs(transform.position.z % 1);

		if (xdec > detectionRadius || zdec > detectionRadius || xint % mapSize != 0 || zint % mapSize != 0) return false;
		return true;
	}
}
