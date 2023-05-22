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
	public bool pause = false;
	[SerializeField]
	private GameObject pauseUi;

	[SerializeField]
	private Text scoreText, endScoreText, bestScoreText2, bestScoreText;
	private int score;
	public int Score {
		get
		{
			return score;
		}
		set
		{
			score = value;
			SetScore(value);
		}
	}

	[SerializeField]
	private GameObject gameOverPanel;

	private void Awake()
	{
		Time.timeScale = 0;

        if (instance == null)
        {
            instance = this;
        }
    }

	private void Start()
	{
        bestScoreText2.text = "High Score: " + GameManager.instance.GetHighScore().ToString();
    }

	private void Update()
	{
		if (!pause)
		{
			Score++;
        }
	}

	public void StartGame()
	{
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		startButton.SetActive(false);
		Time.timeScale = 1;
		start = true;
		pause = false;
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
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	public void Quit()
	{
		Application.Quit();
	}

	public void SetScore(int score)
	{
		scoreText.text = "Score: " + score;
	}

	public void GameOverShowPanel()
	{
		StartCoroutine(EndGame());	
	}

	IEnumerator EndGame()
	{
		yield return new WaitForSeconds(1);

        scoreText.gameObject.SetActive(false);
        gameOverPanel.SetActive(true);
        endScoreText.text = "" + Score;
        if (score > GameManager.instance.GetHighScore())
        {
            GameManager.instance.SetHighScore(Score);
        }

        bestScoreText.text = GameManager.instance.GetHighScore().ToString();
    }
}
