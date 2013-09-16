using UnityEngine;
using System.Collections;

public class Alien : MonoBehaviour {
  private float velocity;
  // true is vertical, false is horizontal
  public bool vertical;
  // true is right, false is left
  public bool right = true;

  private int health = 1;

  private float delay = 0.5f;
  private float timer;
  private float fireChance = 0.0003f;

	// Use this for initialization
	void Start () {
    // velocity = 0.05f;
    velocity = 0.03f;
    vertical = false;
	}
  public float getVelocity() {
    return velocity;
  }

	// Update is called once per frame
	void Update () {

    // if the delay time is up
    if(Time.time > timer && timer != 0) {
      vertical = false;
      // flip the direction
    }

    if(vertical)
      gameObject.transform.position -= new Vector3(0, velocity, 0);
    else {
      if(right)
        gameObject.transform.position += new Vector3(velocity, 0, 0);
      else
        gameObject.transform.position -= new Vector3(velocity, 0, 0);
    }
    float chance = Random.Range(0.0f, 1.0f);
    if(chance < fireChance)
      fire();
	}
  // called when direction is outside of camera
  public void changeDirection() {
    // vertical = true;
    right = !right;
    // timer = Time.time + delay;

  }

  public void takeDamage() {
    health -= 1;
    if(health == 0) {
      // generate a random bunch of wood

      for(int i=0; i<Random.Range(0, 9.0f); i++){
        Wood wood = Instantiate(Resources.Load("Prefabs/WoodPrefab"),
            gameObject.transform.position, Quaternion.identity) as Wood;
      }

      die();
    }
  }

  void moveLeft() {
    // gameObject.transform.position += Vector3.left;
    right = false;
  }
  void moveRight() {
    // gameObject.transform.position += Vector3.right;
    right = true;
  }
  void moveDown() {
    gameObject.transform.position += Vector3.down;
  }
  void moveUp() {
    gameObject.transform.position += Vector3.up;
  }
  AlienBullet fire() {
    Vector3 spawnPos = gameObject.transform.position;
    spawnPos.y += 1.5f; // * Mathf.Sin(rotation * Mathf.PI/180);

    GameObject newBullet = Instantiate(Resources.Load("Prefabs/AlienBulletPrefab"), spawnPos, Quaternion.identity) as GameObject;
    AlienBullet bulletComponent = newBullet.GetComponent<AlienBullet>();
    bulletComponent.ship = gameObject.GetComponent<Alien>();

    return bulletComponent;
  }
  public void die() {
    Destroy(gameObject);
    // update global score
    GameObject g = GameObject.Find("Environment");
    Environment globalObj = g.GetComponent<Environment>();
    globalObj.score += 10;
    AudioClip fireSound = (AudioClip)Resources.Load("Audio/invaderkilled");
    AudioSource.PlayClipAtPoint(fireSound, gameObject.transform.position);
  }
}
