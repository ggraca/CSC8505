using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	private Locomotive locomotive;
	public int id = 0;

	public static Vector3[] startingPositions = new [] {
		new Vector3(-6, 0, 4),
		new Vector3(4, 0, 6),
		new Vector3(6, 0, -4),
		new Vector3(-4, 0, -6),
	};

	public static int[] startingRotations = new [] {
		90,
		180,
		-90,
		0
	};

	public static Color[] colors = new [] {
		Color.red,
		Color.blue,
		Color.green,
		Color.yellow
	};

	void Start () {
		locomotive = GetComponent<Locomotive>();
		locomotive.setColour(colors[id]);
	}
	
	// Update is called once per frame
	void Update () {
		handleInput();
	}

	void handleInput() {
		if (Input.GetAxis("Vertical" + id) > 0) locomotive.dir = Vector3.forward;
		if (Input.GetAxis("Vertical" + id) < -0) locomotive.dir = -Vector3.forward;
		if (Input.GetAxis("Horizontal" + id) > 0) locomotive.dir = Vector3.right;
		if (Input.GetAxis("Horizontal" + id) < -0) locomotive.dir = -Vector3.right;
	}
}
