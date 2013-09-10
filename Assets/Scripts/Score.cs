using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {

  Environment globalObj;
  GUIText scoreText;
	// Use this for initialization
	void Start () {
    GameObject g = GameObject.Find("Environment");
    globalObj = g.GetComponent<Environment>();
    scoreText = gameObject.GetComponent<GUIText>();
	}

	// Update is called once per frame
	void Update () {
    scoreText.text = "Score: " + globalObj.score.ToString();
	}
}
