using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : MonoBehaviour {
	Vector3 target;
	Vector3 pos;
	GameObject HMD;
	GameObject m;
	GameObject camRig;
	GameObject anim;
	int i;
	int b;
	bool ledge;
	AudioSource stone;
	public float volume;
	Transform T;
	Transform P;
	// Use this for initialization
	void Start () {
		HMD = GameObject.Find ("Camera (eye)");
		m = GameObject.Find ("marker");
		camRig = GameObject.Find ("[CameraRig]");
		anim = GameObject.Find ("floor_anim");
		ledge = true;
		stone = this.GetComponent<AudioSource> ();
		i = 0;
		b = 0;
		volume = 0.0f;
		T = HMD.GetComponent<Transform> ();
		P = m.GetComponent<Transform> ();
	}

	// Update is called once per frame
	void Update () {
		target = T.position;
		pos = P.position;
		if (target.x < (pos.x)){
		//Check if HMD is inside the trap room
			i += 1;
			if (target.x < (pos.x) & (i > 180)) {
			//Time delay for floor sliding
				anim.GetComponent<Animator>().SetTrigger("slideOn");
				//print (i);

				volume = 1f - ((630.0f - (float)i) / 450.0f);
				//print (volume);
				stone.volume = volume;
				//stone.volume = 0;
				//print (i + " " + volume + " " + stone.volume);

				if (i == 181) {
					
					stone.Play ();
				}
				//transform.Translate ((Time.deltaTime / -2), 0, 0);
				//print (camRig.transform.position);
				if (target.x < (pos.x) & (i > 180) & ledge) {
				//if there's space on the floor for the HMD it slides with the floor	
					camRig.transform.parent = m.transform;
				}
				if (target.x <= -171.9) {
				//if there's no space	
					ledge = false;
					b += 1;
				}
			} 
//			if (i > 480) {
//			
//			}
			if ((target.x <= -171.9) & (b == 1)) {
			//you get trapped	
				//print ("unparent");
				camRig.transform.parent = null;

			}
		}
	}
}
