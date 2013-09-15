using UnityEngine;
using System.Collections;

public class BuilderAttack : MonoBehaviour {

	// Use this for initialization
	void Start () {

    renderer.enabled = false;
	}

	// Update is called once per frame
	void Update () {

    if(Input.GetButtonDown("Fire1-2")) {
      // generate a sword attack, right and middle
      BuilderAttack particles = (Instantiate(Resources.Load("Prefabs/BuilderAttackPrefab"),
            gameObject.transform.position + new Vector3(), Quaternion.identity) as GameObject).GetComponent<BuilderAttack>();
      renderer.enabled = true;
    }
	}
  void OnParticleCollision(GameObject obj) {
  }
}
