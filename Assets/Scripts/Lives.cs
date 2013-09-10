using UnityEngine;
using System.Collections;

public class Lives : MonoBehaviour {

  Environment globalObj;
  GUIText livesText;
	// Use this for initialization
	void Start () {
    GameObject g = GameObject.Find("Environment");
    globalObj = g.GetComponent<Environment>();
    livesText = gameObject.GetComponent<GUIText>();
	}

	// Update is called once per frame
	void Update () {
    livesText.text = "Lives: " + globalObj.lives.ToString();
	}
}
