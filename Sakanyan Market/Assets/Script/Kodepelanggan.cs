using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kodepelanggan : MonoBehaviour
{
    public BarSabar bar;
    public Vector3 screenPoint;
    public Vector3 offset;
    public Pelanggan pelanggan = new Pelanggan();
    public Vector3 kursi;
    public RuntimeAnimatorController mina,nino,newton;
    public Animator skin;
    bool ondrag = false;
    public int noAntri;
    public float speed = 1;
    public int noKursi = -999;
    public Rigidbody2D rb;
    public Vector3 keluar;
    int waktumakan = 1000;
    void Start(){
      bar.set(10000,pelanggan.kesabaran);
      rb = this.GetComponent<Rigidbody2D>();
      keluar = GameObject.FindWithTag("manajer").GetComponent<Ordermanager>().keluar.position;
    }
    void Update()
    {
      bar.set(10000f,pelanggan.kesabaran);
      //cek state
      switch(pelanggan.state)
      {
        case 0:
          //randomize pelanggan tersedia + pesanannya
          pelanggan = new Pelanggan("garong","ikan goreng");
          break;
        case 1:
          //jalan ke antre.kalau belum sampai, jalan. kalau sudah ganti state 2
          if(transform.position.y > kursi.y){
            rb.MovePosition(transform.position + new Vector3(0,-1) * speed * Time.deltaTime);
            skin.Play("Base Layer.walk_down");
          }else{
            pelanggan.state++;
            GameObject.FindWithTag("manajer").GetComponent<Ordermanager>().isAntrianGerak = false;
            skin.Play("Base Layer.down");
          }
          break;
        case 2:
          //kalau belum dapat kursi kurangi kesabaran, pergantian state ke state 3 ditangani player
          if(pelanggan.kesabaran > 0){
            pelanggan.kesabaran--;
          }else{
            pelanggan.state = 5;
            LevelDesigner.kurangreputasi(2);
            GameObject.FindWithTag("manajer").GetComponent<Ordermanager>().keluarAntrian(noAntri);
          }

          break;
        case 3:
          skin.Play("Base Layer.right");
            //kalau belum dapat makanan kurangi kesabaran, pergantian state ke state 4 ditangani player
          if(pelanggan.kesabaran > 0){
            pelanggan.kesabaran--;
          }else{
            pelanggan.state = 5;
            LevelDesigner.kurangreputasi(2);
            GameObject.FindWithTag("manajer").GetComponent<Ordermanager>().pulang(noKursi);
          }

          break;
        case 4:
          //tunggu 10 detik lalu ganti ke state 5 dan bayar makan
          if(waktumakan > 0 ){
            waktumakan--;
          }else{
            LevelDesigner.bayar(10);
            LevelDesigner.tambahreputasi(1);
            pelanggan.state++;
            GameObject.FindWithTag("manajer").GetComponent<Ordermanager>().pulang(noKursi);
          }
          break;
        case 5:
          //kalau belum sampai, jalan. kalau sudah trigger pelanggan baru di order manager dan hancurkan objek
          if(transform.position.x > keluar.x){
            skin.Play("Base Layer.left");
            rb.MovePosition(transform.position - new Vector3(1,0) * speed * Time.deltaTime);
          }else if(transform.position.y < keluar.y){
            skin.Play("Base Layer.walk_up");
            rb.MovePosition(transform.position + new Vector3(0,1) * speed * Time.deltaTime);
          }else{
            Destroy(gameObject);
          }
          break;
      }
    }
    public void dapatkursi(){
      if(noKursi != -999){
        if(!GameObject.FindWithTag("manajer").GetComponent<Ordermanager>().kursiterisi[noKursi]){
          pelanggan.state = 3;
          kursi = GameObject.FindWithTag("manajer").GetComponent<Ordermanager>().kursi[noKursi].transform.position;
          GameObject.FindWithTag("manajer").GetComponent<Ordermanager>().masukKursi(noAntri,noKursi);
        }
      }
      rb.MovePosition(kursi);
    }
    void OnTriggerEnter2D(Collider2D hit){
      if(hit.tag == "kursi" & pelanggan.state == 2){
        Debug.Log("we hit");
        noKursi = hit.gameObject.GetComponent<Kursi>().noKursi;
        Debug.Log(noKursi);
      }
    }
    void OnTriggerExit2D(Collider2D hit){
      if(hit.tag == "kursi" & pelanggan.state == 2){
        noKursi = -999;
      }
    }

    void OnMouseDown(){
      if(pelanggan.state == 2){
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        ondrag = true;
      }
    }

    void OnMouseDrag(){
      if(ondrag){
        Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
        transform.position = cursorPosition;
      }
    }
    void OnMouseUpAsButton(){
      ondrag = false;
      dapatkursi();
    }
    public void pilihSkin(){
      switch(pelanggan.warna){
        case "Merah":
          skin.runtimeAnimatorController = mina;
          break;
        case "Kuning":
          skin.runtimeAnimatorController = nino;
          break;
        case "Biru":
          skin.runtimeAnimatorController = newton;
          break;
      }
      skin.Play("Base Layer.down");
    }
}


public class Pelanggan {
    public string name;
    public string order;
    public string warna; // merah, kuning, biru
    public int preferensi;
    public int state = 0; //0 = kosong(cuman buat init), 1 = jalan ke kursi, 2 = nunggu pesanan, 3 = makan, 4 = jalan pulang
                          // new: 0= init , 1 = jalan ke antrian, 2 = nunggu kursi, 3 nunggu pesanan, 4 makan, 5 jalan pulang
    public float kesabaran = 10000;
    public Pelanggan(string name, string order){
      this.name = name;
      this.order = order;
      this.state = 1;
    }
    public Pelanggan(string name, string order, string warna){
      this.name = name;
      this.order = order;
      this.state = 1;
      this.warna = warna;
    }
    public Pelanggan(Pelanggan copy){
      this.name = copy.name;
      this.order = copy.order;
      this.state = 1;
    }
    public Pelanggan(){
      this.state = 0;
    }
}
