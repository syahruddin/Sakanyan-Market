using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class infoPesanan : MonoBehaviour
{
    public Kodepelanggan pelanggan;
    public Text info;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      if(pelanggan.pelanggan.state == 3){
        info.text = pelanggan.pelanggan.order;
      }else{
        info.text = "";
      }
    }
}
