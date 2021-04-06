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
    bool ondrag = false;
    public int noAntri;
    public float speed = 1;
    public int noKursi = -999;
    public Rigidbody2D rb;
    public Vector3 keluar;
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
          }else{
            pelanggan.state++;
            GameObject.FindWithTag("manajer").GetComponent<Ordermanager>().isAntrianGerak = false;
          }
          break;
        case 2:
          //kalau belum dapat kursi kurangi kesabaran, pergantian state ke state 3 ditangani player
          if(pelanggan.kesabaran > 0){
            pelanggan.kesabaran--;
          }else{
            pelanggan.state = 5;
            GameObject.FindWithTag("manajer").GetComponent<Ordermanager>().keluarAntrian(noAntri);
          }

          break;
        case 3:
            //kalau belum dapat makanan kurangi kesabaran, pergantian state ke state 4 ditangani player
          if(pelanggan.kesabaran > 0){
            pelanggan.kesabaran--;
          }else{
            pelanggan.state = 5;
            GameObject.FindWithTag("manajer").GetComponent<Ordermanager>().keluarAntrian(noAntri);
          }

          break;
        case 4:
          //kalau belum selesai kurangi waktu makan, kalau sudah ganti state 5
          pelanggan.state++;
          GameObject.FindWithTag("manajer").GetComponent<Ordermanager>().pulang(noKursi);
          break;
        case 5:
          //kalau belum sampai, jalan. kalau sudah trigger pelanggan baru di order manager dan hancurkan objek
          if(transform.position.x > keluar.x){
            rb.MovePosition(transform.position - new Vector3(1,0) * speed * Time.deltaTime);
          }else if(transform.position.y < keluar.y){
            rb.MovePosition(transform.position + new Vector3(0,1) * speed * Time.deltaTime);
          }else{
            Destroy(gameObject);
          }
          break;
      }
    }
    void dapatkursi(){
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
}


public class Pelanggan {
    public float waktumakan = 100;
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
    public Pelanggan(){
      this.state = 0;
    }
}
