using UnityEngine;
using System.Collections;

public class Human : MonoBehaviour {

  private bool direction; // true right, false left
  private float speed;
  private float vert_speed = 0.1f;
  private int health = 1;
  public bool floating = false;
  AudioClip floatSound;
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
    if(!floating) {
      gameObject.transform.position += new Vector3((direction ?speed : -speed), 0, 0);
    }
    else {
      gameObject.rigidbody.velocity = new Vector3(0, gameObject.rigidbody.velocity.y, 0);
      // gameObject.transform.position += new Vector3(0, vert_speed, 0);
      gameObject.rigidbody.AddForce(new Vector3(0, 420f, 0));
    }
	}

  private void die(){

    AudioClip scream =  (AudioClip)Resources.Load("Audio/scream");
    AudioSource.PlayClipAtPoint(scream, gameObject.transform.position);
    // update environment
    GameObject g = GameObject.Find("Environment");
    Environment globalObj = g.GetComponent<Environment>();
    globalObj.humanCount -= 1;
    floating = false;
    Destroy(gameObject);
  }

  public void takeDamage() {
    health -= 1;
    if(health == 0)
      die();
  }

  public void floatUp() {
    if(!floating) {
      floating = true;
      floatSound =  (AudioClip)Resources.Load("Audio/ufo_lowpitch");
      AudioSource.PlayClipAtPoint(floatSound, gameObject.transform.position);
    }
  }

  public void floatDown() {
    floating = false;
  }

  void OnCollisionEnter(Collision collision) {
    Collider collider = collision.collider;
    if(collider.CompareTag("UFO")) {
      UFO ufo = collider.gameObject.GetComponent<UFO>();
      ufo.rigidbody.velocity = new Vector3(0,0,0);
      die();
    }
    if(collider.CompareTag("Alien")) {
      Alien alien = collider.gameObject.GetComponent<Alien>();
      // alien.rigidbody.velocity = new Vector3(0,0,0);
      die();
    }
  }
}
