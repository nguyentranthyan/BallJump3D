using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	private const string HIGH_SCORE = "High Score";

	private void Awake()
	{
		MakeSingleInstance();
		IsGameStartedForTheFirstTime();
	}
	//check đã tải app lần đầu
	void IsGameStartedForTheFirstTime()
	{
		if (!PlayerPrefs.HasKey("IsGameStartedForTheFirstTime"))
		{
			PlayerPrefs.SetInt(HIGH_SCORE, 0);
			PlayerPrefs.SetInt("IsGameStartedForTheFirstTime", 0);
		}
	}
	//đồng bộ giữa các scence
	void MakeSingleInstance()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

	public void SetHighScore(int score)
	{
		PlayerPrefs.SetInt(HIGH_SCORE, score);
	}

	public int GetHighScore()
	{
		return PlayerPrefs.GetInt(HIGH_SCORE);
	}
	public void PlayGame()
	{
		SceneManager.LoadScene(1);
	}
	public void Quit()
	{
		Application.Quit();
	}

	public void ResetScore()
	{
		PlayerPrefs.SetInt(HIGH_SCORE, 0);
	}
}
