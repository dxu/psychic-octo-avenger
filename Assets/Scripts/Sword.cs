using UnityEngine;
using System.Collections;

public class Sword : MonoBehaviour {

  private float delay = 0.05f;
  private float timer;
	// Use this for initialization
	void Start () {

    gameObject.active = false;
	}

	// Update is called once per frame
	void Update () {

    // if the delay time is up, hide
    if(Time.time > timer && timer != 0) {
      gameObject.active = false;
    }

    if(Input.GetButtonDown("Fire1-2")) {
      Debug.Log("INSIDE");
      // generate a sword attack, right and middle
      // BuilderAttack particles = (Instantiate(Resources.Load("Prefabs/BuilderAttackPrefab"),
      //       gameObject.transform.position + new Vector3(), Quaternion.identity) as GameObject).GetComponent<BuilderAttack>();
      gameObject.active = true;
      timer = Time.time + delay;
    }
	}
  void OnParticleCollision(GameObject obj) {
  }
}
