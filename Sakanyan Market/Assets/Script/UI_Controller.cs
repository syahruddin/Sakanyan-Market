using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Controller : MonoBehaviour
{
    public GameObject[] bintang = new GameObject[5];
    public Text goal,money,sisabiru,sisamerah,sisakuning,level;
    public Image tangan,warnatangan;
    public Sprite goreng,sup,potong,merah,kuning,biru;
    public Playercontrol player;
    public GameObject menu;
    public static bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
      Time.timeScale = 1;
      isPaused = false;
      goal.text = "$" + LevelDesigner.targetUang;
      level.text = "Level " + CurrentLevel.levelname;
    }

    // Update is called once per frame
    void Update()
    {
      urusReputasi();
      urusWarna();
      urusTangan();
      urusUang();
      urusStok();
      menu.SetActive(isPaused);
    }
    void urusReputasi(){
      for(int i = 1; i <= 5; i ++){
        bintang[i-1].SetActive(i <= LevelDesigner.reputasi);
      }
    }
    public void pause(){
      isPaused = true;
      Time.timeScale = 0;
    }
    public void resume(){
      isPaused = false;
      Time.timeScale = 1;
    }
    public void mainmenu(){
      SceneManager.LoadScene("MainMenu");
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
