using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyboard_teleport : MonoBehaviour {

	GameObject sphere;
	GameObject camRig;
	GameObject ctrlL;
	GameObject ctrlR;
	bool noControllers;
	bool area;
	Vector3 height;
	Vector3 target;
	Vector3 pos;
	GameObject tport;

	// Use this for initialization
	void Start () {
		sphere = GameObject.Find ("Sphere");
		tport = GameObject.FindGameObjectWithTag("porter");

		camRig = GameObject.Find ("[CameraRig]");
		ctrlL = GameObject.FindGameObjectWithTag ("ctrlL");
		ctrlR = GameObject.FindGameObjectWithTag ("ctrlR");
		//noControllers = ((ctrlL == null) & (ctrlR == null));

	}
	
	// Update is called once per frame
	void Update () {
		noControllers = ((ctrlL == null) & (ctrlR == null));
		
		area = sphere.GetComponent<MeshRenderer> ().enabled;

		if (Input.anyKeyDown && area && noControllers){
			//print ("Porting");
			pos = sphere.transform.position;
			target = tport.transform.position;
			height = sphere.transform.position;

			if ((pos.x <= target.x + 1) & (pos.x >= target.x - 1) & (pos.z <= target.z + 1) & (pos.z >= target.z - 1)) {
				height = target;
			}
			if (SteamVR.instance.hmd_TrackingSystemName == "oculus") {
				height.y += 2;
			}
			camRig.transform.position = height;
		}
	}
}
