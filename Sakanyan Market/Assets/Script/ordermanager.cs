using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ordermanager : MonoBehaviour
{
    public GameObject pelanggan;
    public Transform kursi1,kursi2,kursi3;
    public bool[] kursiterisi = new bool[3] {false,false,false};
    public GameObject[] pemesan = new GameObject[3];

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
      //cek kursi kosong
      //tiap kursi kosong summon pelanggan
      int i;
      for(i = 0; i <= 2;i++){
        if(!kursiterisi[i]){
          summon(i);
          kursiterisi[i] = true;
        }
      }
    }
    void summon(int i){
      i+=1;
      float x = 0f;
      float y = 0f;
      float z = 0f;
      switch(i){
        case 1:
          x = kursi1.position.x;
          y = kursi1.position.y;
          z = kursi1.position.z;
          break;
        case 2:
          x = kursi2.position.x;
          y = kursi2.position.y;
          z = kursi2.position.z;
          break;
        case 3:
          x = kursi3.position.x;
          y = kursi3.position.y;
          z = kursi3.position.z;
          break;
      }
      var temp = Instantiate(pelanggan,new Vector3(-10f,y,z),Quaternion.identity);
      temp.GetComponent<Kodepelanggan>().kursi = new Vector3(x,y,z);
      temp.GetComponent<Kodepelanggan>().nokursi = i;
      pemesan[i-1] = temp;
    }
    public void pulang(int i){
      i-=1;
      kursiterisi[i] = false;
    }

}
