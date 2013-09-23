using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

  Environment globalObj;
  GUIText scoreText;
	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
	}

  void OnGUI() {
    GUILayout.BeginArea(new Rect(10, Screen.height / 2 + 100, Screen.width -10, 200));

    GameObject g = GameObject.Find("Text");
    GUIText globalObj = g.GetComponent<GUIText>();

    if(PlayerPrefs.GetFloat("New") == PlayerPrefs.GetFloat("High"))
      // got a new high score!
      globalObj.text = "You got a new high score of " + PlayerPrefs.GetFloat("New").ToString() + ". Your old high score was " + PlayerPrefs.GetFloat("Old") + "! Try again?";
    else
      globalObj.text = "You got a score of " + PlayerPrefs.GetFloat("New").ToString() + ". Your high score is " + PlayerPrefs.GetFloat("Old") + ". Try again?";
    // globalObj.text = PlayerPrefs.GetFloat("New").ToString();
    if(GUILayout.Button("New Game")) {
      Application.LoadLevel("GameplayScene");
    }
    if(GUILayout.Button("Exit")) {
      Application.Quit();
    }
    GUILayout.EndArea();
  }
}
