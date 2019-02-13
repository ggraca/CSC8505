using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour {
	[SerializeField]
	private int numberPlayers = 4;

	private int gridWidth = 4;
	private int gridLength = 2;

	[SerializeField]
	private int gridScale = 4;

	[SerializeField]
	private int amount = 50;
	
	[SerializeField]
	public GameObject cartPrefab;
	[SerializeField]
	public GameObject intersectionPrefab;
	[SerializeField]
	public GameObject playerPrefab;

	public int matchTime = 30;
	private float timeLeft;

	private Text timerText;
	private Text winnerText;

	public GameObject background;

	private bool gameOver = false;

	void Start () {
		timeLeft = matchTime;
		timerText = GameObject.Find("Timer").GetComponent<Text>();

		spawnIntersections();
		spawnPlayers();
		spawnCarts();
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Return)) {
			Time.timeScale = 1;
			SceneManager.LoadScene(1);
		}
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Time.timeScale = 1;
			SceneManager.LoadScene(0);
		}

		timeLeft -= Time.deltaTime;
		timerText.text = ((int)timeLeft).ToString();
		
		if (timeLeft < 0 && !gameOver) gameOverScreen();
	}

	void spawnCarts() {
		for(int i = 0; i < amount; i++) {
			float x = Random.Range(-(gridScale * gridWidth), gridScale * gridWidth);
			float z = Random.Range(-(gridScale * gridLength), gridScale * gridLength);
			
			Instantiate(
				cartPrefab,
				new Vector3(x, 0, z),
				Quaternion.identity
			);
		}
	}

	void spawnIntersections() {
		for (int i = -gridScale * gridWidth; i <= gridScale * gridWidth; i += gridScale) {
			for (int j = -gridScale * gridLength; j <= gridScale * gridLength; j += gridScale) {
				Instantiate(intersectionPrefab, new Vector3(i, 0, j), Quaternion.identity);
			}
		}
	}

	void spawnPlayers() {
		for(int i = 0; i < numberPlayers; i++) {
			GameObject go = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
			go.GetComponent<Player>().id = i;
		}
	}

	void gameOverScreen() {
		Time.timeScale = 0;
		gameOver = true;

		background.SetActive(true);
		winnerText = GameObject.Find("Winner").GetComponent<Text>();

		int best = -1;
		int winner = -1;
		bool draw = false;

		GameObject[] gos = GameObject.FindGameObjectsWithTag("Player");
		for(int i = 0; i < gos.Length; i++) {
			Player p = gos[i].GetComponent<Player>();
			if (p.score > best) {
				best = p.score;
				winner = p.id;
				draw = false;
			} else if (p.score == best) {
				draw = true;
			}
		}

		if (draw) {
			winnerText.text = "Draw!";
		} else {
			winnerText.color = Player.colors[winner];
			winnerText.text = "Player " + (winner + 1).ToString() + " wins!";
		}
	}
}
