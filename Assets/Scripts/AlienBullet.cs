﻿using UnityEngine;
using System.Collections;

public class AlienBullet : MonoBehaviour {

  Vector3 thrust;
  float speed = 0.25f;
  private bool reflected = false;
  public Vector3 direction = new Vector3(0, -1, 0);
  public Alien ship;
	// Use this for initialization
	void Start () {
    thrust.y = 0.0f;
	}


  void OnCollisionEnter(Collision collision) {
    Collider collider = collision.collider;
    if(collider.CompareTag("Ship")) {
      Ship player = collider.gameObject.GetComponent<Ship>();
      player.takeDamage();
      die();
    }
    else if(collider.CompareTag("Platform")) {
      Platform platform = collider.gameObject.GetComponent<Platform>();
      platform.takeDamage();
      die();
    }
    else if(collider.CompareTag("Alien")) {
      if(reflected) {
        Alien alien = collider.gameObject.GetComponent<Alien>();
        alien.takeDamage();
        die();
      }
    }
    else if(collider.CompareTag("UFO")) {
      if(reflected) {
        UFO ufo = collider.gameObject.GetComponent<UFO>();
        ufo.die();
        die();
        GameObject g = GameObject.Find("Environment");
        Environment globalObj = g.GetComponent<Environment>();
        globalObj.score += 300;
      }
    }
    else if(collider.CompareTag("Shield")) {

      // reflect only if player isn't shielded
      Shield shield = collider.GetComponent<Shield>();
      if(shield.activated) {
        reflected = true;
        Vector3 avg = new Vector3(0,0,0);
        // average over the collision's contact points is the normal
        foreach(ContactPoint contact in collision.contacts) {
          avg += contact.normal;
        }
        avg /= collision.contacts.GetLength(0);
        Debug.Log("his");
        Debug.Log(avg);
        direction = Vector3.Reflect(gameObject.transform.position, avg);
        direction.Normalize();
        direction += shield.rigidbody.velocity;
        // TODO: add to the direction vector of the player

      }
      else {
        shield.transform.parent.GetComponent<Player1>().takeDamage();
        die();
      }
    }
    else if(collider.CompareTag("Player")){
      // TODO: Figure out why it never hits player when shield is down
    	Debug.Log("Hit player. should not happen");

      Shield shield = collider.GetComponent<Player1>().shield;
      if(shield.activated) {
        reflected = true;
        Vector3 avg = new Vector3(0,0,0);
        // average over the collision's contact points is the normal
        foreach(ContactPoint contact in collision.contacts) {
          avg += contact.normal;
        }
        avg /= collision.contacts.GetLength(0);
        Debug.Log("his");
        Debug.Log(avg);
        direction = Vector3.Reflect(gameObject.transform.position, avg);
        direction.Normalize();
        direction += rigidbody.velocity;
        // TODO: add to the direction vector of the player
      }
      else {
        shield.transform.parent.GetComponent<Player1>().takeDamage();
        die();
      }
    }
    else if(collider.CompareTag("Human")){
      Human human = collider.gameObject.GetComponent<Human>();
      human.takeDamage();
      die();
    }
    else if(collider.CompareTag("Ground")){
      die();
    }
    else {
      Debug.Log("Collided with " + collider.tag);
    }
  }

  // void OnTriggerEnter(Collider collider){
  //   if(collider.CompareTag("Shield")) {

  //     Vector3 avg = new Vector3(0,0,0);
  //     // average over the collision's contact points is the normal
  //     foreach(ContactPoint contact in collision.contacts) {
  //       avg += contact.normal;
  //     }
  //     avg /= collision.contacts.GetLength(0);
  //     Debug.Log("his");
  //     Debug.Log(avg);
  //   }


  // }

	// Update is called once per frame
	void FixedUpdate () {
    gameObject.transform.position += speed * direction;

	}

  void die() {
    Destroy(gameObject);
  }
}
