using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDesigner : MonoBehaviour
{
    public GameObject loseScreen, winpage;
    public static int uang = 0;
    public static int reputasi = 3;
    public static int sisaIkanKuning = 10;
    public static int sisaIkanMerah = 10;
    public static int sisaIkanBiru = 10;
    public static int targetUang = 100;
    public static string levelname = "";
    public int[] poolPelanggan = new int[3] {5,5,5}; //merah kuning biru
    public string[] menu = new string[2] {"ikan goreng","ikan potong"};

    public static void bayar(int bayaran){
      uang+=bayaran;
    }
    public static void tambahreputasi(int perubahan){
      for(int i = 1; i<= perubahan;i++){
        if(reputasi < 5){
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
      loseScreen.SetActive(false);
      winpage.SetActive(false);
      LevelDesigner.uang = 0;
      LevelDesigner.reputasi = 3;
      sisaIkanBiru = CurrentLevel.stock[0];
      sisaIkanMerah = CurrentLevel.stock[1];
      sisaIkanKuning = CurrentLevel.stock[2];
      poolPelanggan[0] = CurrentLevel.level.jumlahMerah;
      poolPelanggan[1] = CurrentLevel.level.jumlahKuning;
      poolPelanggan[2] = CurrentLevel.level.jumlahBiru;
      targetUang = CurrentLevel.level.target;
      levelname = CurrentLevel.levelname;
    }
    void Update(){
      if(uang >= targetUang ||Ordermanager.pelangganHabis) {
        win();
      }else if(reputasi <=0){
        lose("Reputasi Habis");
      }
    }
    public bool isSisaPelanggan(){//return ada atau enggaknya pelanggan tersisa
      return (poolPelanggan[0] + poolPelanggan[1] + poolPelanggan[2] > 0);
    }
    void win(){
      winpage.SetActive(true);
      UI_Controller.isPaused = true;
      Time.timeScale = 0;
    }
    void lose(string kondisi){
      Debug.Log(kondisi);
      loseScreen.SetActive(true);
      UI_Controller.isPaused = true;
      Time.timeScale = 0;
    }
    public void Restart(){
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
    public void winScreen(){
      SceneManager.LoadScene("AfterGame");
    }
}
