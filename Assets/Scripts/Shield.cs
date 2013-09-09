using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {

  private int health = 10;
	// Use this for initialization
	void Start () {

	}

  public void takeDamage() {
    health -= 1;
    Debug.Log("took damage!");
    Debug.Log(health);
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
