using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carriage : Train {
	public bool adjusting = false;
	public GameObject frontCarriage;

	private Vector3 targetPosition;
	private Vector3 targetDirection;

	void Start () {
	}
	
	void Update () {
		if (transform.position.y > 1) return;

		if (frontCarriage == null) {
			adjustPosition();
			return;
		}

		if (meetsIntersection()) 
			target = frontCarriage.GetComponent<Train>().target;
			
		updatePosition();
	}

	void adjustPosition() {
		float targetx = transform.position.x;
		float targetz = transform.position.z;

		if (getDistanceToCenter(transform.position.x) > getDistanceToCenter(transform.position.z)) {
			targetx = Mathf.Round(transform.position.x / mapSize) * mapSize;
			targetDirection = Vector3.forward;
		} else {
			targetz = Mathf.Round(transform.position.z / mapSize) * mapSize;
			targetDirection = Vector3.right;
		}
		targetPosition = new Vector3(targetx, transform.position.y, targetz);

		transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);
		Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDirection, Time.deltaTime * speed, 0.0f);
		transform.rotation = Quaternion.LookRotation(newDir);
	}

	float getDistanceToCenter(float pos) {
		float lambda = Mathf.Abs(pos / mapSize);
		return Mathf.Abs((lambda % 1) - 0.5f);
	}
}
