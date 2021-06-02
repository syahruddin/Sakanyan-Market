using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteScript : MonoBehaviour
{
    public bool ondrag = false;
    public Vector3 screenPoint;
    public Vector3 offset;
    public Kodepelanggan pelanggan;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnMouseDown(){
      if(pelanggan.pelanggan.state == 2 & !UI_Controller.isPaused){
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
    //  dapatkursi();
    }
}
