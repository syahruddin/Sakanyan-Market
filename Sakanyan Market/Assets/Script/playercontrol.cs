using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontrol : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 1;
    public string hadap = "kiri";

    public Transform tunjuk;
    public string tangan = "";
    public string interact = "";
    public int sisaikan;
    public bool isgerak = false;
    public Animator anim;
    GameObject manajer;
    // Start is called before the first frame update
    void Start()
    {
      sisaikan = 30;
      rb = this.GetComponent<Rigidbody2D>();
      manajer = GameObject.FindWithTag("manajer");
    }

    // Update is called once per frame
    void Update()
    {
        gerak();
        ubahhadap();
        interaksi();
        animasi();
    }
    void ubahhadap(){
      switch(hadap){
        case "kiri":
          tunjuk.transform.position = new Vector3(transform.position.x-0.8f,transform.position.y-0.6f,0f);
          break;
        case "kanan":
          tunjuk.transform.position = new Vector3(transform.position.x+0.8f,transform.position.y-0.6f,0f);
          break;
        case "atas":
          tunjuk.transform.position = new Vector3(transform.position.x,transform.position.y+0.45f,0f);
          break;
        case "bawah":
          tunjuk.transform.position = new Vector3(transform.position.x,transform.position.y-0.45f,0f);
          break;
      }
    }
    void gerak(){
      isgerak = false;
      if (Input.GetKey(KeyCode.W)) {
        rb.MovePosition(transform.position + new Vector3(0,1) * speed * Time.deltaTime);
        hadap = "atas";
        isgerak = true;
      }else if (Input.GetKey(KeyCode.S)) {
        rb.MovePosition(transform.position + new Vector3(0,-1) * speed * Time.deltaTime);
        hadap = "bawah";
        isgerak = true;
      }

      if (Input.GetKey(KeyCode.A)) {
        rb.MovePosition(transform.position + new Vector3(-1,0) * speed * Time.deltaTime);
        hadap = "kiri";
        isgerak = true;
      }else if (Input.GetKey(KeyCode.D)) {
        rb.MovePosition(transform.position + new Vector3(1,0) * speed * Time.deltaTime);
        hadap = "kanan";
        isgerak = true;
      }
    }
    void interaksi(){
      if(Input.GetKeyDown("space")){
        switch(interact){
          case "potong":
            if(tangan=="ikan"){
              tangan = "ikan potong";
            }
            break;
          case "goreng":
            if(tangan == "ikan"){
              tangan ="ikan goreng";
            }
            break;
          case "box ikan":
            if(tangan == "" && sisaikan > 0){
              tangan ="ikan";
              sisaikan--;
            }
            break;
          case "sampah":
            tangan = "";
            break;
          case "meja 1":
            if(tangan!=""){
              kasihpesanan(1);
            }
            break;
          case "meja 2":
            if(tangan!=""){
              kasihpesanan(2);
            }
            break;
          case "meja 3":
            if(tangan!=""){
              kasihpesanan(3);
            }
            break;
          case "meja 4":
            if(tangan!=""){
              kasihpesanan(4);
            }
            break;
        }
      }
    }
    void kasihpesanan(int nomormeja){
      nomormeja--;
      //cek ada orang
      if(manajer.GetComponent<Ordermanager>().kursiterisi[nomormeja]){
        Debug.Log("ngasih ke meja " + (nomormeja +1));
        Debug.Log(nomormeja);
        Debug.Log(manajer.GetComponent<Ordermanager>().pemesan[nomormeja].GetComponent<Kodepelanggan>().pelanggan.state);
        //cek udah dapet pesanan belum
        if(manajer.GetComponent<Ordermanager>().pemesan[nomormeja].GetComponent<Kodepelanggan>().pelanggan.state == 3){
          Debug.Log("meja" + (nomormeja +1) + " belum dapat pesanan");
          //cek apakah pesanan benar
          if(manajer.GetComponent<Ordermanager>().pemesan[nomormeja].GetComponent<Kodepelanggan>().pelanggan.order == tangan){
            Debug.Log("meja" + (nomormeja +1) + " benar memesan " + tangan);
            manajer.GetComponent<Ordermanager>().pemesan[nomormeja].GetComponent<Kodepelanggan>().pelanggan.state = 4;
            tangan = "";
          }
        }
      }
    }
    void animasi(){
      if(isgerak){
        switch(hadap){
          case "kiri":
            anim.Play("Base Layer.mc_walk_left");
            break;
          case "kanan":
            anim.Play("Base Layer.mc_walk_right");
            break;
          case "atas":
            anim.Play("Base Layer.mc_walk_up");
            break;
          case "bawah":
            anim.Play("Base Layer.mc_walk_down");
            break;
        }
      }else{
        switch(hadap){
          case "kiri":
            anim.Play("Base Layer.mc_idle_left");
            break;
          case "kanan":
            anim.Play("Base Layer.mc_idle_right");
            break;
          case "atas":
            anim.Play("Base Layer.mc_idle_up");
            break;
          case "bawah":
            anim.Play("Base Layer.mc_idle_down");
            break;
        }
      }
    }
}
