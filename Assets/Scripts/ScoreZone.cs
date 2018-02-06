using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreZone : MonoBehaviour {

	public Transform cups;
	public GameLogic gameLogic;

	void OnTriggerEnter(Collider other) {
		if (other.GetComponent<Cup>() != null) {
			gameLogic.add_score();
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

	

}
