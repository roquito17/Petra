using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class drop_init : MonoBehaviour {

	GameObject HMD;
	GameObject m;
	GameObject anim;
	GameObject anim_child;
	GameObject camRig;
	Transform T;
	Transform P;
	Vector3 target;
	Vector3 pos;
	bool fall;
	int i;
	float alpha = 1;
	Image image;    
	Color tempAlpha;
	Animator drop;
	bool inRoom;


	// Use this for initialization
	void Start () {
		HMD = GameObject.Find ("Camera (eye)");
		image = GameObject.Find ("Image").GetComponent<Image> ();
		anim = GameObject.Find ("anim_parent");
		anim_child = GameObject.Find ("animation");
		drop = anim_child.GetComponent<Animator> ();
		camRig = GameObject.Find ("[CameraRig]");
		tempAlpha = image.color;
		m = GameObject.Find ("marker");
		inRoom = false;
		fall = false;
		i = 0;
		T = HMD.GetComponent<Transform>();
		P = m.GetComponent<Transform> ();

		//StartCoroutine;
	}

	// Update is called once per frame
	void Update () {
		target = T.position;
		pos = P.position;
		//print ("Marker pos: " + pos + "HMD pos: " + target);
		if (target.x < (pos.x - .5f)){
			inRoom = true;
		}
		if (target.x > (pos.x - .5f) && inRoom) {
			fall = true;
			i += 1;
			if (i == 1) {

				anim.transform.position = camRig.transform.position;
				//anim_child.GetComponent<Animation> ().Play();
				camRig.transform.parent = anim_child.transform;
				drop.SetTrigger ("dropOn");

				i += 1;
				//print("starting");
				//print ("True");
				//print (i);
			}
			if (i == 90) {
				StartCoroutine (Fade ());
				Invoke ("movingOn", 3);
			}

		}

	}

	IEnumerator Fade(){

		//        while (i < 100) {
		//            print ("True " + i);
		//            i += 1;
		//            yield return null;
		//        }
		while (fall) {
			tempAlpha.a = alpha/255;
			//print (alpha);
			image.color = tempAlpha;
			//image = alpha;
			alpha = alpha + 4;
			yield return null;
		}


	}

	void movingOn(){
		SceneManager.LoadSceneAsync(2);
	}

}

