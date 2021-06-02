using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    int selectedLevel = 0;
    int[] ikan = new int[3]; //0 biru, 1 merah, 2 kuning
    public Text banner,levelname,biru,merah,kuning,total,ikanm,ikanb,ikank;
    int state = 1; // 1 = level select, 2 overview, 3 prepare
    public GameObject state1, state2,state3,state23;
    public LevelInfo[] level = new LevelInfo[5];
    void Start()
    {
        ikan[0] = 10;
        ikan[2] = 10;
        ikan[1] = 10;
        state = 1;
        level[0] = new LevelInfo(0,0,10,25);
        level[1] = new LevelInfo(0,3,10,30);
        level[2] = new LevelInfo(5,5,5,75);
        level[3] = new LevelInfo(8,8,8,150);
        level[4] = new LevelInfo(10,10,10,300);
    }

    // Update is called once per frame
    void Update()
    {
      stateManage();
      urusBanner();
      urusLevel();
      urusCustomer();
      urusTotal();
    }
    void urusTotal(){
      if(state == 3){
        int temp = ikan[0] + ikan[1] + ikan[2];
        total.text = temp + "/30";
        ikank.text = ikan[2].ToString();
        ikanb.text = ikan[0].ToString();
        ikanm.text = ikan[1].ToString();
      }


    }
    public void addIkan(int i){
      if(ikan[0] + ikan[1] + ikan[2] < 30){
        ikan[i]++;
      }
    }
    public void removeIkan(int i){
      if(ikan[i] > 0){
        ikan[i]--;
      }
    }
    void urusCustomer(){
      if(state == 2){
        merah.text = level[selectedLevel-1].jumlahMerah + "X";
        biru.text = level[selectedLevel-1].jumlahBiru + "X";
        kuning.text = level[selectedLevel-1].jumlahKuning + "X";
      }
    }
    void stateManage(){
      state1.SetActive(state == 1);
      state2.SetActive(state == 2);
      state3.SetActive(state == 3);
      state23.SetActive(state == 2||state == 3);
    }
    void urusBanner(){
      string temp = "";
      switch(state){
        case 1:
        temp = "Level Select";
        break;
        case 2:
        temp = "Customer Overview";
        break;
        case 3:
        temp = "Stock Preparation";
        break;
      }
      banner.text = temp;

    }
    void urusLevel(){
      if(state > 1){
        levelname.text = "Level" + "\n" + "1-" + selectedLevel;
      }
    }
    public void mulai(){
      CurrentLevel.level = level[selectedLevel-1];
      CurrentLevel.stock[0] = ikan[0];
      CurrentLevel.stock[1] = ikan[1];
      CurrentLevel.stock[2] = ikan[2];
      CurrentLevel.levelname = "1-" + selectedLevel;
      SceneManager.LoadScene("MainGame");
    }
    public void back(){
      if(state > 1){
        state = 1;
      }else{
        SceneManager.LoadScene("MainMenu");
      }
    }
    public void changeState(int state){
      this.state = state;
    }
    public void pilihLevel(int level){
      selectedLevel = level;
      state = 2;
    }
}
public class LevelInfo{
  public int jumlahBiru;
  public int jumlahKuning;
  public int jumlahMerah;
  public int target;
  public LevelInfo(int biru,int kuning, int merah, int target){
    jumlahBiru = biru;
    jumlahKuning = kuning;
    jumlahMerah = merah;
    this.target = target;
  }
}
public class CurrentLevel{
  public static LevelInfo level = new LevelInfo(10,10,10,100);
  public static string levelname = "";
  public static int[] stock = new int[3];
  static CurrentLevel(){
    stock[0] = 10;
    stock[1] = 10;
    stock[2] = 10;
  }
}
