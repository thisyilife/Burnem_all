﻿using UnityEngine;
using System.Collections;

public class BearAI : MonoBehaviour {

	public bool freezeRotation = true;

	public float moveSpeed;
	public float jumpHeight;


	public bool goBacka = false;
	public bool goBackb = false;

	public bool shoota = false;
	public bool shootb = false;

	public bool bearCacSound; 
	public bool playerHited = false;
	public bool rockHited = false;


	private int amount = 2; 
	GameObject BulletTrailPrefab;

	void Start () {
		BulletTrailPrefab = Resources.Load("magicarpe2") as GameObject;
		bearCacSound = true; 

	}
	void OnCollisionEnter2D(Collision2D collider)
	{
		if (collider.gameObject.name == "Player")
		{
			playerHited = true;

		}

		if (collider.gameObject.name == "Rock")
		{
			rockHited = true;
			Debug.Log (rockHited);
		}
	}

	void Update () {


		var epos = gameObject.transform.position;
		Vector2 Sens = new Vector2(PlayerControllerV3.ppos.x - epos.x, PlayerControllerV3.ppos.y - epos.y).normalized;
		var xpos = (PlayerControllerV3.ppos.x - epos.x);

		if(Sens.x > 0) 
		{
			gameObject.transform.localScale = new Vector3(-1, 1, 1);
			if (playerHited == false) {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveSpeed, GetComponent<Rigidbody2D> ().velocity.y);
			} else {
				playerHited = false;
				goBacka = true;
				if (bearCacSound == true) {
					SoundEffectsHelper.Instance.DoBearSound ();
					bearCacSound = false;
				}
			}
		}
		else 
		{
			gameObject.transform.localScale = new Vector3(1, 1, 1);
			if (playerHited == false) {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (-moveSpeed, GetComponent<Rigidbody2D> ().velocity.y);
			}else {
				playerHited = false;
				goBackb = true;
				if (bearCacSound == true) {
					SoundEffectsHelper.Instance.DoBearSound ();
					bearCacSound = false;
				}
			}
		}

		if (goBacka == true) {
			if ((xpos < 8) && !rockHited) {
				gameObject.transform.localScale = new Vector3 (1, 1, 1);
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (-moveSpeed, GetComponent<Rigidbody2D> ().velocity.y);
			}
			if ((xpos >8) || rockHited){
				shoota = true;
				SoundEffectsHelper.Instance.DoBearSoundCac();
				rockHited = false;

			}
		}

		if (goBackb == true) {
			if ((xpos > -8) && !rockHited) {
				gameObject.transform.localScale = new Vector3 (-1, 1, 1);
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveSpeed, GetComponent<Rigidbody2D> ().velocity.y);
			}
			if ((xpos < -8) || rockHited){
				shootb = true;
				SoundEffectsHelper.Instance.DoBearSoundCac();
				rockHited = false;
			}
		}

		if (shootb == true) {
				gameObject.transform.localScale = new Vector3 (1, 1, 1);
				GameObject bullet = Instantiate (BulletTrailPrefab) as GameObject;
			Vector3 dir = new Vector3 (-5,-1,0);
			bullet.transform.position = new Vector3 (gameObject.transform.position.x -1.5f,gameObject.transform.position.y +1,0);             // C'est pour que la torche part de la main et non au milieu
				Rigidbody2D rb = bullet.GetComponent<Rigidbody2D> (); 
				rb.velocity = (dir * amount);
			shootb = false;
			goBackb = false;
			bearCacSound = true;

		}
		if (shoota == true) {
			gameObject.transform.localScale = new Vector3 (-1, 1, 1);
			GameObject bullet = Instantiate (BulletTrailPrefab) as GameObject;
			Vector3 dir = new Vector3 (5,1,0);
			bullet.transform.position = new Vector3 (gameObject.transform.position.x +1.5f,gameObject.transform.position.y +1,0);             // C'est pour que la torche part de la main et non au milieu
			Rigidbody2D rb = bullet.GetComponent<Rigidbody2D> ();
			rb.velocity = (dir * amount); 
			shoota = false;
			goBacka = false;
			bearCacSound = true;


		}
	}


}
