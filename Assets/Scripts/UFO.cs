using UnityEngine;
using System.Collections;

public class UFO : MonoBehaviour {

  public Vector3 thrust;
	// Use this for initialization
	void Start () {
    Debug.Log("HELLOOOOOOOOO");
    thrust.x = 400f;
    gameObject.rigidbody.AddRelativeForce(thrust);
	}

	// Update is called once per frame
	void Update () {

	}
  public void die() {
    Destroy(gameObject);
  }
}
