using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCarts : MonoBehaviour {
	[SerializeField]
	private int gridSize = 7;

	[SerializeField]
	private int gridScale = 4;

	[SerializeField]
	private int amount = 50;
	
	public GameObject cartPrefab;

	void Start () {
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
	
	// Update is called once per frame
	void Update () {
		
	}
}
