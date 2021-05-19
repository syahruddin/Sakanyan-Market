using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDesigner : MonoBehaviour
{
    public static int uang = 0;
    public static int reputasi = 3;
    public static int targetUang = 100;
    public int[] poolPelanggan = new int[3] {5,5,5}; //merah kuning biru
    public string[] menu = new string[2] {"ikan goreng","ikan potong"};

    public static void bayar(int bayaran){
      uang+=bayaran;
    }
    public static void tambahreputasi(int perubahan){
      for(int i = 1; i<= perubahan;i++){
        if(reputasi < 10){
          reputasi++;
        }
      }
    }
    public static void kurangreputasi(int perubahan){
      for(int i = 1; i<= perubahan;i++){
        if(reputasi > 0){
          reputasi--;
        }
      }
    }
    void Start(){
      LevelDesigner.uang = 0;
      LevelDesigner.reputasi = 3;
    }
    void Update(){
      if(uang >= targetUang || Ordermanager.pelangganHabis) {
        win();
      }else if(reputasi <=0){
        lose("Reputasi Habis");
      }
    }
    public bool isSisaPelanggan(){//return ada atau enggaknya pelanggan tersisa
      return (poolPelanggan[0] + poolPelanggan[1] + poolPelanggan[2] > 0);
    }
    void win(){
      invokerestart();
    }
    void lose(string kondisi){
      Debug.Log(kondisi);
      invokerestart();
    }
    void invokerestart(){
      Invoke("Restart",5f);
    }
    void Restart(){
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public int datang(){ //asumsi cuman dipanggil setelah cek isSisapelanggan()
      int chs = -1;
      int i;
      //cek kondisi sisa berapa jenis
      switch(cekSisaJenis()){
        case 1:
          i = 0;
          while(chs == -1){
            if (poolPelanggan[i] > 0){
              chs = i;
            }else{
              i++;
            }
          }
          break;
        case 2:
          int a = -1;
          int b = -1;
          for(i = 0; i <= 2; i++){
            if (poolPelanggan[i] > 0){
              if(a == -1){
                a = i;
              }else{
                b = i;
              }
            }
          }
          if(Random.Range(0,2) == 1){
            chs = a;
          }else{
            chs = b;
          }
          break;
        case 3:
          chs = Random.Range(0,3);
          break;
      }
      poolPelanggan[chs]--;
      return chs;
    }
    int cekSisaJenis(){
      int temp = 0;
      for(int i = 0; i <= 2; i++){
        if (poolPelanggan[i] > 0){
          temp++;
        }
      }
      return temp;
    }
    public static string intToStringWarna(int i){
      switch(i){
        case 0:
          return "Merah";
        case 1:
          return "Kuning";
        case 2:
          return "Biru";
        default:
          return "";
      }
    }
    public string pesanRandom(){
      int temp = Random.Range(0,menu.Length);
      return menu[temp];
    }
}
