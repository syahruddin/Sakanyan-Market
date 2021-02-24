using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ordermanager : MonoBehaviour
{
    public Transform kursi1,kursi2,kursi3;
    public Order[] orderlist;
    int i;
    public float decaykesabaran = 0.1f;
    public float delaymakan = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
      orderlist = new Order[3];
      for(i = 0; i <= 2; i++){
        orderlist[i] = new Order();
      }
    }

    // Update is called once per frame
    void Update()
    {
      //cek keadaan tiap kursi
      managekursi(orderlist);

    }

    void managekursi(Order[] orderlist){
      int i;
      for(i = 0; i <= 2; i++){
        switch(orderlist[i].state)
          {
            case 0:
            //kursi kosong
              int pesanan = Random.Range(1,2);
              orderlist[i] = new Order("test", pesanan);
              break;
            case 1:
            //jalan masuk
              break;
            case 2:
            //nunggu pesanan
              if(orderlist[i].kesabaran > 0){
                orderlist[i].kesabaran -= decaykesabaran;
              }else{
                //hilang kesabaran
                orderlist[i].state = 4;
              }
              break;
            case 3:
            //makan
              if(orderlist[i].waktumakan > 0){
                orderlist[i].waktumakan -= delaymakan;
              }else{
                //selesai makan
                orderlist[i].state = 4;
              }
              break;
            case 4:
            //pulang
              break;
          }
      }
    }
}

public class Order {
    public float waktumakan = 100;
    public string name;
    public int order;
    public int state = 0; //0 = kosong(cuman buat init), 1 = jalan ke kursi, 2 = nunggu pesanan, 3 = makan, 4 = jalan pulang
    public float kesabaran = 100;
    public Order(string name, int order){
      this.name = name;
      this.order = order;
      this.state = 1;
    }
    public Order(){
      this.state = 0;
    }
}
