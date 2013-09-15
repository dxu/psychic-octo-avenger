using UnityEngine;
using System.Collections;

public class Wood : MonoBehaviour {

	// Use this for initialization
	void Start () {
    // add an initial velocity in a random direction up
    rigidbody.velocity = new Vector3(Random.Range(-10.0f, 10f), 0, 0);

	}

	// Update is called once per frame
	void Update () {

	}
  void OnCollisionEnter(Collision collision){
    Collider collider = collision.collider;
    if(collider.CompareTag("Player")) {
      Player1 player = collider.gameObject.GetComponent<Player1>();
      player.getWood();
      die();
    }
    // TODO: Refactor: should be hitting player
    else if(collider.CompareTag("Shield")) {
      Player1 player = collider.gameObject.GetComponent<Shield>().player;
      player.getWood();
      die();
    }
  }
  void die() {
    Destroy(gameObject);
  }
}
