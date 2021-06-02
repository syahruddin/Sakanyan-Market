using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hold : MonoBehaviour
{
    public Image gambar;
    public Barang barang;
    public Sprite goreng,sup,potong,merah,kuning,biru;
    // Start is called before the first frame update
    void Start()
    {
      barang = new Barang();
    }

    // Update is called once per frame
    void Update()
    {
      if(barang.isKosong()){
        gambar.color = new Color32(255,255,255,0);
      }else{
        gambar.color = new Color32(255,255,255,255);
        switch(barang.nama){
          case "ikan goreng":
            gambar.sprite = goreng;
            break;
          case "ikan potong":
            gambar.sprite = potong;
            break;
          case "ikan rebus":
            gambar.sprite = sup;
            break;
          case "ikan":
            switch(barang.warna){
              case "merah":
                gambar.sprite = merah;
                break;
              case "kuning":
                gambar.sprite = kuning;
                break;
              case "biru":
                gambar.sprite = biru;
                break;
            }
            break;
        }
      }
    }
    public Barang interact(Barang tangan){
      if(tangan.isKosong() & barang.isKosong()){
        return tangan;
      }
      Barang temp = barang;
      barang = tangan;
      return temp;
    }
}
