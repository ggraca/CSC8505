using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour {
	protected float speed = 4.0f;

	protected float detectionRadius = 0.1f;
	protected int mapSize = 4;

	public Vector3 target;

	public int dir = 0;

	public GameObject frontTrain = null;
	public GameObject backTrain = null;

	protected void updatePosition() {
		transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
		transform.LookAt(target);
	}

	void OnTriggerEnter (Collider col) {
		if (gameObject.GetComponent<Carriage>() != null && gameObject.GetComponent<Carriage>().trainState != TrainState.Following) return;
        
		if(col.gameObject.tag == "Intersection") {
			if (backTrain) backTrain.GetComponent<Train>().dir = dir;
			
			if (dir != 0) {
				transform.eulerAngles += new Vector3(0, 90 * dir, 0);
				transform.position = getNextPosition(col.gameObject.transform.position);
				dir = 0;
			}
			findTarget();
        }
    }

	protected void findTarget() {
		target = transform.position + (transform.forward * mapSize);
	}

	Vector3 getNextPosition(Vector3 colliderPos) {
		if (frontTrain) {
			return frontTrain.transform.position - frontTrain.transform.forward * 0.6f;
		} else {
			return new Vector3(
				colliderPos.x,
				transform.position.y,
				colliderPos.z
			);
		}
	}

	public void breakTrains() {
		if (backTrain) backTrain.GetComponent<Train>().breakTrains();
		if (frontTrain) frontTrain.GetComponent<Train>().backTrain = null;
		frontTrain = null;
	}
}