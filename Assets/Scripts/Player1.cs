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

  private bool builder = true;
  private int wood = 5;

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
    Physics.gravity = new Vector3(0, -50, 0);
	}

	// Update is called once per frame
  void Update() {
  }

  void moveVertical(float distance) {
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


    // vertical movement handling
    if(grounded) {
      verticalMovement = 0;

      if(Input.GetButtonDown("Jump") && grounded) {
        rigidbody.AddRelativeForce(0, jumpAcceleration, 0);
        verticalMovement = jumpHeight;
      Debug.Log("JUMPED");
      }
    }
    else if(Input.GetButtonDown("Jump") && doubleJump){
      rigidbody.velocity = new Vector3(rigidbody.velocity.x,0,rigidbody.velocity.z);
      rigidbody.AddRelativeForce(0, Mathf.Abs(Physics.gravity.y) + jumpAcceleration, 0);
      doubleJump = false;
      Debug.Log("ho");
    }
    verticalMovement += Physics.gravity.y * Time.deltaTime;
    moveVertical(verticalMovement * Time.deltaTime);


    // keypresses
    if(Input.GetButtonDown("Fire1")) {
      if(wood > 0) {
        // generate a shield
        Shield shield = (Instantiate(Resources.Load("Prefabs/ShieldPrefab"),
            gameObject.transform.position, Quaternion.identity) as GameObject).GetComponent<Shield>();
        wood -= 1;
      }
    }

  }


  void OnCollisionEnter(Collision collision) {
    foreach(ContactPoint contact in collision.contacts) {
      if(Vector3.Angle(contact.normal, Vector3.up) < maxSlope){
        grounded = true;
        doubleJump = true;
      }
    }

    Collider collider = collision.collider;
    if(collider.CompareTag("BuilderSpawn")) {
      Debug.Log("Now a buidler");
      builder = true;
    }
    else if(collider.CompareTag("FighterSpawn")) {
      Debug.Log("Now a gfighter");
      builder = false;
    }
    // if it's the builderspawn

  }

  void OnCollisionExit() {
    grounded = false;
  }


}
