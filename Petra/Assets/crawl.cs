using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crawl : MonoBehaviour {

	GameObject baser;
	Vector3 pos;

	// Use this for initialization
	void Start () {
		//baser = GameObject.FindGameObjectWithTag("porter");
		//pos = baser.GetComponent<Transform> ().position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (0, 0, (Time.deltaTime/10));
	}
}
