using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ordermanager : MonoBehaviour
{
    public GameObject pelanggan;
    public GameObject[] kursi = new GameObject[3];
    public bool[] kursiterisi = new bool[3] {false,false,false};
    public GameObject[] pemesan = new GameObject[3];
    public Transform[] antre = new Transform[5];
    public GameObject[] antrian = new GameObject[5];
    public bool[] antrianterisi = new bool[5] {false,false,false,false,false};
    public bool isAntrianGerak = false;
    public Transform keluar,masuk;

    void Start()
    {
      int i;
      for(i=0; i<=2; i++){
        kursi[i].GetComponent<Kursi>().noKursi = i;
      }
    }

    // Update is called once per frame
    void Update()
    {
      //cek antrian kosong
      //tiap antrian kosong summon pelanggan
      urusAntrian();
    }
    public void keluarAntrian(int i){
      antrianterisi[i] = false;
      antrian[i] = null;
    }
    public void masukKursi(int noAntri, int noKursi){
      kursiterisi[noKursi] = true;
      pemesan[noKursi] = antrian[noAntri];
      keluarAntrian(noAntri);
    }
    void summon(int i){
      float x = 0f;
      float y = 0f;
      float z = 0f;
      x = kursi[i].transform.position.x;
      y = kursi[i].transform.position.y;
      z = kursi[i].transform.position.z;
      var temp = Instantiate(pelanggan,new Vector3(-10f,y,z),Quaternion.identity);
      temp.GetComponent<Kodepelanggan>().kursi = new Vector3(x,y,z);
      temp.GetComponent<Kodepelanggan>().noKursi = i+1;
      pemesan[i] = temp;
    }
    public void pulang(int i){
      i-=1;
      kursiterisi[i] = false;
    }
    void urusAntrian(){
      if(!isAntrianGerak){
        int i = 0;
        while(i <=4 & !isAntrianGerak){
          uruskosong(i);
          i++;
        }
      }
    }
    void uruskosong(int noAntri){
      if(!antrianterisi[noAntri]){
        isAntrianGerak = true;
        if(noAntri == 4){
          datangbaru();
        }else if(antrianterisi[noAntri+1]){
          maju(noAntri+1);
        }else{
          uruskosong(noAntri+1);
        }
      }
    }
    void datangbaru(){
      var temp = Instantiate(pelanggan,masuk.position,Quaternion.identity);
      temp.GetComponent<Kodepelanggan>().kursi = antre[4].position;
      temp.GetComponent<Kodepelanggan>().noAntri = 4;
      antrian[4] = temp;
      antrianterisi[4] = true;
    }
    void maju(int noAntri){
      antrianterisi[noAntri] = false;
      antrianterisi[noAntri-1] = true;
      antrian[noAntri-1] = antrian[noAntri];
      antrian[noAntri].GetComponent<Kodepelanggan>().kursi = antre[noAntri-1].position;
      antrian[noAntri] = null;
      antrian[noAntri-1].GetComponent<Kodepelanggan>().pelanggan.state = 1;
      antrian[noAntri-1].GetComponent<Kodepelanggan>().noAntri = noAntri-1;
    }

}
