using UnityEngine;
using System.Collections;

public class Player1 : MonoBehaviour {

  private Vector3 speed = new Vector3(9.0f, 0, 0);// = new Vector3(3.0f, 0, 0);
  private Vector3 horizontalMovement;
  private float walkAcceleration = 6000.0f;
  private float walkDeAcc = 0.2f;
  private float walkDeAccVolx;
  private float jumpAcceleration = 500.0f;
	// Use this for initialization
  //
  private float maxWalkSpeed = 10;
  private bool grounded;
  private float maxSlope = 60;
	void Start () {

	}

	// Update is called once per frame
  void Update() {
  }
	void FixedUpdate () {
    // set up max speed
    // horizontalMovement = new Vector3(rigidbody.velocity.x, rigidbody.velocity.y, 0);
    horizontalMovement = new Vector3(rigidbody.velocity.x, 0, 0);
    if(horizontalMovement.magnitude > maxWalkSpeed) {
      horizontalMovement = horizontalMovement.normalized * maxWalkSpeed;
    }
    // retain y velocity
    rigidbody.velocity = new Vector3(horizontalMovement.x, rigidbody.velocity.y, 0);

    rigidbody.AddRelativeForce(new Vector3(Input.GetAxisRaw("Horizontal")*walkAcceleration * Time.fixedDeltaTime, 0, 0));

    // Debug.Log(new Vector3(Input.GetAxisRaw("Horizontal")*walkAcceleration * Time.fixedDeltaTime, 0, 0));


    // slow down
    if(Input.GetAxisRaw("Horizontal") == 0 && grounded) {
      rigidbody.velocity = new Vector3(Mathf.SmoothDamp(rigidbody.velocity.x, 0.0f, ref walkDeAccVolx, walkDeAcc), rigidbody.velocity.y, 0);
    }

    if(Input.GetAxisRaw("Horizontal") > 0) {
      // gameObject.rigidbody.AddRelativeForce(speed);
      // gameObject.transform.position += speed * 0.1f;
      // rigidbody.AddForce(new Vector3(Input.GetAxis("Horizontal")*10, 0, 0));
      // Debug.Log("moving");
      // Debug.Log(Input.GetAxis("Horizontal"));
      // rigidbody.velocity = speed;
    }
    if(Input.GetAxisRaw("Horizontal") < 0) {
      // gameObject.rigidbody.AddRelativeForce(-speed);
      // gameObject.transform.position -= speed * 0.1f;
      // rigidbody.AddForce(new Vector3(Input.GetAxis("Horizontal")*10, 0, 0));
      // Debug.Log("moving");
      // Debug.Log(Input.GetAxis("Horizontal"));
      // rigidbody.velocity = -speed;
    }

    if(Input.GetButtonDown("Jump") && grounded) {
      Debug.Log("inside");
      rigidbody.AddRelativeForce(0, jumpAcceleration, 0);
    }
    // if(Input.GetButtonDown("Fire1") && reloaded) {
    //   [> we don’t want to spawn a Bullet inside our ship, so some
    //   Simple trigonometry is done here to spawn the bullet
    //   at the tip of where the ship is pointed.
    //   */

    //   // get the Bullet Script Component of the new Bullet instance
    //   Bullet b = createBullet();
    //   // set the direction the Bullet will travel in
    //   Quaternion rot = Quaternion.Euler(new Vector3(0,rotation,0));
    //   b.heading = rot;
    //   reloaded = false;
    // }
  }

  void OnCollisionStay(Collision collision) {
    foreach(ContactPoint contact in collision.contacts) {
      if(Vector3.Angle(contact.normal, Vector3.up) < maxSlope)
        grounded = true;
    }
  }

  void OnCollisionExit() {
    grounded = false;
  }


}
