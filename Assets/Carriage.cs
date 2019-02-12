using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrainState {
	Flying,
	Adjusting,
	Following,
	CatchingUp
}

public class Carriage : Train {

	public TrainState trainState;

	private Vector3 targetPosition;
	private Vector3 targetDirection;

	protected float adjustingSpeed = 1.0f;
	
	void Start() {
		if (frontTrain) trainState = TrainState.Following;
		else trainState = TrainState.Adjusting;

		GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
	}

	void Update () {
		switch (trainState) {
			case TrainState.Flying:
				break;

			case TrainState.Adjusting:
				adjustPosition();
				break;

			case TrainState.Following:
				if (!frontTrain) {
					trainState = TrainState.Adjusting;
					return;
				}
				updatePosition();
				break;

			case TrainState.CatchingUp:
				if (!frontTrain) {
					trainState = TrainState.Adjusting;
					return;
				}
				target = frontTrain.transform.position - (frontTrain.transform.forward * 0.6f);
				speed = 8;
				updatePosition();
				
				if (Vector3.Distance(transform.position, target) < 0.1f) {
					if (frontTrain.GetComponent<Carriage>() && frontTrain.GetComponent<Carriage>().trainState != TrainState.Following) return;

					transform.rotation = frontTrain.transform.rotation;
					transform.position = target;
					target = frontTrain.GetComponent<Train>().target;
					dir = frontTrain.GetComponent<Train>().dir;
					speed = 4;

					trainState = TrainState.Following;
				}
				
				break;
		}
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
		targetPosition = new Vector3(targetx, 0, targetz);

		transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * adjustingSpeed);
		Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDirection, Time.deltaTime * adjustingSpeed, 0.0f);
		transform.rotation = Quaternion.LookRotation(newDir);
	}

	float getDistanceToCenter(float pos) {
		float lambda = Mathf.Abs(pos / mapSize);
		return Mathf.Abs((lambda % 1) - 0.5f);
	}

	void OnCollisionEnter (Collision col) {
        if(col.gameObject.tag != "Floor") return;

		if (frontTrain) trainState = TrainState.CatchingUp;
		else trainState = TrainState.Adjusting;

		GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

		transform.position = new Vector3(
			transform.position.x,
			0,
			transform.position.z
		);
		transform.rotation = Quaternion.identity;

    }
}
