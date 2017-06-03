//======= Copyright (c) Valve Corporation, All rights reserved. ===============
using UnityEngine;
using System.Collections;


public struct PointerEventArgs
{
	public uint controllerIndex;
	public uint flags;
	public float distance;
	public Transform target;
}

public delegate void PointerEventHandler(object sender, PointerEventArgs e);


public class SteamVR_LaserPointer : MonoBehaviour
{
	public bool active = true;
	public Color color;
	public float thickness = 0.002f;
	public GameObject holder;
	public GameObject pointer;
	bool isActive = false;
	public bool addRigidBody = false;
	public Transform reference;
	public event PointerEventHandler PointerIn;
	public event PointerEventHandler PointerOut;
	GameObject ctrlL;
	GameObject ctrlR;
	Vector3 offset;
	bool noControllers;
	GameObject sphere; 
	bool deleted;
	int interval;
	float nextTime;
	//GameObject cameraRig;


	Transform previousContact = null;

	// Use this for initialization
	void Start ()
	{
		interval = 2;
		nextTime = 0;
		ctrlL = GameObject.FindGameObjectWithTag ("ctrlL");
		ctrlR = GameObject.FindGameObjectWithTag ("ctrlR");
		sphere = GameObject.Find ("Sphere");
		//cameraRig = GameObject.Find("[CameraRig]");
		//noControllers = ((ctrlL == null) & (ctrlR == null));
		deleted = false;


//		if (!noControllers && !deleted) {
//			Destroy (GameObject.Find ("Camera (eye)").GetComponent<SteamVR_LaserPointer> ());
//			deleted = true;
//			print ("deleted");
//		} 

		holder = new GameObject ();
		holder.transform.parent = this.transform;
		holder.transform.localPosition = Vector3.zero;
		holder.transform.localRotation = Quaternion.identity;

		//            pointer = GameObject.CreatePrimitive (PrimitiveType.Cube);
		//            pointer.transform.parent = holder.transform;
		//            pointer.transform.localScale = new Vector3 (thickness, thickness, 100f);
		//            pointer.transform.localPosition = new Vector3 (0f, 0f, 50f);
		//            pointer.transform.localRotation = Quaternion.identity;
		//            BoxCollider collider = pointer.GetComponent<BoxCollider> ();
		//            if (addRigidBody) {
		//                if (collider) {
		//                    collider.isTrigger = true;
		//                }
		//                Rigidbody rigidBody = pointer.AddComponent<Rigidbody> ();
		//                rigidBody.isKinematic = true;
		//            } else {
		//                if (collider) {
		//                    Object.Destroy (collider);
		//                }
		//            }
		Material newMaterial = new Material (Shader.Find ("Unlit/Color"));
		newMaterial.SetColor ("_Color", color);
		//pointer.GetComponent<MeshRenderer> ().material = newMaterial;

	}

	public virtual void OnPointerIn(PointerEventArgs e)
	{
		if (PointerIn != null)
			PointerIn(this, e);
	}

	public virtual void OnPointerOut(PointerEventArgs e)
	{
		if (PointerOut != null)
			PointerOut(this, e);
	}


	// Update is called once per frame
	void Update ()
	{
		

		if ((Time.time >= nextTime) && (!deleted)) {
			ctrlL = GameObject.FindGameObjectWithTag ("ctrlL");
			ctrlR = GameObject.FindGameObjectWithTag ("ctrlR");
			noControllers = ((ctrlL == null) & (ctrlR == null));
			if (!noControllers && !deleted) {

				Destroy (GameObject.Find ("Camera (eye)").GetComponent<SteamVR_LaserPointer> ());
				deleted = true;
				print ("deleted");
			} 
			print (nextTime);
			nextTime += interval;
		}
		if (!isActive) {
			isActive = true;
			this.transform.GetChild (0).gameObject.SetActive (true);
		}

		float dist = 100f;

		SteamVR_TrackedController controller = GetComponent<SteamVR_TrackedController>();

		Ray raycast = new Ray(transform.position, transform.forward);
		RaycastHit hit;
		bool bHit = Physics.Raycast(raycast, out hit);

		if(previousContact && previousContact != hit.transform)
		{
			PointerEventArgs args = new PointerEventArgs();
			if (controller != null)
			{
				args.controllerIndex = controller.controllerIndex;
			}
			args.distance = 0f;
			args.flags = 0;
			args.target = previousContact;
			OnPointerOut(args);
			previousContact = null;
			//pointer.GetComponent<MeshRenderer> ().enabled = false;

		}
		if(bHit && previousContact != hit.transform)
		{
			PointerEventArgs argsIn = new PointerEventArgs();
			if (controller != null)
			{
				argsIn.controllerIndex = controller.controllerIndex;
			}
			argsIn.distance = hit.distance;
			argsIn.flags = 0;
			argsIn.target = hit.transform;
			OnPointerIn(argsIn);
			previousContact = hit.transform;
			//print (previousContact);


		}
		if(!bHit)
		{
			previousContact = null;
		}
		if (bHit && hit.distance < 7f) {
			dist = hit.distance;
			//print (dist);
			sphere.transform.position = (raycast.origin + (raycast.direction * dist));

			//pointer.GetComponent<MeshRenderer> ().enabled = false;
			sphere.GetComponent<MeshRenderer> ().enabled = true;



		} else {
			//pointer.GetComponent<MeshRenderer> ().enabled = false;
			sphere.GetComponent<MeshRenderer> ().enabled = false;

		}
		//        if ((bHit && hit.distance < 7f) && noControllers) {
		//            
		//            //print ("hit");
		//            dist = hit.distance;
		//            //print ("origin: " + raycast.origin + " direction: " + raycast.direction + " distance: " + dist + " cameraRig: " + cameraRig.transform.position);
		//            //print (dist);
		//            sphere.transform.position = (raycast.origin + (raycast.direction * dist));
		//            //print (sphere.transform.position);
		//            pointer.GetComponent<MeshRenderer> ().enabled = false;
		//            sphere.GetComponent<MeshRenderer> ().enabled = true;
		//
		//
		//        } else {
		//            pointer.GetComponent<MeshRenderer> ().enabled = false;
		//            sphere.GetComponent<MeshRenderer> ().enabled = false;
		//        }

		if (controller != null && controller.triggerPressed)
		{
			//pointer.transform.localScale = new Vector3(thickness * 5f, thickness * 5f, dist);

		}
		else
		{
			//pointer.transform.localScale = new Vector3(thickness, thickness, dist);

		}
		//pointer.transform.localPosition = new Vector3(0f, 0f, dist/2f);

	}
}

