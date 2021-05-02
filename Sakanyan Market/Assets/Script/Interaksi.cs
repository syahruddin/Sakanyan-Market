using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaksi : MonoBehaviour
{
  GameObject player;
  void Start(){
    player = GameObject.FindWithTag("Player");
  }
  void OnTriggerEnter2D(Collider2D hit){
    if(hit.tag == "interaksi"){
      player.GetComponent<Playercontrol>().interact = hit.name;
      Debug.Log("we hit");
    }
  }
  void OnTriggerExit2D(Collider2D hit){
    if(hit.tag == "interaksi"){
      player.GetComponent<Playercontrol>().interact = "";
    }
  }
}
