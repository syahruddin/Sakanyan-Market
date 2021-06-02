using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class start : MonoBehaviour
{

    public void Mulai()
    {
      SceneManager.LoadScene("LevelSelect");
    }
    public void Keluar(){
      Application.Quit();
    }

}
