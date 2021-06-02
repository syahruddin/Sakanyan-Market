using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Masak masak;
    public Image tile;
    public GameObject timer;
    public GameObject masakan;
    byte r,g,b;

    // Update is called once per frame
    void start(){
      r = 248;
      g = 79;
      b = 79;
    }
    void Update()
    {
      timer.transform.position = transform.parent.position;
      timer.SetActive(masak.state != 1);
      masakan.SetActive(masak.state == 3);
      if(masak.state == 2){
        tile.color = new Color32(r,g,b,255);
        tile.fillAmount += (1f / masak.waktuMasak);
      }else{
        tile.color = new Color32(r,g,b,0);
        tile.fillAmount = 0f;
      }
      aturWarna();
    }
    void aturWarna(){
      switch(masak.state){
        case 1:
          r = 248;
          g = 79;
          b = 79;
          break;
        case 2:
          if(tile.fillAmount < 0.25f){
            r = 248;
            g = 79;
            b = 79;
          }else if(tile.fillAmount < 0.5f){
            r = 248;
            g = 158;
            b = 79;
          }else if(tile.fillAmount < 0.75f){
            r = 234;
            g = 248;
            b = 79;
          }else {
            r = 164;
            g = 207;
            b = 79;
          }
          break;

      }

    }
}
