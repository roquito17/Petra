using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parent : MonoBehaviour {
	GameObject prent;
	Vector3 pos;
	//int rot;
	Quaternion rot;
	Vector3 scl;

	// Use this for initialization
	void Start () {
		prent = GameObject.FindGameObjectWithTag ("porter");	
		pos = prent.transform.position;
		rot = prent.transform.rotation;
		scl = prent.transform.localScale;
		pos.y = pos.y + .5211895f;
		rot.x = rot.x - 90;
		//rot = -90;
		this.transform.position = pos;
		transform.Rotate (-90, 0, 0);
		this.transform.localScale = scl;
		//this.transform.rotation.x = rot;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
