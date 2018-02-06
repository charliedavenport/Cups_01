using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CupsGUI : MonoBehaviour {

	public Text scoreText;
	public Text resetText;
	public Text EndGameText;
	public GameLogic gameLogic;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		scoreText.text = "Score: " + gameLogic.get_score();
		resetText.text = "Ball Resets: " + gameLogic.get_ball_resets();
	}

	public void EndGame(bool win) {
		EndGameText.text = win ? "Congrats! Press B to reset the game." : "Uh oh! Press B to reset the game.";
	}
}
