using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyController : MonoBehaviour {

	public CircleRotation circleRotation;
	public CircleSpawner circleSpawner;
    public Text timerText;

    GameManager mainGameManager;

    float originalSpeed;
    bool speedChanged;
    [SerializeField]
    bool timerStarted;
	int levelNumber;
	string levelNumberKey = "LevelNumber";

    const int percentageIncreaseInDifficultyForNumberOfBalls = 1;
	const float percentageIncreaseInDifficultyForSpeed = 0.2f;
    const int percentageIncreaseInDifficulty = 1;
    
    float startTimer = 1.0f;

    private void Start()
    {
        mainGameManager = GetComponent<GameManager>();
    }

    public void SetConditions(){
		levelNumber = PlayerPrefs.GetInt (levelNumberKey);

        circleRotation.rotatingSpeed = Mathf.Clamp(1 + (percentageIncreaseInDifficultyForSpeed * levelNumber),1,4);
        originalSpeed = circleRotation.rotatingSpeed;

        if (levelNumber >= 1 && levelNumber <= 10)
        {
            circleRotation.directionVector = 1;

            circleSpawner.numberOfPoints = Mathf.Clamp(Mathf.RoundToInt(percentageIncreaseInDifficultyForNumberOfBalls * levelNumber), 3, 7);
        }
        else if (levelNumber >= 11 && levelNumber <= 20)
        {
            circleRotation.directionVector = -1;

            circleSpawner.numberOfPoints = Mathf.Clamp(Mathf.RoundToInt(percentageIncreaseInDifficultyForNumberOfBalls * levelNumber), 7, 15);

            InvokeRepeating("ChangeSpeed", 0f, 7f);
        }
        else if (levelNumber >= 21 && levelNumber <= 30)
        {
            circleRotation.directionVector = 1;

            circleSpawner.numberOfPoints = Mathf.Clamp(Mathf.RoundToInt(percentageIncreaseInDifficultyForNumberOfBalls * levelNumber), 15, 20);

            InvokeRepeating("ChangeSpeed", 0f, 7f);
        }
        else if (levelNumber >= 31 && levelNumber <= 40)
        {
            circleRotation.directionVector = -1;

            circleSpawner.numberOfPoints = Mathf.Clamp(Mathf.RoundToInt(percentageIncreaseInDifficultyForNumberOfBalls * levelNumber), 20, 25);

            InvokeRepeating("ChangeSpeed", 0f, 7f);
        }
        else if (levelNumber >= 41 && levelNumber <= 50)
        {
            circleRotation.directionVector = GetRandomDirection();

            circleSpawner.numberOfPoints = Mathf.Clamp(Mathf.RoundToInt(percentageIncreaseInDifficultyForNumberOfBalls * levelNumber), 25, 30);

            InvokeRepeating("ChangeDirection", 0f, 20f);

            InvokeRepeating("ChangeSpeed", 0f, 5f);
        }
        else if (levelNumber >= 51 && levelNumber <= 60)
        {
            circleRotation.directionVector = GetRandomDirection();

            circleSpawner.numberOfPoints = Mathf.Clamp(Mathf.RoundToInt(percentageIncreaseInDifficultyForNumberOfBalls * levelNumber), 30, 35);

            InvokeRepeating("ChangeDirection", 0f, 15f);

            InvokeRepeating("ChangeSpeed", 0f, 4f);
        }
        else {

            circleRotation.directionVector = GetRandomDirection();

            circleSpawner.numberOfPoints = Mathf.Clamp(Mathf.RoundToInt(percentageIncreaseInDifficultyForNumberOfBalls * levelNumber), 35, 35);

            InvokeRepeating("ChangeDirection", 0f, 10f);

            InvokeRepeating("ChangeSpeed", 0f, 4f);
        }
    }

    int GetRandomDirection() {
        int result;

        result = Random.Range(-1, 2);

        if (result == 0 || result == 2) {
            result = GetRandomDirection();
        }

        return result;
    }

    void ChangeDirection() {
        circleRotation.directionVector *= -1;
    }

    void ChangeSpeed() {
        if (!speedChanged)
        {
            circleRotation.rotatingSpeed = originalSpeed;

            speedChanged = true;
        }
        else {
            circleRotation.rotatingSpeed /= 3;

            speedChanged = false;
        }
    }
}
