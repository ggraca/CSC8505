using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	private Locomotive locomotive;
	public int id = 0;

	public int score = 1;
	private Text scoreText;

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
		Color.cyan,
		Color.green,
		Color.black
	};

	public static Vector2[] scoreCoords = new [] {
		new Vector2(0, 0)
	};

	void Start () {
		locomotive = GetComponent<Locomotive>();
		locomotive.setColour(colors[id]);
		scoreText = GameObject.Find("Score" + id).GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		handleInput();
		scoreText.text = score.ToString();
	}

	void handleInput() {
		if (Input.GetAxis("Vertical" + id) > 0.1f) locomotive.dir = Vector3.forward;
		if (Input.GetAxis("Vertical" + id) < -0.1f) locomotive.dir = -Vector3.forward;
		if (Input.GetAxis("Horizontal" + id) > 0.1f) locomotive.dir = Vector3.right;
		if (Input.GetAxis("Horizontal" + id) < -0.1f) locomotive.dir = -Vector3.right;
	}
}
