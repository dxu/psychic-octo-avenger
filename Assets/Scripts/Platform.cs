using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {

  private int health = 3;
	// Use this for initialization
	void Start () {

	}

  public void takeDamage() {
    health -= 1;
    if(health == 0) {
      die();
    }
  }

	// Update is called once per frame
	void Update () {
	}
  void die() {
    Destroy(gameObject);
  }
}
