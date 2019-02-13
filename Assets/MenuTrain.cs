using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTrain : MonoBehaviour {

	public int id = 0;
	// Use this for initialization
	void Start () {
		transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material.color = Player.colors[id];
		transform.GetChild(1).GetChild(0).GetComponent<Renderer>().material.color = Player.colors[id];
	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround(transform.position, transform.up, Time.deltaTime * 90f);
	}
}
