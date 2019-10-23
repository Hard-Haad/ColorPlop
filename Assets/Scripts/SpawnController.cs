using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {

	public GameObject spawnAbleObject;

	[SerializeField]
	int count;
	Queue colorQ;
	bool spawned;
	GameObject currentShootAbleColorObject;

	void Start () {
		spawned = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (count != 0) {
			if (!spawned) {
				GameObject _shootAbleColorInstance = (GameObject)Instantiate (spawnAbleObject, transform.position, Quaternion.identity);
				currentShootAbleColorObject = _shootAbleColorInstance;
                Color _colorInstance = (Color)colorQ.Dequeue();
                currentShootAbleColorObject.GetComponent<SpriteRenderer>().color = _colorInstance;
                currentShootAbleColorObject.GetComponent<TrailRenderer>().startColor = _colorInstance;
                currentShootAbleColorObject.GetComponent<Light>().color = _colorInstance;
                spawned = true;
			}

			if (Input.GetMouseButtonDown(0)) {
				currentShootAbleColorObject.GetComponent<ShootAbleController> ().Shoot ();
				spawned = false;
				count--;
			}
		}
	}

	public void AcquireColors(Color[] colorsArray){

        colorsArray = ShuffleArray(colorsArray);

		colorQ = new Queue();
		foreach (Color colorValue in colorsArray) {
			colorQ.Enqueue (colorValue);
			count++;
		}

	}

    Color[] ShuffleArray(Color[] colorsArray) {
        for (int i = 0; i < colorsArray.Length; i++) {
            int shuffleInteger = Random.Range(0, colorsArray.Length - 1);

            Color _color = colorsArray[i];
            colorsArray[i] = colorsArray[shuffleInteger];
            colorsArray[shuffleInteger] = _color;

        }

        return colorsArray;
    }
}
