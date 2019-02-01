using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour {
	protected float speed = 4.0f;

	protected float detectionRadius = 0.1f;
	protected int mapSize = 4;

	public Vector3 target;

	protected void updatePosition() {
		if (target == null) return;
		transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
		transform.LookAt(target);
	}

	protected bool meetsIntersection() {
		int xint = (int) Mathf.Round(transform.position.x);
		int zint = (int) Mathf.Round(transform.position.z);

		float xdec = Mathf.Abs(transform.position.x % 1);
		float zdec = Mathf.Abs(transform.position.z % 1);

		if (xdec > detectionRadius || zdec > detectionRadius || xint % mapSize != 0 || zint % mapSize != 0) return false;
		return true;
	}
}
