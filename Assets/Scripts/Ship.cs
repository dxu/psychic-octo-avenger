using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {


  private Vector3 speed;
  // reloaded should be set to true when bullet dies
  public bool reloaded;
	// Use this for initialization
	void Start() {
    speed.x = 4.0f;
    // Can i not set relaoded outside?
    reloaded = true;
	}

  public GameObject bullet;
  public float rotation;

	// Update is called once per frame
  // Don't use physics to maintain the "jerkiness" of the original Space Invaders.
	void Update() {
    // Do not do shit for up and down
    // if(Input.GetAxisRaw("Vertical") > 0) {
    //   gameObject.rigidbody.AddRelativeForce(forceVector);
    // }
    //
    if(Input.GetAxisRaw("Horizontal") > 0) {
      // gameObject.rigidbody.AddRelativeForce(speed);
      // Debug.Log(gameObject.transform.x);
      gameObject.transform.position += Vector3.right * 0.1f;
    }
    if(Input.GetAxisRaw("Horizontal") < 0) {
      // gameObject.rigidbody.AddRelativeForce(-speed);
      gameObject.transform.position += Vector3.left * 0.1f;
    }
    if(Input.GetButtonDown("Fire1") && reloaded) {
      /* we don’t want to spawn a Bullet inside our ship, so some
      Simple trigonometry is done here to spawn the bullet
      at the tip of where the ship is pointed.
      */

      // get the Bullet Script Component of the new Bullet instance
      Bullet b = createBullet();
      // set the direction the Bullet will travel in
      Quaternion rot = Quaternion.Euler(new Vector3(0,rotation,0));
      b.heading = rot;
      reloaded = false;
    }
	}

  private Bullet createBullet() {
    // Debug.Log("heloo");
    Vector3 spawnPos = gameObject.transform.position;
    spawnPos.y += 1.5f; // * Mathf.Sin(rotation * Mathf.PI/180);

    GameObject newBullet = Instantiate(Resources.Load("Prefabs/BulletPrefab"), spawnPos, Quaternion.identity) as GameObject;
    Bullet bulletComponent = newBullet.GetComponent<Bullet>();
    // Debug.Log(gameObject);
    // Debug.Log(newBullet);
    // Debug.Log(bulletComponent);
    bulletComponent.ship = gameObject.GetComponent<Ship>();

    // Debug.Log("in hereee");
    // Debug.Log(bulletComponent);

    return bulletComponent;
  }
}
