using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour {
	[SerializeField]
	private int gridSize = 7;

	[SerializeField]
	private int gridScale = 4;

	[SerializeField]
	private int amount = 50;
	
	[SerializeField]
	public GameObject cartPrefab;
	[SerializeField]
	public GameObject intersectionPrefab;

	void Start () {
		spawnIntersections();
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
}
