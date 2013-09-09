﻿using UnityEngine;
using System.Collections;

public class Environment : MonoBehaviour {

  private int rows = 5;
  private int cols = 11;
  private Alien[,] aliens;
  private Shield[] shields;
  private int shieldCount = 4;
  private Ship ship;
  private Vector3 br;
  private Vector3 tl;
	// Use this for initialization
	void Start () {
    // generate aliens in a grid
    Vector3 originScreen = Camera.main.WorldToScreenPoint(new Vector3(0,0,0));
    br = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,0,originScreen.z));
    tl = Camera.main.ScreenToWorldPoint(new Vector3(0,Screen.height,originScreen.z));

    aliens  = new Alien[rows, cols];
    shields  = new Shield[shieldCount];
    Vector3 start = Camera.main.ScreenToWorldPoint(new Vector3(40, Screen.height - 40, originScreen.z));
    start.z = 0;

    // generate the ship
    ship = Instantiate(Resources.Load("Prefabs/ShipPrefab"),
        new Vector3(0, br.y + 4, start.z),
        Quaternion.identity) as Ship;

    Vector3 alienOffset = new Vector3(-(tl.x - br.x) / 2 / cols, (tl.y - br.y) / 2 / rows, 0);
    Vector3 shieldOffset = new Vector3(-(tl.x - br.x) / 5,
        br.y + 2.5f * 4.0f, 0);
    // generate aliens
    for(int i = 0; i < rows; i++) {
      for(int j = 0; j < cols; j++) {
        aliens[i, j] = (Instantiate(Resources.Load("Prefabs/AlienPrefab"),
            new Vector3(start.x + alienOffset.x * j, start.y - alienOffset.y * i, start.z),
            Quaternion.identity) as GameObject).GetComponent<Alien>();
      }
    }
    // generate shields
    for(int i = 0; i < shieldCount ; i++) {
      shields[i] = (Instantiate(Resources.Load("Prefabs/ShieldPrefab"),
          new Vector3(tl.x + shieldOffset.x * (i+1), shieldOffset.y, start.z),
          Quaternion.identity) as GameObject).GetComponent<Shield>();
    }
	}

  private bool right = true;
  private bool vertical = false;
	// Update is called once per frame
	void Update () {
    // check each alien, if it goes past the boundary, change all aliens directions and return

    foreach(Alien a in aliens) {
      // if going vertical, ignore
      if(a == null || a.vertical) {
        return;
      }
      // if horizontal check to see if hit edge
      if((!a.right && a.transform.position.x - a.getVelocity() < tl.x) || (a.right && a.transform.position.x + a.getVelocity() > br.x)) {
        foreach(Alien b in aliens) {
          b.changeDirection();
        }
        return;
      }
    }
	}
}
