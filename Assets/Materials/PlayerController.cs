using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public Transform head;
	public HandController leftHand;
	public HandController rightHand;

	public Transform hmd;
	public Transform leftController;
	public Transform rightController;

	public Rigidbody leftHeldObj;
	public Rigidbody rightHeldObj;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void LateUpdate() {
		head.position = hmd.position;
		leftHand.transform.position = leftController.position;
		rightHand.transform.position = rightController.position;

		head.rotation = hmd.rotation;
		leftHand.transform.rotation = leftController.transform.rotation;
		rightHand.transform.rotation = rightController.transform.rotation;

		int leftIndex = (int)leftController.GetComponent<SteamVR_TrackedObject>().index;
		int rightIndex = (int)rightController.GetComponent<SteamVR_TrackedObject>().index;

		float leftTrigger = SteamVR_Controller.Input(leftIndex).GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger).magnitude;
		float rightTrigger = SteamVR_Controller.Input(rightIndex).GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger).magnitude;

		if (leftHand.intersected != null && leftTrigger > 0.2f) {
			// pick up left
			leftHeldObj = leftHand.intersected;
		}
		if (leftHeldObj != null && leftTrigger <= 0.2f) {
			// release left object
			leftHeldObj.velocity = SteamVR_Controller.Input(leftIndex).velocity;
			leftHeldObj = null;
		}

		if (rightHand.intersected != null && rightTrigger > 0.2f) {
			// pick up right
			rightHeldObj = rightHand.intersected;
		}
		if (rightHeldObj != null && rightTrigger <= 0.2f) {
			// release right object
			rightHeldObj.velocity = SteamVR_Controller.Input(rightIndex).velocity;
			rightHeldObj = null;
		}

		if (leftHeldObj != null) {
			// force object to follow left hand
			leftHeldObj.velocity = (leftHand.transform.position - leftHeldObj.position) / Time.deltaTime;
			leftHeldObj.rotation = leftHand.transform.rotation;
		}
		if (rightHeldObj != null) {
			// force object to follow right hand
			rightHeldObj.velocity = (rightHand.transform.position - rightHeldObj.position) / Time.deltaTime;
			rightHeldObj.rotation = rightHand.transform.rotation;
		}
	}
}
