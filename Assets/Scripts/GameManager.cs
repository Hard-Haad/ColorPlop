using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public SpawnController spawnController;
    public Text levelText;
    public Text startButtonText;
    public GameObject startButton;
    public GameObject facebookShareButton;
    public GameObject exitButton;
    public GameObject gameOverMenu;
    public AudioClip plopSound;
    public AudioClip badPlopSound;

    public CircleSpawner circleSpawner;
    public Animator fadeAnimator;
    public Animator startButtonAnimator;
    public Animator titleTextAnimator;
    public Animator exitButtonAnimator;
    public Animator facebookShareButtonAnimator;
    public GameObject mainMenuEffect;

    DifficultyController difficultyController;

    bool gameOver;
    int numberOfBallsSpawned;
    int numberOfBallsDestroyed;
    int levelNumber;
    string levelNumberKey = "LevelNumber";
    string gameStartedKey = "GameStarted";
    AudioSource plopSource;

    void Start() {

        plopSource = GetComponent<AudioSource>();

        levelNumber = PlayerPrefs.GetInt(levelNumberKey);

        difficultyController = GetComponent<DifficultyController>();

        if (levelNumber == 0) {
            levelNumber++;
            PlayerPrefs.SetInt(levelNumberKey, levelNumber);
            PlayerPrefs.Save();
        }

        levelText.text = "";

        CheckIfGameStarted();

    }

    void CheckIfGameStarted() {
        int _gameStarted = PlayerPrefs.GetInt(gameStartedKey);

        if (_gameStarted == 1) {
            startButton.SetActive(false);
            facebookShareButton.SetActive(false);
            exitButton.SetActive(false);
            StartGame();
        }
    }

    public void StartGame() {
        PlayerPrefs.SetInt(gameStartedKey, 1);
        PlayerPrefs.Save();

        fadeAnimator.SetTrigger("FadeIn");
        titleTextAnimator.SetTrigger("FadeIn");

        startButton.GetComponent<Button>().interactable = false;
        facebookShareButton.GetComponent<Button>().interactable = false;
        exitButton.GetComponent<Button>().interactable = false;
        mainMenuEffect.SetActive(false);

        if (startButton.activeSelf == true) {
            startButtonAnimator.SetTrigger("FadeIn");
            facebookShareButtonAnimator.SetTrigger("FadeIn");
            exitButtonAnimator.SetTrigger("FadeIn");
        }

        levelText.text = "Level " + levelNumber;

        CreateGame();
    }

    void CreateGame() {

        difficultyController.SetConditions();
        circleSpawner.SpawnInCircle();

    }

    public void SetNumberOfBallsSpawned(int _number) {
        numberOfBallsSpawned = _number;
    }

    public void EventBallDestroyed() {
        if (!gameOver)
        {
            plopSource.clip = plopSound;
            plopSource.Play();
        }

        numberOfBallsDestroyed++;

        if (numberOfBallsDestroyed == numberOfBallsSpawned) {
            Debug.Log("Game Over");
            StartNextLevel();
        }
    }

    public void EventBallMissed() {
        if (!gameOver)
        {
            plopSource.clip = badPlopSound;
            plopSource.Play();
            gameOver = true;
        }
        circleSpawner.DetachCircles();
        DisplayGameOverMenu();
    }

    public void EventWrongCollision()
    {
        if (!gameOver)
        {
            plopSource.clip = badPlopSound;
            plopSource.Play();
            gameOver = true;
        }
        circleSpawner.DetachCircles();
        DisplayGameOverMenu();
    }

    public void EventTimerUp()
    {
        if (!gameOver)
        {
            plopSource.clip = badPlopSound;
            plopSource.Play();
            gameOver = true;
        }
        circleSpawner.DetachCircles();
        DisplayGameOverMenu();
    }

    public void PlayAgain() {
        CreateNewLevel();
    }

    void DisplayGameOverMenu() {
        spawnController.enabled = false;
        levelText.text = "";
        gameOverMenu.SetActive(true);
    }


    public void DisplayMainMenu() {
        PlayerPrefs.SetInt(gameStartedKey, 0);
        PlayerPrefs.Save();
        SceneManager.LoadScene(0);    
    }
		
	void StartNextLevel(){
		IncrementLevel ();
		CreateNewLevel ();
	}

	void CreateNewLevel(){
		SceneManager.LoadScene (0);
	}

	void IncrementLevel(){
		levelNumber++;
		PlayerPrefs.SetInt (levelNumberKey, levelNumber);
		PlayerPrefs.Save ();
	}

    public void ExitGame(){
        Application.Quit();
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt(gameStartedKey, 0);
        PlayerPrefs.Save();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            PlayerPrefs.SetInt(gameStartedKey, 0);
            PlayerPrefs.Save();
        }
    }


    private void OnApplicationFocus(bool focus)
    {
        if (!focus) {
            PlayerPrefs.SetInt(gameStartedKey, 0);
            PlayerPrefs.Save();
        }
    }

}
