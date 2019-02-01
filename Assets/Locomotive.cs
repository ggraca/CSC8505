using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locomotive : Train {
	[SerializeField]
	private int size = 5;
	public GameObject cartPrefab;
	private List<GameObject> cartList = new List<GameObject>();

	private int dir = 0;

	void Start() {
		transform.position = Vector3.zero;
		transform.rotation = Quaternion.identity;
		findTarget();
		for(int i = 0; i < size; i++) AddCart();
	}

	void Update () {
		if (meetsIntersection()) {
			transform.eulerAngles += new Vector3(0, 90 * dir, 0);
			dir = 0;
			findTarget();
		} else {
			changeDir();
		}
		updatePosition();
	}

	void findTarget() {
		target = transform.position + (transform.forward * mapSize);
	}

	void changeDir() {
		if (Input.GetKey("down")) dir = 0;
		if (Input.GetKey("right")) dir = 1;
		if (Input.GetKey("up")) dir = 0;
		if (Input.GetKey("left")) dir = 3;
	}

	void AddCart(GameObject newCart = null) {
		GameObject last = (cartList.Count == 0) ? this.gameObject : cartList[cartList.Count - 1];
		
		Vector3 pos = last.transform.position - last.transform.forward * 0.6f;
		Quaternion rot = last.transform.rotation;

		if (newCart) {
			if (cartList.Contains(newCart)) return;
		} else {
			newCart = Instantiate(cartPrefab, pos, rot);
		}
		
		newCart.GetComponent<Carriage>().frontCarriage = last;
		cartList.Add(newCart);
	}

	void OnCollisionEnter (Collision col) {
        if(col.gameObject.tag == "Cart") {
			AddCart(col.gameObject);
            col.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
			col.gameObject.GetComponent<Rigidbody>().AddExplosionForce(400, col.contacts[0].point, 10, 10);
        }
    }

	
}
