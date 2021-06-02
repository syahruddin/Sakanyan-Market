using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ordermanager : MonoBehaviour
{
    public GameObject pelanggan;
    public GameObject[] kursi = new GameObject[3];
    public bool[] kursiterisi = new bool[3] {false,false,false};
    public GameObject[] pemesan = new GameObject[3];
    public Transform[] antre = new Transform[3];
    public GameObject[] antrian = new GameObject[3];
    public bool[] antrianterisi = new bool[3] {false,false,false};
    public bool isAntrianGerak = false;
    public Transform keluar,masuk;
    public LevelDesigner level;
    public static bool pelangganHabis = false;
    void Start()
    {
      Ordermanager.pelangganHabis = false;
      int i;
      for(i=0; i<=2; i++){
        kursi[i].GetComponent<Kursi>().noKursi = i;
      }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      //cek antrian kosong
      //tiap antrian kosong summon pelanggan
      urusAntrian();
      cekPelangganHabis();
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
      kursiterisi[i] = false;
    }
    void urusAntrian(){
      if(!isAntrianGerak){
        int i = 0;
        while(i <=2 & !isAntrianGerak){
          uruskosong(i);
          i++;
        }
      }
    }
    void uruskosong(int noAntri){
      if(!antrianterisi[noAntri]){
        isAntrianGerak = true;
        if(noAntri == 2){
          if(level.isSisaPelanggan()){
            datangbaru();
          }
        }else if(antrianterisi[noAntri+1]){
          maju(noAntri+1);
        }else{
          uruskosong(noAntri+1);
        }
      }
    }
    void datangbaru(){
      var temp = Instantiate(pelanggan,masuk.position,Quaternion.identity);
      temp.GetComponent<Kodepelanggan>().kursi = antre[2].position;
      temp.GetComponent<Kodepelanggan>().noAntri = 2;
      temp.GetComponent<Kodepelanggan>().pelanggan = new Pelanggan("a",level.pesanRandom(),LevelDesigner.intToStringWarna(level.datang()));
      temp.GetComponent<Kodepelanggan>().pilihSkin();
      antrian[2] = temp;
      antrianterisi[2] = true;
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
    void cekPelangganHabis(){
      if(kursiKosong() && antriankosong()&& level.isSisaPelanggan()){
        Ordermanager.pelangganHabis = true;
      }
    }
    bool kursiKosong(){
      bool temp = true;
      for(int i = 0; i < kursiterisi.Length;i++){
        temp = temp && (!kursiterisi[i]);
      }
      return temp;
    }
    bool antriankosong(){
      bool temp = true;
      for(int i = 0; i < antrianterisi.Length;i++){
        temp = temp && (!antrianterisi[i]);
      }
      return temp;
    }

}
