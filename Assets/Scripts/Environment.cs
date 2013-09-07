using UnityEngine;
using System.Collections;

public class Environment : MonoBehaviour {

  private int rows = 5;
  private int cols = 11;
  private GameObject[,] aliens;
	// Use this for initialization
	void Start () {
    Camera.main.orthographic = true;
  // generate aliens in a grid

    // Vector3 width = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0));
    // Vector3 height = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));
    // float dist = (transform.position - Camera.main.transform.position).z;
    Vector3 originScreen = Camera.main.WorldToScreenPoint(new Vector3(0,0,0));
    // Vector3 bl = Camera.main.ScreenToWorldPoint(new Vector3(0,0,originScreen.z));
    // Vector3 br = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,0,originScreen.z));
    // Vector3 tl = Camera.main.ScreenToWorldPoint(new Vector3(0,Screen.height,originScreen.z));
    // Vector3 tr = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height,originScreen.z));
    // Vector3 horizontal = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / cols, 0, originScreen.z));
    // Vector3 vertical = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height / rows, originScreen.z));
    aliens  = new GameObject[rows, cols];
    // Debug.Log(Screen.width);
    // Debug.Log(Screen.height);
    // Debug.Log(bl);
    // Debug.Log(br);
    // Debug.Log(tl);
    // Debug.Log(tr);
    // Debug.Log(horizontal);
    // Debug.Log(vertical);
    Debug.Log(Screen.width);
    Debug.Log(Screen.width / cols);
    Debug.Log(Screen.height);
    Debug.Log(rows);
    Debug.Log(cols);

    for(int i = 0; i < rows; i++) {
      for(int j = 0; j < cols; j++) {
        // Debug.Log(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / cols * j, Screen.height / rows * i, originScreen.z)));
        // Debug.Log(Camera.main.ScreenToWorldPoint(new Vector3(0, 0, originScreen.z)));
        // Debug.Log(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / cols * j, Screen.height / rows * i, originScreen.z)));
        // Debug.Log(originScreen.z);
        // Debug.Log(i);
        // Debug.Log(j);
        // Debug.Log(width / cols * j);
        // Debug.Log(height / rows * i);
        Debug.Log(i);
        Vector3 n = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / cols * j, Screen.height - Screen.height / rows * i, originScreen.z));
        // TODO: WHY THE HELL DO I HAVE TO DO THIS?!
        n.z = 0;
        Debug.Log(n);
        Debug.Log(originScreen.z);

        aliens[i, j] = Instantiate(Resources.Load("Prefabs/AlienPrefab"),
            n,
            Quaternion.identity) as GameObject;
      }
    }
	}

	// Update is called once per frame
	void Update () {

	}
}
