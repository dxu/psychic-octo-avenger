using UnityEngine;
using System.Collections;

public class Environment : MonoBehaviour {

  private int rows = 5;
  private int cols = 11;
  private Alien[,] aliens;
  private Platform[] platforms;
  private Human[] humans;
  public int humanCount = 20;
  private int platformCount = 4;
  private Ship ship;
  private Vector3 br;
  private Vector3 tl;
  private UFO ufo;
  private float ufoChance = 0.005f;
  public int score = 0;
  public int lives = 3;
  Vector3 alienStart; // start of aliens
  Vector3 groundStart; // start of aliens
  private int groundHeight = 20; // the height of the ground in screen pixels
  private Ground ground;
  public Player1 player1;
  public Player1 player2;
  private BuilderSpawn builderSpawn;
  private FighterSpawn fighterSpawn;
  public Camera fpcam;
  public Camera maincam;

	// Use this for initialization
	void Start () {

    // setup cameras
    fpcam = GameObject.Find("FPCamera").GetComponent<Camera>();
    maincam = GameObject.Find("Main Camera").GetComponent<Camera>();
    fpcam.active = false;
    maincam.active = true;

    // generate aliens in a grid
    Vector3 originScreen = Camera.main.WorldToScreenPoint(new Vector3(0,0,0));
    br = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,0,originScreen.z));
    tl = Camera.main.ScreenToWorldPoint(new Vector3(0,Screen.height,originScreen.z));

    humans  = new Human[humanCount];

    aliens  = new Alien[rows, cols];
    platforms  = new Platform[platformCount];
    alienStart = Camera.main.ScreenToWorldPoint(new Vector3(40, Screen.height - 100, originScreen.z));
    alienStart.z = 0;

    groundStart = Camera.main.ScreenToWorldPoint(new Vector3(0, groundHeight, originScreen.z));
    groundStart.x = 0;
    groundStart.z = 0;
    // generate ground
    ground = (Instantiate(Resources.Load("Prefabs/GroundPrefab"),
        groundStart,
        Quaternion.identity) as GameObject).GetComponent<Ground>();
    // generate player 1 and 2
    player1 = (Instantiate(Resources.Load("Prefabs/Player1Prefab"),
        groundStart - new Vector3(5, -3, 0),
        Quaternion.identity) as GameObject).GetComponent<Player1>();
    player2 = (Instantiate(Resources.Load("Prefabs/Player1Prefab"),
        groundStart + new Vector3(5.0f, 3.0f, 0.0f),
        Quaternion.identity) as GameObject).GetComponent<Player1>();
    player2.id = 2;

    // generate the builder and fighter spawns
    builderSpawn = (Instantiate(Resources.Load("Prefabs/BuilderSpawnPrefab"),
        new Vector3(tl.x + 1.5f, groundStart.y + 3.0f, 0),
        Quaternion.identity) as GameObject).GetComponent<BuilderSpawn>();
    fighterSpawn = (Instantiate(Resources.Load("Prefabs/FighterSpawnPrefab"),
        new Vector3(br.x - 1.5f, groundStart.y + 3.0f, 0),
        Quaternion.identity) as GameObject).GetComponent<FighterSpawn>();
    // generate 20 humans
    for(int i=0; i<humans.GetLength(0); i++) {
      humans[i] = (Instantiate(Resources.Load("Prefabs/HumanPrefab"),
        new Vector3(Random.Range(br.x - 2.0f,tl.x + 2.0f) , groundStart.y + 3.0f, 0),
        Quaternion.identity) as GameObject).GetComponent<Human>();
    }


    // generate the ship
    // ship = Instantiate(Resources.Load("Prefabs/ShipPrefab"),
    //     new Vector3(0, br.y + 4, alienStart.z),
    //     Quaternion.identity) as Ship;

    Vector3 alienOffset = new Vector3(-(tl.x - br.x) / 2 / cols, (tl.y - br.y) / 3 / rows, 0);
    Vector3 platformOffset = new Vector3(-(tl.x - br.x) / 5,
        br.y + 2.5f * 4.0f, 0);
    // generate aliens
    for(int i = 0; i < rows; i++) {
      for(int j = 0; j < cols; j++) {
        aliens[i, j] = (Instantiate(Resources.Load("Prefabs/AlienPrefab"),
            new Vector3(alienStart.x + alienOffset.x * j, alienStart.y - alienOffset.y * i, alienStart.z),
            Quaternion.identity) as GameObject).GetComponent<Alien>();
      }
    }
    // generate platforms
    for(int i = 0; i < platformCount ; i++) {
      platforms[i] = (Instantiate(Resources.Load("Prefabs/PlatformPrefab"),
          new Vector3(tl.x + platformOffset.x * (i+1), platformOffset.y, alienStart.z),
          Quaternion.identity) as GameObject).GetComponent<Platform>();
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

  public void freeHumans(){
    for(int i=0; i<humans.GetLength(0);i++) {
      humans[i].floatDown();
    }
  }

  public void checkHumans(){
    if(ufo == null)
      return;
    // if already moving, ignore
    if(ufo.moving)
      return;
    bool move = true;
    // if any human is floating, don't start it up
    Debug.Log("CHECKING");
    for(int i=0; i<humans.GetLength(0);i++) {
      if(humans[i].floating == true)
        return;
    }
    ufo.start();
  }

	void Update () {
    checkHumans();
    // camera logic
    if(Input.GetButtonDown("Camera")) {
      // switch camera
      fpcam.active = !fpcam.active;
      maincam.active = !maincam.active;
    }


    // check the ufo, if it exists, check if its out of boundaries
    if(ufo != null) {
      if(ufo.transform.position.x < tl.x || ufo.transform.position.x > br.x) {
        ufo.die();
      }
      // check, for each human, if it is within the bounds of the ufo's position,
      else {
        for(int i=0; i <humans.GetLength(0); i++) {
          if(humans[i] != null) {
            if(humans[i].transform.position.x <= ufo.transform.position.x + ufo.renderer.bounds.size.x/2 &&
               humans[i].transform.position.x >= ufo.transform.position.x - ufo.renderer.bounds.size.x/2) {
              humans[i].floatUp();
              ufo.stop();
            }
          }
        }
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
      if(a == null) {
        continue;
      }
      if(a.vertical) {
        break;
      }
      // if horizontal check to see if hit edge
      if((!a.right && a.transform.position.x - a.getVelocity() < tl.x) || (a.right && a.transform.position.x + a.getVelocity() > br.x)) {
        foreach(Alien b in aliens) {
          b.changeDirection();
        }
        break;
      }
    }
	}
}
