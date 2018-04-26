using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour {

	public static GameplayController instance;

	[SerializeField]
	private Text scoreText, endScore, High_Score, gameOverText;

	[SerializeField]
	private Button restartGameButton, instructionsButton;

	[SerializeField]
	private GameObject pausePanel;

	[SerializeField]
	private GameObject[] birds;

	[SerializeField]
	private Sprite[] medals;

	[SerializeField]
	private Image medalImage;

	// Use this for initialization
	void Awake () {
		MakeInstance ();
		Time.timeScale = 0f;
	}

	// Update is called once per frame
	void MakeInstance () {
		if (instance == null) {
			instance = this;
		}
	}

	public void PauseGame() {
		if (BirdScript.instance != null) {
			if (BirdScript.instance.isAlive) {
				pausePanel.SetActive(true);
				gameOverText.gameObject.SetActive(false);
				endScore.text = "" + BirdScript.instance.score;
				High_Score.text = "" + GameController.instance.GetHighScore();
				Time.timeScale = 0f;
				restartGameButton.onClick.RemoveAllListeners();
				restartGameButton.onClick.AddListener(() => ResumeGame());
			}
		}
	}

	public void GoToMenuButton() {
		SceneFader.instance.FadeIn ("MainMenu");
	}

	public void ResumeGame() {
		pausePanel.SetActive (false);
		Time.timeScale = 1f;
	}

	public void RestartGame() {
		SceneFader.instance.FadeIn (Application.loadedLevelName);
	}

	public void PlayGame() {
		scoreText.gameObject.SetActive (true);
		birds[GameController.instance.GetSelectedBird()].SetActive(true);
		instructionsButton.gameObject.SetActive (false);
		Time.timeScale = 1f;
	}

	public void SetScore(int score) {
		scoreText.text = "" + score;
	}

	public void PlayerDiedShowScore(int score) {
		pausePanel.SetActive (true);
		gameOverText.gameObject.SetActive (true);
		scoreText.gameObject.SetActive (false);

		endScore.text = "" + score;

		if (score > GameController.instance.GetHighScore()) {
			GameController.instance.SetHighScore(score);
		}

		High_Score.text = "" + GameController.instance.GetHighScore ();

		if (score <= 20) {
			medalImage.sprite = medals [0];
		} else if (score > 20 && score < 40) {
			medalImage.sprite = medals [1];
			if (GameController.instance.IsGreenBirdUnlocked() == 0) {
				GameController.instance.UnlockGreenBird ();
			}

		} else {
			medalImage.sprite = medals [2];
			if (GameController.instance.IsGreenBirdUnlocked() == 0) {
				GameController.instance.UnlockGreenBird ();
			}
			if (GameController.instance.IsRedBirdUnlocked() == 0) {
				GameController.instance.UnlockRedBird ();
			}
		}

		restartGameButton.onClick.RemoveAllListeners ();
		restartGameButton.onClick.AddListener (() => RestartGame());
	}


}
