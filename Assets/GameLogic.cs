using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour {
	[SerializeField]
	private int numberPlayers = 4;

	[SerializeField]
	private int gridSize = 5;

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

	void Start () {
		spawnIntersections();
		spawnPlayers();
		spawnCarts();
	}

	void spawnCarts() {
		float sideSize = gridSize * gridScale;
		for(int i = 0; i < amount; i++) {
			float x = Random.Range(-sideSize, sideSize);
			float z = Random.Range(-sideSize, sideSize);
			
			Instantiate(
				cartPrefab,
				new Vector3(x, 0, z),
				Quaternion.identity
			);
		}
	}

	void spawnIntersections() {
		for (int i = -gridScale * gridSize; i <= gridScale * gridSize; i += gridScale) {
			for (int j = -gridScale * gridSize; j <= gridScale * gridSize; j += gridScale) {
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
}
