using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AfterGameClear : MonoBehaviour
{
    int count = 1;
    public GameObject target,reputation;
    public Image piala;
    public Sprite bronze,silver,gold;
    // Start is called before the first frame update
    void Start()
    {
      count = 1;
      target.SetActive(LevelDesigner.reputasi == 5);
      reputation.SetActive(LevelDesigner.targetUang == LevelDesigner.uang);
      if(LevelDesigner.reputasi == 5){
        count++;
      }
      if(LevelDesigner.targetUang == LevelDesigner.uang){
        count++;
      }
    }

    // Update is called once per frame
    void Update()
    {
      switch(count){
        case 1:
        piala.sprite = bronze;
        break;
        case 2:
        piala.sprite = silver;
        break;
        case 3:
        piala.sprite = gold;
        break;
      }
    }
}
