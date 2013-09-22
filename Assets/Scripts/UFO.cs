using UnityEngine;
using System.Collections;

public class UFO : MonoBehaviour {

  public Vector3 thrust;
  public bool moving = false;
	// Use this for initialization
	void Start () {
    Debug.Log("HELLOOOOOOOOO");
    thrust.x = 400f;
    start();
	}

	// Update is called once per frame
	void Update () {
	}
  public void die() {
    GameObject g = GameObject.Find("Environment");
    Environment globalObj = g.GetComponent<Environment>();
    globalObj.freeHumans();
    // set all humans to be not floating
    Destroy(gameObject);
  }
  public void start(){
    // go right
    if(Random.Range(0,1) < 0.5)
      gameObject.rigidbody.AddRelativeForce(thrust);
    else
      gameObject.rigidbody.AddRelativeForce(-thrust);
    moving = true;
  }
  public void stop() {
    gameObject.rigidbody.velocity = new Vector3(0,0,0);
    moving = false;
  }

  void OnCollisionEnter(Collision collision) {

  }

}
