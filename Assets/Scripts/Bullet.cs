using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

  public Quaternion heading;
  public Vector3 thrust;
  public Ship ship; // ship that shot it
	void Start() {
    thrust.y = 400.0f;
    gameObject.rigidbody.MoveRotation(heading);
    gameObject.rigidbody.AddRelativeForce(thrust);
	}

	// Update is called once per frame
	void Update() {

    // check if its out of bounds, if it is dieeee
    // if gameObject.position
    //   Camera.main
    // float width = Camera.main.GetScreenWidth();
    // float height = Camera.main.GetScreenHeight();

    Vector3 view = Camera.main.WorldToViewportPoint(gameObject.transform.position);
    if(view.x >= 1.0f || view.x <= 0.0f ||
       view.y >= 1.0f || view.y <= 0.0f) {
      die();
    }

	}

  void OnCollisionEnter(Collision collision) {
    Collider collider = collision.collider;
    Debug.Log(collision.gameObject);
    if(collider.CompareTag("Alien")) {
      Alien enemy = collider.gameObject.GetComponent<Alien>();
      enemy.die();
      die();
    }
    else {
      Debug.Log("Collided with " + collider.tag);
    }
  }

  void die() {
    ship.reloaded = true;
    Destroy(gameObject);
  }
}
