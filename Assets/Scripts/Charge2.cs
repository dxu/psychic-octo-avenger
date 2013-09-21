﻿using UnityEngine;
using System.Collections;

public class Charge2 : MonoBehaviour {
  Player1 globalObj;
  GUIText charge2Text;
	// Use this for initialization
	void Start () {
    charge2Text = gameObject.GetComponent<GUIText>();
	}

	// Update is called once per frame
	void Update () {
    if(!globalObj) {
      GameObject g = GameObject.Find("Environment");
      globalObj = g.GetComponent<Environment>().player2;
    }
    charge2Text.text = "Shield: " + globalObj.shield.life.ToString();
	}
}
