using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

  public Quaternion heading;
  public Vector3 thrust;
  public Ship ship; // ship that shot it
	void Start() {
    // thrust.y = 4000.0f;
    gameObject.rigidbody.MoveRotation(heading);
    gameObject.rigidbody.AddRelativeForce(thrust);
	}

	// Update is called once per frame
	void Update() {

    // check if its out of bounds, if it is dieeee
    gameObject.transform.position += new Vector3(0, 0.5f, 0);

    Vector3 view = Camera.main.WorldToViewportPoint(gameObject.transform.position);
    if(view.x >= 1.0f || view.x <= 0.0f ||
       view.y >= 1.0f || view.y <= 0.0f) {
      die();
    }

	}

  void OnCollisionEnter(Collision collision) {
    Collider collider = collision.collider;
    if(collider.CompareTag("Alien")) {
      Alien enemy = collider.gameObject.GetComponent<Alien>();
      enemy.die();
      die();
    }
    else if(collider.CompareTag("UFO")) {
      UFO ufo = collider.gameObject.GetComponent<UFO>();
      ufo.die();
      GameObject g = GameObject.Find("Environment");
      Environment globalObj = g.GetComponent<Environment>();
      globalObj.score += Random.Range(50, 200);
      die();
    }
    else if(collider.CompareTag("Shield")) {
      Shield shield = collider.gameObject.GetComponent<Shield>();
      shield.takeDamage();
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
