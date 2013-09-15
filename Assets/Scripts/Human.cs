using UnityEngine;
using System.Collections;

public class Human : MonoBehaviour {

  private bool direction; // true right, false left
  private float speed;
  private int health = 1;
	// Use this for initialization
	void Start () {
    // random start direction
    float rand = Random.Range(0, 1f);
    direction = rand < 0.5f;
	}

	// Update is called once per frame
	void Update () {
    speed = Random.Range(0, 0.1f);
    // random movement
    float r = Random.Range(0, 1.0f);
    if(r > 0.95f) {
      // flip direction
      direction = !direction;
    }
    gameObject.transform.position += new Vector3((direction ?speed : -speed), 0, 0);

	}

  private void die(){
    Destroy(gameObject);
  }

  public void takeDamage() {
    health -= 1;
    if(health == 0)
      die();
  }
}
