using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDesigner : MonoBehaviour
{
    public static int uang = 0;
    public static int reputasi = 3;
    public static int targetUang = 100;

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
    void update(){
      if(uang >= targetUang){
        win();
      }else if(reputasi <=0){
        lose("Reputasi Habis");
      }
    }
    public static void win(){

    }
    public static void lose(string kondisi){

    }
}
