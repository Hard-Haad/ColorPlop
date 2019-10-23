using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSpawner : MonoBehaviour {

	public GameManager gameManager;
	public GameObject parentCircle;
	public GameObject gamePrefab;
	public SpawnController spawnController;
	public int numberOfPoints;

	Vector3 centerPos;
	Color[] colorsArray;
    GameObject[] circles;

	public void SpawnInCircle(){
	
		centerPos = transform.position;
		colorsArray = new Color[numberOfPoints];
        circles = new GameObject[numberOfPoints];
		gameManager.SetNumberOfBallsSpawned (numberOfPoints);

		for (int i = 0; i < numberOfPoints; i++) {
			float j = (i * 1.0f) / numberOfPoints;
			float angle = j * Mathf.PI * 2f;
			var x = Mathf.Sin (angle) * 2f;
			var y = Mathf.Cos (angle) * 2f;
			var pos =  new Vector3 (x, y, 0) + parentCircle.transform.position;
			GameObject colorCircle =(GameObject) Instantiate (gamePrefab, pos, Quaternion.identity);
            circles[i] = colorCircle;
			colorCircle.transform.SetParent (parentCircle.transform);
            Color _randomColor = GenerateRandomColor(i, 1.0f / (float)numberOfPoints);
            colorCircle.GetComponent<SpriteRenderer>().color = _randomColor;
            colorCircle.GetComponent<Light>().color = _randomColor;
            colorsArray [i] = colorCircle.GetComponent<SpriteRenderer> ().color;
		}

		spawnController.AcquireColors (colorsArray);

	}

    Color GenerateRandomColor(int _value,float hueInterval) {
        Color result;

        _value += 1;

        float maxHue = hueInterval * _value;
        float minHue =(maxHue - hueInterval < 0) ? 0f : maxHue - hueInterval;

        Debug.Log("Hue Interval: " + hueInterval);
        Debug.Log("Max Hue: " + maxHue);
        Debug.Log("Min Hue: " + minHue);

        result = Random.ColorHSV(minHue, maxHue, 0.7f, 1, 0.5f, 1);

        return result;
    }

    public void DetachCircles() {
        for (int i = 0; i < numberOfPoints; i++) {
            if (circles[i] != null) {
                circles[i].GetComponent<Rigidbody2D>().gravityScale = Random.Range(0.3f, 1.0f);
                circles[i].GetComponent<Collider2D>().isTrigger = false;
                circles[i].transform.SetParent(null);
            }
        }
       
    }
}
