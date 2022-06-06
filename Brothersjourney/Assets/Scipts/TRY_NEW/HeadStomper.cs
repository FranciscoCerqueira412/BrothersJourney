using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadStomper : MonoBehaviour
{
    public float alturaPulo;
    [HideInInspector]public GameObject obj;


    private Rigidbody2D rb;

    //inicializações
    public void Awake()
    {
        rb = transform.parent.GetComponent<Rigidbody2D>();
        obj = GetComponent<GameObject>(); 
    }
    //Dar bounce no topo dos inimigos
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            rb.velocity = new Vector2(rb.velocity.x, alturaPulo);
            SoundManagerScript.PlaySound("bounce");
        }
       
    }
}
