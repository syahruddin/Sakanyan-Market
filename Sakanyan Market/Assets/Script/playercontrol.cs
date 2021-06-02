using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontrol : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 1;
    public string hadap = "kiri";

    public Transform tunjuk;
    public Barang tangan;
    public string interact = "";
    public bool isgerak = false;
    public Animator anim;
    GameObject manajer;
    // Start is called before the first frame update
    void Start()
    {
      rb = this.GetComponent<Rigidbody2D>();
      manajer = GameObject.FindWithTag("manajer");
      tangan = new Barang();
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
      if(!UI_Controller.isPaused){
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
    }
    void interaksi(){
      if(Input.GetKeyDown("space") & !UI_Controller.isPaused){
        switch(interact){
          case "potong":
            if(tangan.nama =="ikan"){
              tangan.nama = "ikan potong";
            }
            break;
          case "goreng":
            tangan = GameObject.Find("goreng").GetComponent<Masak>().interact(tangan);
            break;
          case "rebus":
            tangan = GameObject.Find("rebus").GetComponent<Masak>().interact(tangan);
            break;
          case "hold1":
            tangan = GameObject.Find("hold1").GetComponent<Hold>().interact(tangan);
            break;
          case "hold2":
            tangan = GameObject.Find("hold2").GetComponent<Hold>().interact(tangan);
            break;
          case "box ikan kuning":
            if(tangan.isKosong() && LevelDesigner.sisaIkanKuning > 0){
              tangan.nama ="ikan";
              tangan.warna = "kuning";
              LevelDesigner.sisaIkanKuning--;
            }
            break;
          case "box ikan merah":
            if(tangan.isKosong() && LevelDesigner.sisaIkanMerah > 0){
              tangan.nama ="ikan";
              tangan.warna = "merah";
              LevelDesigner.sisaIkanMerah--;
            }
            break;
          case "box ikan biru":
            if(tangan.isKosong() && LevelDesigner.sisaIkanBiru > 0){
              tangan.nama ="ikan";
              tangan.warna = "biru";
              LevelDesigner.sisaIkanBiru--;
            }
            break;
          case "sampah":
            tangan.nama = "";
            break;
          case "meja 1":
            if(tangan.nama !=""){
              kasihpesanan(1);
            }
            break;
          case "meja 2":
            if(tangan.nama !=""){
              kasihpesanan(2);
            }
            break;
          case "meja 3":
            if(tangan.nama !=""){
              kasihpesanan(3);
            }
            break;
          case "meja 4":
            if(tangan.nama !=""){
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
          if(manajer.GetComponent<Ordermanager>().pemesan[nomormeja].GetComponent<Kodepelanggan>().pelanggan.order == tangan.nama){
            Debug.Log("meja" + (nomormeja +1) + " benar memesan " + tangan.nama);
            manajer.GetComponent<Ordermanager>().pemesan[nomormeja].GetComponent<Kodepelanggan>().pelanggan.state = 4;
            manajer.GetComponent<Ordermanager>().pemesan[nomormeja].GetComponent<Kodepelanggan>().warnamakanan = tangan.warna;
            tangan.nama = "";
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
public class Barang{
  public string warna = "";
  public string nama = "";
  public Barang(){
  }
  public Barang(string nama, string warna){
    this.nama = nama;
    this.warna = warna;
  }
  public string infoBarang(){
    return nama + " " + warna;
  }
  public bool isKosong(){
    return nama == "";
  }
}
