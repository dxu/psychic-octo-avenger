using UnityEngine;
using System.Collections;

public class AlienBullet : MonoBehaviour {

  Vector3 thrust;
  float speed = 0.25f;
  private bool reflected = false;
  public Vector3 direction = new Vector3(0, -1, 0);
  public Alien ship;
	// Use this for initialization
	void Start () {
    thrust.y = 0.0f;
	}


  void OnCollisionEnter(Collision collision) {
    Collider collider = collision.collider;
    if(collider.CompareTag("Ship")) {
      Ship player = collider.gameObject.GetComponent<Ship>();
      player.takeDamage();
      die();
    }
    else if(collider.CompareTag("Platform")) {
      Platform platform = collider.gameObject.GetComponent<Platform>();
      platform.takeDamage();
      die();
    }
    else if(collider.CompareTag("Alien")) {
      if(reflected) {
        Alien alien = collider.gameObject.GetComponent<Alien>();
        alien.takeDamage();
        die();
      }
    }
    else if(collider.CompareTag("Shield")) {

      // reflect only if player isn't shielded
      Shield shield = collider.GetComponent<Shield>();
      if(shield.activated) {
        reflected = true;
        Vector3 avg = new Vector3(0,0,0);
        // average over the collision's contact points is the normal
        foreach(ContactPoint contact in collision.contacts) {
          avg += contact.normal;
        }
        avg /= collision.contacts.GetLength(0);
        Debug.Log("his");
        Debug.Log(avg);
        direction = Vector3.Reflect(gameObject.transform.position, avg);
        direction.Normalize();
      }
      else {
        shield.transform.parent.GetComponent<Player1>().takeDamage();
      }

    }
    else if(collider.CompareTag("Player")){
      // TODO: Figure out why it never hits player when shield is down
    	Debug.Log("Hit player. should not happen");
    }
    else if(collider.CompareTag("Ground")){
      die();
    }
    else {
      Debug.Log("Collided with " + collider.tag);
    }
  }

  // void OnTriggerEnter(Collider collider){
  //   if(collider.CompareTag("Shield")) {

  //     Vector3 avg = new Vector3(0,0,0);
  //     // average over the collision's contact points is the normal
  //     foreach(ContactPoint contact in collision.contacts) {
  //       avg += contact.normal;
  //     }
  //     avg /= collision.contacts.GetLength(0);
  //     Debug.Log("his");
  //     Debug.Log(avg);
  //   }


  // }

	// Update is called once per frame
	void Update () {
    gameObject.transform.position += speed * direction;

	}

  void die() {
    Destroy(gameObject);
  }
}
