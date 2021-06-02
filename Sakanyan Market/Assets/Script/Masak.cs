using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Masak : MonoBehaviour
{
    public Barang barang;
    public string jenisMasakan;
    public int waktuMasak = 600;
    public int state = 1; // 1 kosong, 2 masak, 3 jadi
    public int timer = 0;
    // Start is called before the first frame update
    void Start()
    {
      state = 1;
    }

    // Update is called once per frame
    void Update()
    {
      if(state == 2){
        if(timer > 0){
          timer--;
        }else{
          masakJadi();
        }
      }
    }
    void masakJadi(){
      state = 3;
      barang.nama = jenisMasakan;
    }
    public Barang interact(Barang tangan){
      switch(state){
        case 1:
          if(tangan.nama == "ikan"){
            state = 2;
            timer = waktuMasak;
            barang = tangan;
            return new Barang();
          }else{
            return tangan;
          }
        case 3:
          if(tangan.isKosong()){
            state = 1;
            return barang;
          }else{
            return tangan;
          }
      }
      return tangan;
    }
}
