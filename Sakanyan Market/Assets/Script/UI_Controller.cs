using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour
{
    public GameObject[] bintang = new GameObject[5];
    public Text goal,money,sisabiru,sisamerah,sisakuning,level;
    public Image tangan,warnatangan;
    public Sprite goreng,sup,potong,merah,kuning,biru;
    public Playercontrol player;

    // Start is called before the first frame update
    void Start()
    {
      goal.text = "$" + LevelDesigner.targetUang;
    }

    // Update is called once per frame
    void Update()
    {
      urusReputasi();
      urusWarna();
      urusTangan();
      urusUang();
      urusStok();
    }
    void urusReputasi(){
      for(int i = 1; i <= 5; i ++){
        bintang[i-1].SetActive(i <= LevelDesigner.reputasi);
      }
    }
    void urusTangan(){
      if(player.tangan.isKosong()){
        tangan.color = new Color32(255,255,255,0);
      }else{
        tangan.color = new Color32(255,255,255,255);
        switch(player.tangan.nama){
          case "ikan goreng":
            tangan.sprite = goreng;
            break;
          case "ikan potong":
            tangan.sprite = potong;
            break;
          case "ikan rebus":
            tangan.sprite = sup;
            break;
          case "ikan":
            switch(player.tangan.warna){
              case "merah":
                tangan.sprite = merah;
                break;
              case "kuning":
                tangan.sprite = kuning;
                break;
              case "biru":
                tangan.sprite = biru;
                break;
            }
            break;
        }
      }
    }
    void urusStok(){
      sisabiru.text = LevelDesigner.sisaIkanBiru.ToString();
      sisakuning.text = LevelDesigner.sisaIkanKuning.ToString();
      sisamerah.text = LevelDesigner.sisaIkanMerah.ToString();
    }
    void urusUang(){
      money.text = "$" + LevelDesigner.uang;
    }
    void urusWarna(){
      if(player.tangan.isKosong()){
        warnatangan.color = new Color32(255,255,255,255);
      }else{
        switch(player.tangan.warna){
          case "merah":
            warnatangan.color = new Color32(255,0,0,255);
            break;
          case "kuning":
            warnatangan.color = new Color32(255,255,0,255);
            break;
          case "biru":
            warnatangan.color = new Color32(0,0,255,255);
            break;
        }
      }
    }
}
