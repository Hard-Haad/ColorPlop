using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAbleController : MonoBehaviour {


	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "ShootAbleColorObject") {
			if (col.gameObject.GetComponent<SpriteRenderer> ().color == gameObject.GetComponent<SpriteRenderer> ().color) {
				GameObject.Find ("GameManager").GetComponent<GameManager> ().EventBallDestroyed ();
				Destroy (gameObject);
			} else {
				GameObject.Find ("GameManager").GetComponent<GameManager> ().EventWrongCollision ();
			}
		} 
	}
}
