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

  private bool builder = false;
  public int wood = 100;

  private int health = 100;
  private bool dead = false;


  public int id = 1;

  private Vector3 speed = new Vector3(9.0f, 0, 0);// = new Vector3(3.0f, 0, 0);
  private Vector3 horizontalMovement;
  private float walkAcceleration = 6000.0f;
  private float walkDeAcc = 0.2f;
  private float walkDeAccVolx;
  private float builderJumpAcc = 1000.0f;
  private float fighterJumpAcc = 850.0f;
  private float jumpAcc;
  private float airDragRatio = 0.3f;
	// Use this for initialization
  private float maxWalkSpeed = 10;
  private bool grounded;
  private bool doubleJump;
  private float maxSlope = 60;
  private float jumpHeight = 15;

  private Vector3 localScale;

  Sword sword;
  public Shield shield;


	void Start () {
    collider = GetComponent<BoxCollider>();
    size = collider.size;
    center = collider.center;
    Physics.gravity = new Vector3(0, -50.0f, 0);
    sword = gameObject.GetComponentInChildren<Sword>();
    shield = gameObject.GetComponentInChildren<Shield>();
    shield.player = gameObject.GetComponent<Player1>();
    localScale = gameObject.transform.localScale;
    // initialize stats
    updateClass();
	}

  public void takeDamage(){
    Debug.Log("TOOK DAMAGE");
    health -= 1;
    if(health == 0)
      die();
  }

  // changes class stats
  void updateClass(){
    if(builder) {
      jumpAcc = builderJumpAcc;
    }
    else {
      jumpAcc = fighterJumpAcc;
    }
  }

  private float deathDelay = 5.0f;
  private float deathTimer;
  public string deathDisplayTime = "";
  private float delay = 0.05f;
  private float swordTimer;

  private float shieldDelay = 0.01f;
  private float shieldTimer = Time.time;
	// Update is called once per frame
  void Update() {

    // if the delay time is up, hide
    if(Time.time > shieldTimer) {
      // requires a little bit of buffer so you can't just spam it
      if(shield.activated){
        shield.life -= 1.0f;
      }
      else {
        shield.life += 0.3f;
      }
      shieldTimer = Time.time + shieldDelay;
    }

    // if the delay time is up, hide
    if(Time.time > swordTimer && swordTimer != 0) {
      sword.active = false;
    }
    if(Time.time > deathTimer && deathTimer != 0) {
      Debug.Log("SHOULD BE ALIVEEE");
      // gameObject.active = true;
      dead = false;
      health = 1;
      gameObject.transform.localScale = localScale;
      deathDisplayTime = "";
    } else if(deathTimer != 0 && dead){
      // if not in the beginning, but is dead
      deathDisplayTime = (Mathf.Round((deathTimer - Time.time) * 100f) / 100f).ToString();

    }

    //
    if(dead) {
      return;
    }

    // keypresses
    if((Input.GetButtonDown("Fire1") && id == 1) || (Input.GetButtonDown("Fire2") && id == 2)) {
      if(builder) {
        if(wood > 0) {
          // generate a shield
          Platform platform = (Instantiate(Resources.Load("Prefabs/PlatformPrefab"),
              gameObject.transform.position + new Vector3(0, renderer.bounds.size.y/2 * 2.5f , 0),
              Quaternion.identity) as GameObject).GetComponent<Platform>();
          wood -= 1;
        }
      }
    }
    else if((Input.GetButtonDown("Fire1-2") && id == 1) || (Input.GetButtonDown("Fire2-2") && id == 2)) {
      // grab child
      // activate sword - class behavior determind in the sword class
      if(builder){
        sword.active = true;
        swordTimer = Time.time + delay;
      }
      else if(shield.life > 20 && !shield.activated){
        activateShield();
      }
      else if(shield.activated) {
        deactivateShield();
      }
    }

    // hide the shield on button up
    if((Input.GetButtonUp("Fire1-2") && id == 1) || (Input.GetButtonUp("Fire2-2") && id == 2)) {
      deactivateShield();
    }
  }

  void activateShield(){
    shield.transform.localScale = new Vector3(2.0f,2.0f,0);
    shield.transform.GetComponent<SphereCollider>().radius = 0.5f;
    // shield.collider.enabled = true;
    shield.activated = true;
    // requires 20 so you can't just spam it
    shield.life -= 20.0f;
  }

  void deactivateShield(){
    shield.transform.localScale = new Vector3(0,0,0);
    shield.transform.GetComponent<SphereCollider>().radius = 0.0f;
    // shield.collider.enabled = false;
    shield.activated = false;
  }

  void moveVertical(float distance) {
  }

	void FixedUpdate () {
    // don't do anything if dead
    if(dead) {
      return;
    }

    // set up max speed
    horizontalMovement = new Vector3(rigidbody.velocity.x, 0, 0);
    if(horizontalMovement.magnitude > maxWalkSpeed) {
      horizontalMovement = horizontalMovement.normalized * maxWalkSpeed;
    }
    // retain y velocity
    rigidbody.velocity = new Vector3(horizontalMovement.x, rigidbody.velocity.y, 0);

    if(grounded) {
      if(id == 1) {
        rigidbody.AddForce(new Vector3(Input.GetAxisRaw("Horizontal")*walkAcceleration * Time.fixedDeltaTime, 0, 0));
      }
      else if (id == 2) {
        rigidbody.AddForce(new Vector3(Input.GetAxis("Horizontal-2")*walkAcceleration * Time.fixedDeltaTime, 0, 0));
      }
    }
    else {
      if(id == 1) {
        rigidbody.AddForce(new Vector3(Input.GetAxisRaw("Horizontal")*walkAcceleration * airDragRatio * Time.fixedDeltaTime, 0, 0));
      }
      else if(id == 2) {
        rigidbody.AddForce(new Vector3(Input.GetAxisRaw("Horizontal-2")*walkAcceleration * airDragRatio * Time.fixedDeltaTime, 0, 0));
      }
    }


    // slow down
    if(grounded) {
      rigidbody.velocity = new Vector3(Mathf.SmoothDamp(rigidbody.velocity.x, 0.0f, ref walkDeAccVolx, walkDeAcc), rigidbody.velocity.y, 0);
    }

    // vertical movement handling
    if(grounded) {
      verticalMovement = 0;

      if(((Input.GetButtonDown("Jump") && id == 1) ||
            (Input.GetButtonDown("Jump-2") && id == 2)) && grounded ) {
        // reset vertical velocity to get full jump height
        rigidbody.velocity = new Vector3(rigidbody.velocity.x,0,rigidbody.velocity.z);
        rigidbody.AddForce(0, jumpAcc, 0);
        verticalMovement = jumpHeight;
      }
    }
    else if(((Input.GetButtonDown("Jump") && id == 1) || (Input.GetButtonDown("Jump-2") && id == 2)) && doubleJump){
      rigidbody.AddTorque(new Vector3(0, 0, 3000.0f));
      rigidbody.velocity = new Vector3(rigidbody.velocity.x,0,rigidbody.velocity.z);
      rigidbody.AddForce(0, Mathf.Abs(Physics.gravity.y) + jumpAcc, 0);
      if(!builder)
        doubleJump = false;
    }
    verticalMovement += Physics.gravity.y * Time.deltaTime;
    moveVertical(verticalMovement * Time.deltaTime);
  }


  void OnCollisionEnter(Collision collision) {
    Collider collider = collision.collider;
    // right itself
    if(!collider.CompareTag("Alien")) {
      grounded = true;
      doubleJump = true;
    }

    if(collider.CompareTag("BuilderSpawn")) {
      Debug.Log("Now a buidler");
      builder = true;
      updateClass();
    }
    else if(collider.CompareTag("FighterSpawn")) {
      Debug.Log("Now a gfighter");
      builder = false;
      updateClass();
    }
    else if(collider.CompareTag("Alien")) {
      // if shielded
      if(shield.activated){
        Alien alien = collider.GetComponent<Alien>();
        alien.die();
      }
      else {
        takeDamage();
      }
    }
    else if(collider.CompareTag("UFO")) {
      // if shielded
      if(shield.activated){
        UFO ufo = collider.GetComponent<UFO>();
        ufo.die();
      }
      else {
        takeDamage();
      }
    }
    // if it's the builderspawn

  }

  void OnCollisionExit() {
    grounded = false;
  }

  public void getWood(){
    wood += 1;
  }

  void die(){
    // Destroy(gameObject);
    Debug.Log("RESPAWNING");
    gameObject.transform.localScale = new Vector3(0,0,0);
    dead = true;
    deathTimer = Time.time + deathDelay;
  }

}
