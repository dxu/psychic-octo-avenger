using UnityEngine;
using System.Collections;

public class Charge1 : MonoBehaviour {

  Player1 globalObj;
  GUIText charge1Text;
	// Use this for initialization
	void Start () {
    charge1Text = gameObject.GetComponent<GUIText>();
	}

	// Update is called once per frame
	void Update () {
    if(!globalObj) {
      GameObject g = GameObject.Find("Environment");
      globalObj = g.GetComponent<Environment>().player1;
    }
    charge1Text.text = "Shield: " + Mathf.Round(globalObj.shield.life).ToString();
	}
}
