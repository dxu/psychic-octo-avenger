using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {

  public bool activated = false;
  public Player1 player;
  public float life = 100.0f;
	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
    if(life <= 0) {
      life = 0f;
    }
    else if (life >= 100) {
      life = 100.0f;
    }
	}
}
