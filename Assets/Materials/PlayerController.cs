﻿using System.Collections;
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

	public Rigidbody ball;

	float saveMaxLeft;
	float saveMaxRight;

	float[,] cup_positions; // stores the starting position of the cups
	Quaternion[] cup_rotations; // stores the starting rotation of the cups

	// Use this for initialization
	void Start () {
		//save cups transforms
		cup_positions = new float[6, 3];
		cup_rotations = new Quaternion[6];
		for (int i=0; i<6; i++) {
			Transform trans = cups.GetChild(i).transform;
			cup_positions[i, 0] = trans.position.x;
			cup_positions[i, 1] = trans.position.y;
			cup_positions[i, 2] = trans.position.z;

			cup_rotations[i] = cups.GetChild(i).transform.rotation;
		}
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

		// RIGHT HAND
		if (rightIndex >= 0) {
			float rightTrigger = SteamVR_Controller.Input(rightIndex).GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger).magnitude;
			bool a_btn = SteamVR_Controller.Input(rightIndex).GetPress(Valve.VR.EVRButtonId.k_EButton_A);
			if (a_btn) {
				ball.transform.position = new Vector3(-0.5289996f, 1.177f, -0.05564839f);
				ball.velocity = new Vector3(0f, 0f, 0f);
			}
			
			// reset cups
			bool b_btn = SteamVR_Controller.Input(rightIndex).GetPress(Valve.VR.EVRButtonId.k_EButton_ApplicationMenu);
			if (b_btn) {
				Vector3 pos;
				for (int i=0; i<6; i++) {
					pos = new Vector3(cup_positions[i, 0], cup_positions[i, 1], cup_positions[i, 2]);
					cups.GetChild(i).transform.position = pos;
					cups.GetChild(i).transform.rotation = cup_rotations[i];
					cups.GetChild(i).gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
					cups.GetChild(i).gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
				}
			}
				
			if (rightHand.intersected != null && rightTrigger > 0.2f) {
				// pick up right
				rightHeldObj = rightHand.intersected;
				saveMaxRight = rightHand.intersected.maxAngularVelocity;
				rightHand.intersected.maxAngularVelocity = Mathf.Infinity;
			}

			if (rightHeldObj != null && rightTrigger <= 0.2f) {
				// release right object
				rightHeldObj.velocity = SteamVR_Controller.Input(rightIndex).velocity;
				rightHeldObj.angularVelocity = SteamVR_Controller.Input(rightIndex).angularVelocity;
				rightHeldObj.maxAngularVelocity = saveMaxRight;
				rightHeldObj = null;
			}
				
			if (rightHeldObj != null) {
				// force object to follow right hand
				rightHeldObj.velocity = (rightHand.transform.position - rightHeldObj.position) / Time.deltaTime;
				// update rotation
				float angle;
				Vector3 axis;
				Quaternion q = rightHand.transform.rotation * Quaternion.Inverse (rightHeldObj.rotation);
				q.ToAngleAxis (out angle, out axis);
				rightHeldObj.angularVelocity = axis * angle * Mathf.Deg2Rad / Time.deltaTime;
			}

		} // RIGHT HAND

		// LEFT HAND
		if (leftIndex >= 0) {
			float leftTrigger = SteamVR_Controller.Input(leftIndex).GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger).magnitude;

			if (leftHand.intersected != null && leftTrigger > 0.2f) {
				// pick up left
				leftHeldObj = leftHand.intersected;
			}
				
			if (leftHeldObj != null && leftTrigger <= 0.2f) {
				// release left object
				leftHeldObj.velocity = SteamVR_Controller.Input(leftIndex).velocity;
				leftHeldObj.angularVelocity = SteamVR_Controller.Input(leftIndex).angularVelocity;
				leftHeldObj = null;
			}

			if (leftHeldObj != null) {
				// force object to follow left hand
				leftHeldObj.velocity = (leftHand.transform.position - leftHeldObj.position) / Time.deltaTime;
				// update rotation
				float angle;
				Vector3 axis;
				Quaternion q = leftHand.transform.rotation * Quaternion.Inverse (leftHeldObj.rotation);
				q.ToAngleAxis (out angle, out axis);
				leftHeldObj.angularVelocity = axis * angle * Mathf.Deg2Rad / Time.deltaTime;
			}

		} // LEFT HAND

	} // LateUpdate


}
