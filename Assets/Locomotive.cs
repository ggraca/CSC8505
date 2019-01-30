using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locomotive : Train {
	[SerializeField]
	private int size = 5;
	public GameObject cartPrefab;

	void Start() {
		GameObject current;
		GameObject previous = this.gameObject;
		for(int i = 0; i < size; i++) {
			current = Instantiate(cartPrefab, previous.transform.position - previous.transform.forward * 0.4f, previous.transform.rotation);
			current.GetComponent<Carriage>().frontCarriage = previous;
			previous = current;
		}
	}

	void Update () {
		changeDir();
		updatePosition();
	}

	void changeDir() {
		if (Input.GetKey("down")) dir = 0;
		if (Input.GetKey("right")) dir = 1;
		if (Input.GetKey("up")) dir = 2;
		if (Input.GetKey("left")) dir = 3;
	}
}
