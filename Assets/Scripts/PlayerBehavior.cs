﻿using UnityEngine;
using System.Collections;

public class PlayerBehavior : MonoBehaviour {
	public float velocityX = 0.5f;
	public float velocityY = 0f;
	float velocityXLimit = 5f;
	float velocityYLimit = 10f;
	float accelerationX = 0.1f;
	float accelerationY = 0.5f;
	public int damage_level = 0;
	public string ID = "Player";
	public Transform playerBase;
	public Texture duckBase;
	public Texture DuckDamageLevel2;
	public Texture DuckDamageLevel3;
	public Texture[] duckHealth;
	Quaternion spawnRotation;

	//PREFABS
	public Transform whirlpool;
	Transform waterAction;
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Enemy") {
			damage_level += 1;
			if (damage_level >= 3) {
				Debug.Log("Player should be dead by now.");
				damage_level %= 3;	// TODO: Get rid of this
			}
			playerBase.renderer.material.mainTexture = duckHealth[damage_level];
//			Debug.Log("FUNCTION CALLED");
		}
	}
	// Use this for initialization
	void Awake () {
		duckHealth = new Texture[3] {duckBase, DuckDamageLevel2, DuckDamageLevel3};
		spawnRotation = Quaternion.Euler(-90f, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		Move();
	}

	void FixedUpdate() {
		GetInput();
	}

	void GetInput() {
		//WHIRLPOOL INPUT
		if (Input.GetKey(KeyCode.A))
		{
			Whirlpool();
		}
		if (Input.GetKeyUp(KeyCode.A))
		{
			Destroy(waterAction.gameObject);
		}

		//WAVE INPUT
		if (Input.GetKeyDown(KeyCode.D))
		{
			Wave();
		}
		
	}

	void Whirlpool() {
		velocityX = 0;
		if(waterAction == null)
		{
			Vector3 spawnPos = new Vector3(transform.position.x, -4f, transform.position.z);
			waterAction = Instantiate(whirlpool,spawnPos,spawnRotation) as Transform;
		}
	}

	void Wave() {
		velocityY = velocityYLimit;
		velocityX = velocityXLimit;
	}

	void Move() {
		//MOVE TO THE RIGHT
		transform.Translate(Time.deltaTime*velocityX, Time.deltaTime*velocityY, 0);

		//ACCELTERATE
		if (velocityX < velocityXLimit) velocityX += accelerationX;

		//DECELERATE
		if (velocityY > 0) velocityY -= accelerationY;
	}

}
