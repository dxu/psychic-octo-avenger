using UnityEngine;
using System.Collections;

public class Alien : MonoBehaviour {
  private float velocity;
  // true is vertical, false is horizontal
  public bool vertical;
  // true is right, false is left
  public bool right = true;

  private float delay = 0.5f;
  private float timer;

	// Use this for initialization
	void Start () {
    // velocity = 0.05f;
    velocity = 0.1f;
    vertical = false;
	}
  public float getVelocity() {
    return velocity;
  }

	// Update is called once per frame
	void Update () {

    // Debug.Log("outside");
    // Debug.Log(Time.time);
    // Debug.Log(timer);
    // Debug.Log(vertical);
    // if the delay time is up
    if(Time.time > timer && timer != 0) {
      vertical = false;
      Debug.Log(right);
      // flip the direction
      // Debug.Log("inside");
      // Debug.Log(Time.time);
    }

    if(vertical)
      gameObject.transform.position -= new Vector3(0, velocity, 0);
    else
      if(right)
        gameObject.transform.position += new Vector3(velocity, 0, 0);
      else
        gameObject.transform.position -= new Vector3(velocity, 0, 0);
	}
  // called when direction is outside of camera
  public void changeDirection() {
    vertical = true;
    right = !right;
    // Debug.Log("changing");
    timer = Time.time + delay;
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
  public void die() {
    Destroy(gameObject);
  }
}
