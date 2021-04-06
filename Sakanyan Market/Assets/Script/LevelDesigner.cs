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
    public static void ubahreputasi(int perubahan){
      reputasi+=perubahan;
    }
    void update(){
      if(uang >= targetUang){
        win();
      }else if(reputasi <=0){
        lose();
      }
    }
    public static void win(){

    }
    public static void lose(){

    }
}
