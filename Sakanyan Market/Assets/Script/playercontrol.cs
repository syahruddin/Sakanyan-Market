using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontrol : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 1;
    // Start is called before the first frame update
    void Start()
    {
      rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W)) {
          rb.MovePosition(transform.position + new Vector3(0,1) * speed * Time.deltaTime);
        }else if (Input.GetKey(KeyCode.S)) {
          rb.MovePosition(transform.position + new Vector3(0,-1) * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A)) {
          rb.MovePosition(transform.position + new Vector3(-1,0) * speed * Time.deltaTime);
        }else if (Input.GetKey(KeyCode.D)) {
          rb.MovePosition(transform.position + new Vector3(1,0) * speed * Time.deltaTime);
        }
    }
}
