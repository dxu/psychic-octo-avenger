using UnityEngine;
using System.Collections;

public class AlienBullet : MonoBehaviour {

  Vector3 thrust;
  Vector3 velocity = new Vector3(0, -0.25f, 0);
  public Alien ship;
	// Use this for initialization
	void Start () {
    thrust.y = 0.0f;
	}


  void OnCollisionEnter(Collision collision) {
    Collider collider = collision.collider;
    // Debug.Log(collider.tag);
    if(collider.CompareTag("Ship")) {
      Ship player = collider.gameObject.GetComponent<Ship>();
      player.takeDamage();
      die();
    }
    else if(collider.CompareTag("Shield")) {
      Shield shield = collider.gameObject.GetComponent<Shield>();
      shield.takeDamage();
      die();
    }
    else if(collider.CompareTag("Alien")) {
    }
    else {
      Debug.Log("Collided with " + collider.tag);
    }
  }

	// Update is called once per frame
	void Update () {
    gameObject.transform.position += velocity;

	}

  void die() {
    Destroy(gameObject);
  }
}
