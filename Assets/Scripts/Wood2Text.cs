using UnityEngine;
using System.Collections;

public class Wood2Text : MonoBehaviour {
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
      globalObj = g.GetComponent<Environment>().player2;
    }
    charge1Text.text = "Wood: " + globalObj.wood.ToString();
	}
}
