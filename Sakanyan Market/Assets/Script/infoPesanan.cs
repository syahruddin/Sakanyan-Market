using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class infoPesanan : MonoBehaviour
{
    public Kodepelanggan pelanggan;
    public Text info;
    public Vector3 offset;
    public Sprite goreng,potong,rebus;
    public Image pesanan,bg;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      if(pelanggan.pelanggan.state == 3){
        pesanan.color = new Color32(255,255,255,255);
        bg.color = new Color32(255,255,255,255);
        switch(pelanggan.pelanggan.order){
          case "ikan goreng":
            pesanan.sprite = goreng;
            break;
          case "ikan potong":
            pesanan.sprite = potong;
            break;
          case "ikan rebus":
            pesanan.sprite = rebus;
            break;
        }
      }else{
        pesanan.color = new Color32(255,255,255,0);
        bg.color = new Color32(255,255,255,0);
      }
    }
}
