using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locomotive : Train {
	[SerializeField]
	private int size = 5;
	public GameObject cartPrefab;
	private List<GameObject> cartList = new List<GameObject>();

	private int forceModifier = 1000;

	void Start() {
		transform.position = Vector3.zero;
		transform.rotation = Quaternion.identity;
		findTarget();
		for(int i = 0; i < size; i++) AddCart();
	}

	void Update () {
		changeDir();
		updatePosition();
	}

	void changeDir() {
		if (Input.GetKey("down")) dir = 0;
		if (Input.GetKey("right")) dir = 1;
		if (Input.GetKey("up")) dir = 0;
		if (Input.GetKey("left")) dir = 3;
	}

	void OnCollisionEnter (Collision col) {
        if(col.gameObject.tag == "Cart" && col.gameObject.GetComponent<Train>().frontTrain != gameObject) {
			col.gameObject.GetComponent<Train>().breakTrains();
			AddCart(col.gameObject);
            col.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
			col.gameObject.GetComponent<Carriage>().trainState = TrainState.Flying;
			col.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 0.2f * forceModifier);
			col.gameObject.GetComponent<Rigidbody>().AddForce(transform.up * 0.5f * forceModifier);
			col.gameObject.GetComponent<Rigidbody>().AddForce(transform.right * Random.Range(-0.2f, 0.2f) * forceModifier);
        }
    }

	void AddCart(GameObject cart = null) {
		GameObject lastCart = getLastCart();
		
		Vector3 pos = lastCart.transform.position - lastCart.transform.forward * 0.6f;
		Quaternion rot = lastCart.transform.rotation;

		if (!cart) cart = Instantiate(cartPrefab, pos, rot);
		
		joinTrains(lastCart, cart);
	}

	GameObject getLastCart() {
		GameObject currentCart = gameObject;
		while(currentCart.GetComponent<Train>().backTrain != null) {
			currentCart = currentCart.GetComponent<Train>().backTrain;
		}
		return currentCart;
	}

	void joinTrains(GameObject front, GameObject back) {
		//back.GetComponent<Train>().frontTrain.GetComponent<Train>().backTrain = null;
		//front.GetComponent<Train>().backTrain.GetComponent<Train>().frontTrain = null;

		back.GetComponent<Train>().frontTrain = front;
		front.GetComponent<Train>().backTrain = back;
	}
}
