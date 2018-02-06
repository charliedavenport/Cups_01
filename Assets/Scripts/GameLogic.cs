using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour {

	public Ball ball;
	public Transform cups;
	public PlayerController pcontroller;

	public CupsGUI gui;

	public bool gameOver;

	int score;
	int n_ball_resets;
	int n_cups_knocked_over;

	public int get_score() {return score;}
	public int get_ball_resets() { return n_ball_resets;}

	public void add_score() { 
		n_cups_knocked_over++;
		score += 10;
		// check for end of game
		if (n_cups_knocked_over == 18) {
			gameOver = true;
			gui.EndGame(true);
		}
	}

	public void game_reset() {
		gameOver = false;
		score = 0;
		n_ball_resets = 15;
		n_cups_knocked_over = 0;
		gui.EndGameText.text = "";
	}

	public void ball_reset() { 
		score--;
		if (n_ball_resets > 0) n_ball_resets--; 
		if (n_ball_resets == 0) {
			gui.EndGame(false);
			gameOver = true;
		}
	}

	// Use this for initialization
	void Start () {
		gameOver = false;
		score = 0;
		n_ball_resets = 15;
		n_cups_knocked_over = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
