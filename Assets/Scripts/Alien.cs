using UnityEngine;
using System.Collections;

public class Alien : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

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
