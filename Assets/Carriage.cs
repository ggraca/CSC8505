using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carriage : Train {
	public GameObject frontCarriage;
	void Start () {}
	
	void Update () {
		if (transform.right == frontCarriage.transform.forward) dir = 1;
		else if (-transform.right == frontCarriage.transform.forward) dir = 3;
		updatePosition();
	}
}
