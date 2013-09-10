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

    if(GUILayout.Button("New Game")) {
      Application.LoadLevel("GameplayScene");
    }
    if(GUILayout.Button("Exit")) {
      Application.Quit();
    }
    GUILayout.EndArea();
  }
}
