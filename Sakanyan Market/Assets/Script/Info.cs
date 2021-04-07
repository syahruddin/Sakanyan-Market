using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Info : MonoBehaviour
{
    GameObject player;
    public Text info;
    // Start is called before the first frame update
    void Start()
    {
      player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
      info.text = "Tangan: " + player.GetComponent<Playercontrol>().tangan
      + "\nSisa Ikan: " + player.GetComponent<Playercontrol>().sisaikan
      +"\nReputasi: " + LevelDesigner.reputasi
      +"\nUang/Target: $" +LevelDesigner.uang +"/ $" + LevelDesigner.targetUang;
    }
}
