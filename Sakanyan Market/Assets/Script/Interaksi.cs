using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaksi : MonoBehaviour
{ 
  GameObject player;
  void Start(){
    player = GameObject.FindWithTag("Player");
  }
  void OnCollisionEnter2D(Collision2D hit){
    if(hit.collider.tag == "interaksi"){
      player.GetComponent<Playercontrol>().interact = hit.collider.name;
      Debug.Log("we hit");
    }
  }
  void OnCollisionExit2D(Collision2D hit){
    if(hit.collider.tag == "interaksi"){
      player.GetComponent<Playercontrol>().interact = "";
    }
  }
}
