using UnityEngine;
using System.Collections;

public class Environment : MonoBehaviour {

  private int rows = 5;
  private int cols = 11;
  private Alien[,] aliens;
  private Ship ship;
	// Use this for initialization
	void Start () {
    // generate aliens in a grid
    Vector3 originScreen = Camera.main.WorldToScreenPoint(new Vector3(0,0,0));
    Vector3 br = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,0,originScreen.z));
    Vector3 tl = Camera.main.ScreenToWorldPoint(new Vector3(0,Screen.height,originScreen.z));

    aliens  = new Alien[rows, cols];
    Vector3 start = Camera.main.ScreenToWorldPoint(new Vector3(40, Screen.height - 40, originScreen.z));
    start.z = 0;

    // generate the ship
    ship = Instantiate(Resources.Load("Prefabs/ShipPrefab"),
        new Vector3(0, br.y + 4, start.z),
        Quaternion.identity) as Ship;

    Vector3 offset = new Vector3(-(tl.x - br.x) / 2 / cols, (tl.y - br.y) / 2 / rows, 0);
    Debug.Log(start);
    Debug.Log(offset);
    for(int i = 0; i < rows; i++) {
      for(int j = 0; j < cols; j++) {
        aliens[i, j] = Instantiate(Resources.Load("Prefabs/AlienPrefab"),
            new Vector3(start.x + offset.x * j, start.y - offset.y * i, start.z),
            Quaternion.identity) as Alien;
      }
    }
	}

	// Update is called once per frame
	void Update () {
    // check each alien, if it goes past the boundary, change all aliens directions and return

	}
}
