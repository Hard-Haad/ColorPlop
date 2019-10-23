using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAbleController : MonoBehaviour {

	public GameObject onHitEffect;
	public float forceToApply;
	Rigidbody2D spawnAbleRigidbody;
	Collider2D spawnAbleCollider;

	void Start () {
		spawnAbleCollider = GetComponent<Collider2D> ();
		spawnAbleRigidbody = GetComponent<Rigidbody2D> ();	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}

	public void Shoot(){
		spawnAbleRigidbody.AddForce (transform.up * forceToApply * Time.deltaTime, ForceMode2D.Impulse);
		Invoke ("DestroyBall", 1f);
	}

	void DestroyBall(){
		GameObject.Find ("GameManager").GetComponent<GameManager> ().EventBallMissed ();
		Destroy (gameObject);
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "SpawnAbleObject") {
			GameObject onHitEffectInstance = (GameObject)Instantiate (onHitEffect, transform.position, Quaternion.identity);
			onHitEffectInstance.GetComponent<ParticleSystem> ().Play ();
			var main = onHitEffectInstance.GetComponent<ParticleSystem> ().main;
			main.startColor = GetComponent<SpriteRenderer> ().color;
			Destroy (gameObject);
		}
	}
}
