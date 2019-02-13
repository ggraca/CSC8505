using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour {
	protected float speed = 4.0f;

	protected float detectionRadius = 0.1f;
	protected int mapSize = 4;

	public Vector3 target;

	public Vector3 dir = Vector3.zero;

	public GameObject frontTrain = null;
	public GameObject backTrain = null;

	protected void updatePosition() {
		transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
		transform.LookAt(target);
	}

	void OnTriggerEnter (Collider col) {
		if (gameObject.GetComponent<Carriage>() && gameObject.GetComponent<Carriage>().trainState != TrainState.Following) return;
        
		if(col.gameObject.tag == "Intersection") {
			dir = getRealDir(col.transform.position, dir);
			if (backTrain) backTrain.GetComponent<Train>().dir = dir;

			if (dir != transform.forward) {
				transform.eulerAngles = new Vector3(0, vec3ToDegrees(dir), 0);
				transform.position = getNextPosition(col.gameObject.transform.position);
				dir = transform.forward;
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
		setColour(Color.white);
	}

	Vector3 getRealDir(Vector3 pos, Vector3 dir) {
		List<Vector3> availablePos = new List<Vector3>();

		if (isInsideBounds(pos + transform.forward)) availablePos.Add(transform.forward);
		if (isInsideBounds(pos + transform.right)) availablePos.Add(transform.right);
		if (isInsideBounds(pos - transform.right)) availablePos.Add(-transform.right);

		if (dir == -transform.forward || dir == Vector3.zero) dir = transform.forward;
		foreach(Vector3 p in availablePos) {
			if (p == dir) return dir;
		}

		return availablePos[Random.Range(0, availablePos.Count - 1)];
	}

	int vec3ToDegrees(Vector3 vec) {
		if (vec == Vector3.forward) return 0;
		if (vec == Vector3.right) return 90;
		if (vec == -Vector3.forward) return 180;
		if (vec == -Vector3.right) return -90;
		return 0;
	}

	bool isInsideBounds(Vector3 pos) {
		if (Mathf.Abs(pos.x) > mapSize * 5) return false;
		if (Mathf.Abs(pos.z) > mapSize * 5) return false;
		return true;
	}

	public void setColour(Color c) {
		transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material.color = c;
		transform.GetChild(1).GetChild(0).GetComponent<Renderer>().material.color = c;
	}
}