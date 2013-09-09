using UnityEngine;
using System.Collections;

public class Alien : MonoBehaviour {
  public float velocity;
  // true is vertical, false is horizontal
  public bool direction;

  private float delay = 2;
  private float timer;

	// Use this for initialization
	void Start () {
    velocity = 0.05f;
    direction = false;
	}

	// Update is called once per frame
	void Update () {
    Debug.Log(velocity);

    // if the delay time is up
    if(Time.time > timer)
      direction = false;

    if(direction)
      gameObject.transform.position -= new Vector3(0, velocity, 0);
    else
      gameObject.transform.position += new Vector3(velocity, 0, 0);
	}
  // called when direction is outside of camera
  void changeDirection() {
    direction = true;
    timer = Time.time + delay;
  }

  void moveLeft() {
    gameObject.transform.position += Vector3.left;
  }
  void moveRight() {
    gameObject.transform.position += Vector3.right;
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
