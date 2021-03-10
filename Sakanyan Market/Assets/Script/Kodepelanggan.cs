using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kodepelanggan : MonoBehaviour
{
    public Pelanggan pelanggan = new Pelanggan();
    public Vector3 kursi;
    public float speed = 1;
    public int nokursi;
    public Rigidbody2D rb;
    void Start(){
      rb = this.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
      //cek state
      switch(pelanggan.state)
      {
        case 0:
          //randomize pelanggan tersedia + pesanannya
          pelanggan = new Pelanggan("garong","ikan goreng");
          break;
        case 1:
          //jalan ke kursi.kalau belum sampai, jalan. kalau sudah ganti state 2
          if(transform.position.x < kursi.x){
            rb.MovePosition(transform.position + new Vector3(1,0) * speed * Time.deltaTime);
          }else{
            pelanggan.state++;
          }
          break;
        case 2:
          //kalau belum dapat makanan kurangi kesabaran, pergantian state ke state 3 ditangani player
          if(pelanggan.kesabaran > 0){
            pelanggan.kesabaran--;
          }else{
            pelanggan.state = 4;
          }

          break;
        case 3:
          //kalau belum selesai kurangi waktu makan, kalau sudah ganti state 4
          pelanggan.state++;
          break;
        case 4:
          //kalau belum sampai, jalan. kalau sudah trigger pelanggan baru di order manager dan hancurkan objek
          if(transform.position.x > -10f){
            rb.MovePosition(transform.position - new Vector3(1,0) * speed * Time.deltaTime);
          }else{
            GameObject.FindWithTag("manajer").GetComponent<Ordermanager>().pulang(nokursi);
            Destroy(gameObject);
          }
          break;
      }
    }
}


public class Pelanggan {
    public float waktumakan = 100;
    public string name;
    public string order;
    public int preferensi;
    public int state = 0; //0 = kosong(cuman buat init), 1 = jalan ke kursi, 2 = nunggu pesanan, 3 = makan, 4 = jalan pulang
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
