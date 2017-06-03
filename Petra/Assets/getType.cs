using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
using Valve.VR;

public class getType : MonoBehaviour {

	Vector3 seated;
	GameObject camRig;
	// Use this for initialization
	void Start () {
		//print (SteamVR.instance.hmd_TrackingSystemName);
		camRig = GameObject.Find ("[CameraRig]");
		seated = transform.position;
		seated.x = camRig.transform.position.x;
		seated.y = camRig.transform.position.y + 2;
		seated.z = camRig.transform.position.z;
		if (SteamVR.instance.hmd_TrackingSystemName == "oculus") {
			//print ("oculus true");
			this.transform.position = seated;
		}


	}
	

}
