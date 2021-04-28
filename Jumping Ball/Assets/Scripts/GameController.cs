using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	public static GameController instance;

	private PlayerMovement player;

	bool start = false;
	[SerializeField]
	private GameObject startButton;
	bool pause = false;
	[SerializeField]
	private GameObject pauseUi;

	[SerializeField]
	private Text scoreText, endScoreText, bestScoreText;
	public int Score = 0;

	[SerializeField]
	private GameObject gameOverPanel;

	private void Awake()
	{
		Time.timeScale = 0;
		_makeInstance();
	}

	void _makeInstance()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	public void StartGame()
	{
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		startButton.SetActive(false);
		Time.timeScale = 1;
		start = true;
	}
	
	public void Pause()
	{
		if (start)
		{
			pause = !pause;
			pauseUi.SetActive(true);
			Time.timeScale = 0;
		}
		else
		{
			pauseUi.SetActive(false);
			Time.timeScale = 0;
			player.vec = Vector3.zero;
		}
	}
	public void Resume()
	{
		pause = false;
		Time.timeScale = 1;
		pauseUi.SetActive(false);
	}
	public void Restart()
	{
		Time.timeScale = 0;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
	public void Quit()
	{
		Application.Quit();
	}

	public void _setScore(int score)
	{
		scoreText.text = "Score: " + score;
	}

	public void _GameOverShowPanel(int score)
	{
		scoreText.gameObject.SetActive(false);
		gameOverPanel.SetActive(true);
		endScoreText.text = "" + score;
		if (score > GameManager.instance.GetHighScore())
		{
			GameManager.instance.SetHighScore(score);
		}

		bestScoreText.text="" + GameManager.instance.GetHighScore();
	}
}
