using UnityEngine;
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
  private UFO ufo;
  private float ufoChance = 0.005f;
  public int score = 0;
  public int lives = 3;
  Vector3 alienStart; // start of aliens
  Vector3 groundStart; // start of aliens
  private int ground = 40; // the height of the ground in screen pixels

	// Use this for initialization
	void Start () {


    // generate aliens in a grid
    Vector3 originScreen = Camera.main.WorldToScreenPoint(new Vector3(0,0,0));
    br = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,0,originScreen.z));
    tl = Camera.main.ScreenToWorldPoint(new Vector3(0,Screen.height,originScreen.z));

    aliens  = new Alien[rows, cols];
    shields  = new Shield[shieldCount];
    alienStart = Camera.main.ScreenToWorldPoint(new Vector3(40, Screen.height - 100, originScreen.z));
    alienStart.z = 0;

    groundStart = Camera.main.ScreenToWorldPoint(new Vector3(0, 40, originScreen.z));
    groundStart.z = 0;

    // generate the ship
    ship = Instantiate(Resources.Load("Prefabs/ShipPrefab"),
        new Vector3(0, br.y + 4, alienStart.z),
        Quaternion.identity) as Ship;

    Vector3 alienOffset = new Vector3(-(tl.x - br.x) / 2 / cols, (tl.y - br.y) / 3 / rows, 0);
    Vector3 shieldOffset = new Vector3(-(tl.x - br.x) / 5,
        br.y + 2.5f * 4.0f, 0);
    // generate aliens
    for(int i = 0; i < rows; i++) {
      for(int j = 0; j < cols; j++) {
        aliens[i, j] = (Instantiate(Resources.Load("Prefabs/AlienPrefab"),
            new Vector3(alienStart.x + alienOffset.x * j, alienStart.y - alienOffset.y * i, alienStart.z),
            Quaternion.identity) as GameObject).GetComponent<Alien>();
      }
    }
    // generate shields
    for(int i = 0; i < shieldCount ; i++) {
      shields[i] = (Instantiate(Resources.Load("Prefabs/ShieldPrefab"),
          new Vector3(tl.x + shieldOffset.x * (i+1), shieldOffset.y, alienStart.z),
          Quaternion.identity) as GameObject).GetComponent<Shield>();
    }

    // generate the builder and fighter spawns, generate the players
    // 30, 30 from

	}

  void spawnUFO() {
    // generate ufo
    ufo = (Instantiate(Resources.Load("Prefabs/UFOPrefab"),
            new Vector3(tl.x, tl.y - 1, alienStart.z),
            Quaternion.identity) as GameObject).GetComponent<UFO>();
    AudioClip fireSound = (AudioClip)Resources.Load("Audio/ufo_lowpitch");
    AudioSource.PlayClipAtPoint(fireSound, gameObject.transform.position);
  }

  private bool right = true;
  private bool vertical = false;
	// Update is called once per frame
	void Update () {
    // check the ufo, if it exists, check if its out of boundaries
    if(ufo != null) {
      if(ufo.transform.position.x < tl.x || ufo.transform.position.x > br.x) {
        ufo.die();
        Debug.Log(ufo);
      }
    }
    // chance to spawn ufo
    else {
      float chance = Random.Range(0.0f, 1.0f);
      if(chance < ufoChance)
        spawnUFO();
    }



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
