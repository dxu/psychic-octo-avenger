using UnityEngine;
using System.Collections;

public class Player1 : MonoBehaviour {
  private LayerMask collisionMask;
  private float skin = 0.005f;

  private BoxCollider collider;
  private Vector3 size;
  private Vector3 center;
  private float verticalMovement;
  private Ray ray;
  private RaycastHit hit;

  private Vector3 speed = new Vector3(9.0f, 0, 0);// = new Vector3(3.0f, 0, 0);
  private Vector3 horizontalMovement;
  private float walkAcceleration = 6000.0f;
  private float walkDeAcc = 0.2f;
  private float walkDeAccVolx;
  private float jumpAcceleration = 1000.0f;
  private float airDragRatio = 0.3f;
	// Use this for initialization
  private float maxWalkSpeed = 10;
  private bool grounded;
  private bool doubleJump;
  private float maxSlope = 60;
  private float jumpHeight = 15;
	void Start () {

    collider = GetComponent<BoxCollider>();
    size = collider.size;
    center = collider.center;
    // Physics.gravity = new Vector3(0, -5, 0);
	}

	// Update is called once per frame
  void Update() {
    // vertical movement handling - physics not working
    if(grounded) {
      verticalMovement = 0;

      if(Input.GetButtonDown("Jump") && grounded) {
        // rigidbody.AddRelativeForce(0, jumpAcceleration, 0);
        verticalMovement = jumpHeight;
      }
    }
    else if(Input.GetButtonDown("Jump") && doubleJump){
      // rigidbody.AddRelativeForce(0, Mathf.Abs(Physics.gravity.y) + jumpAcceleration, 0);
      doubleJump = false;
    }
    verticalMovement += Physics.gravity.y * Time.deltaTime;
    moveVertical(verticalMovement * Time.deltaTime);
  }

  void moveVertical(float distance) {
    grounded = false;
    for(int i=0; i<3; i++) {
      float dir = Mathf.Sign(distance);
      // left center right rays
      float x = (transform.position.x + center.x - size.x / 2) + size.x / 2 * i;
      float y = transform.position.y + center.y + size.y / 2 * dir;

      ray = new Ray(new Vector2(x,y), new Vector2(0, dir));
      Debug.DrawRay(ray.origin, ray.direction);
      // Debug.Log(Physics.Raycast(ray, out hit, Mathf.Abs(distance), collisionMask));
      if(Physics.Raycast(ray, out hit, Mathf.Abs(distance), collisionMask)){
        Debug.Log("hello");
        float dst = Vector3.Distance(ray.origin, hit.point);
        if(dst > skin) {
          distance = -dst + skin;
        }
        else {
          distance = 0;
        }
        // Debug.Log(distance);
        grounded = true;
        doubleJump = true;
        break;
      }
      Debug.Log(hit);
    }
    transform.Translate(new Vector2(0, distance));
  }

	void FixedUpdate () {
    // Debug.Log(grounded);
    // set up max speed
    // horizontalMovement = new Vector3(rigidbody.velocity.x, rigidbody.velocity.y, 0);
    horizontalMovement = new Vector3(rigidbody.velocity.x, 0, 0);
    if(horizontalMovement.magnitude > maxWalkSpeed) {
      horizontalMovement = horizontalMovement.normalized * maxWalkSpeed;
    }
    // retain y velocity
    rigidbody.velocity = new Vector3(horizontalMovement.x, rigidbody.velocity.y, 0);

    if(grounded)
      rigidbody.AddRelativeForce(new Vector3(Input.GetAxisRaw("Horizontal")*walkAcceleration * Time.fixedDeltaTime, 0, 0));
    else {
      rigidbody.AddRelativeForce(new Vector3(Input.GetAxisRaw("Horizontal")*walkAcceleration * airDragRatio * Time.fixedDeltaTime, 0, 0));
    }

    // Debug.Log(new Vector3(Input.GetAxisRaw("Horizontal")*walkAcceleration * Time.fixedDeltaTime, 0, 0));


    // slow down
    if(grounded) {
      rigidbody.velocity = new Vector3(Mathf.SmoothDamp(rigidbody.velocity.x, 0.0f, ref walkDeAccVolx, walkDeAcc), rigidbody.velocity.y, 0);
    }


  }


  // void OnCollisionStay(Collision collision) {
  //   foreach(ContactPoint contact in collision.contacts) {
  //     if(Vector3.Angle(contact.normal, Vector3.up) < maxSlope){
  //       grounded = true;
  //     }
  //   }
  // }

  // void OnCollisionExit() {
  //   grounded = false;
  // }


}
