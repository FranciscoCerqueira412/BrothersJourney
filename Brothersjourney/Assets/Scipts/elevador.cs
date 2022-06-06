using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevador : MonoBehaviour
{
    public float speed = 1f;
    public GameObject movePlatform;
    private void OnTriggerStay2D(Collider2D other)
    {
        if ((other.tag == "Player") )
        {
            movePlatform.transform.position += movePlatform.transform.up * speed;
        }
    }
}
