using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public static GameController instance;
	private const string High_Score = "High Score";
	private const string Selected_Bird = "Selected Bird";
	private const string Green_Bird = "Green Bird";
	private const string Red_Bird = "Red Bird";

	// Use this for initialization
	void Awake () {
		MakeSingleton ();
		IsTheGameStartedForTheFirstTime ();
		//PlayerPrefs.DeleteAll ();
	}

	// Update is called once per frame
	void MakeSingleton () {
		if (instance != null) {
			Destroy(gameObject);
		} else {
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

	void IsTheGameStartedForTheFirstTime() {
		if (!PlayerPrefs.HasKey ("IsTheGameStartedForTheFirstTime")) {
			PlayerPrefs.SetInt(High_Score, 0);
			PlayerPrefs.SetInt(Selected_Bird, 0);
			PlayerPrefs.SetInt(Green_Bird, 0);
			PlayerPrefs.SetInt(Red_Bird, 0);
			PlayerPrefs.SetInt("IsTheGameStartedForTheFirstTime", 0);
		}
	}

	public void SetHighScore(int score) {
		PlayerPrefs.SetInt(High_Score, score);
	}

	public int GetHighScore() {
		return PlayerPrefs.GetInt (High_Score);
	}

	public void SetSelectedBird(int selectedBird) {
		PlayerPrefs.SetInt (Selected_Bird, selectedBird);
	}

	public int GetSelectedBird() {
		return PlayerPrefs.GetInt (Selected_Bird);
	}

	public void UnlockGreenBird() {
		PlayerPrefs.SetInt (Green_Bird, 1);
	}

	public int IsGreenBirdUnlocked() {
		return PlayerPrefs.GetInt (Green_Bird);
	}

	public void UnlockRedBird() {
		PlayerPrefs.SetInt (Red_Bird, 1);
	}

	public int IsRedBirdUnlocked() {
		return PlayerPrefs.GetInt (Red_Bird);
	}


}
