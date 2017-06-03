using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class teleport_HMD : MonoBehaviour {

	GameObject tport;
	Vector3 target;
	Vector3 pos;
	GameObject HMD;
	public int scene;
	int i = 0;
	GameObject fx;
	AudioSource tp;
	Transform T;
	Transform P;

	// Use this for initialization
	void Start () {
		tport = GameObject.FindGameObjectWithTag("porter");
		HMD = GameObject.Find ("[CameraRig]");
		T = tport.GetComponent<Transform>();
		P = HMD.GetComponent<Transform>();
		pos = this.GetComponent<Transform> ().position;
		tp = tport.GetComponent<AudioSource> ();
		//print(target);
	}

	// Update is called once per frame
	void Update () {
		target = T.position;
		pos = P.position;
		//print("Pos: " + pos.x + " " + pos.z + " Target: " + target.x + " " + target.z);

		if ((pos.x <= target.x + 1) & (pos.x >= target.x - 1) & (pos.z <= target.z + 1) & (pos.z >= target.z - 1)) {
			i += 1;
			//print ("True_test");

			if (i == 1) {
				tp.Play ();
				//Destroy (tport.GetComponent<crawl>());
				SceneManager.LoadScene("tport_fx", LoadSceneMode.Additive);
				Invoke ("Load", 2);
				print ("True");
			}
		}

	}

	void Load(){
		SceneManager.LoadSceneAsync (scene); 
	}

}

