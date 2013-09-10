using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {


  private Vector3 speed;
  // reloaded should be set to true when bullet dies
  public bool reloaded;
  private int health = 3;
  public Camera fpcam;
  public Camera maincam;
	// Use this for initialization
	void Start() {
    speed.x = 3.0f;
    // Can i not set relaoded outside?
    reloaded = true;
    fpcam = GameObject.Find("FPCamera").GetComponent<Camera>();
    maincam = GameObject.Find("Main Camera").GetComponent<Camera>();
    Debug.Log(fpcam);
    Debug.Log(Camera.main);
    fpcam.active = false;
    maincam.active = true;

    // intro music
    // AudioClip bg = (AudioClip)Resources.Load("Audio/spaceinvaders1");
    // AudioSource.PlayClipAtPoint(bg, gameObject.transform.position);
	}

  public GameObject bullet;
  public float rotation;

  public void takeDamage() {
    health -= 1;

    GameObject g = GameObject.Find("Environment");
    Environment globalObj = g.GetComponent<Environment>();
    globalObj.lives = health;

    AudioClip fireSound = (AudioClip)Resources.Load("Audio/explosion");
    AudioSource.PlayClipAtPoint(fireSound, gameObject.transform.position);

    if(health == 0) {
      Debug.Log("YOU LOST THE GAME!");
      die();
    }

  }

  private void die() {
    Destroy(gameObject);
    // play the sound
    Application.LoadLevel("GameOverScene");
  }
  private bool cam = true; // true maincam, false fpcam
	// Update is called once per frame
  // Don't use physics to maintain the "jerkiness" of the original Space Invaders.
	void Update() {
    // Do not do shit for up and down
    // if(Input.GetAxisRaw("Vertical") > 0) {
    //   gameObject.rigidbody.AddRelativeForce(forceVector);
    // }
    //
    if(Input.GetAxisRaw("Horizontal") > 0) {
      // gameObject.rigidbody.AddRelativeForce(speed);
      gameObject.transform.position += speed * 0.1f;
    }
    if(Input.GetAxisRaw("Horizontal") < 0) {
      // gameObject.rigidbody.AddRelativeForce(-speed);
      gameObject.transform.position -= speed * 0.1f;
    }
    if(Input.GetButtonDown("Fire1") && reloaded) {
      /* we don’t want to spawn a Bullet inside our ship, so some
      Simple trigonometry is done here to spawn the bullet
      at the tip of where the ship is pointed.
      */

      // get the Bullet Script Component of the new Bullet instance
      Bullet b = createBullet();
      // set the direction the Bullet will travel in
      Quaternion rot = Quaternion.Euler(new Vector3(0,rotation,0));
      b.heading = rot;
      reloaded = false;
    }
    if(Input.GetButtonDown("Fire2")) {
      // switch camera
      fpcam.active = !fpcam.active;
      maincam.active = !maincam.active;
    }
	}
  private Bullet createBullet() {
    Vector3 spawnPos = gameObject.transform.position;
    spawnPos.y += 1.5f; // * Mathf.Sin(rotation * Mathf.PI/180);

    GameObject newBullet = Instantiate(Resources.Load("Prefabs/BulletPrefab"), spawnPos, Quaternion.identity) as GameObject;
    Bullet bulletComponent = newBullet.GetComponent<Bullet>();
    bulletComponent.ship = gameObject.GetComponent<Ship>();


    // play the sound
    AudioClip fireSound = (AudioClip)Resources.Load("Audio/shoot");
    AudioSource.PlayClipAtPoint(fireSound, gameObject.transform.position);





    return bulletComponent;
  }
}
