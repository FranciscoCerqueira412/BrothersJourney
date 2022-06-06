using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField]
    GameObject door;
    bool isOpened=false;
    private float timer;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isOpened == false)
        {
            isOpened = true;
            door.transform.position += new Vector3(0, 2, 0);
        }
    }
   
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isOpened == true)
        {
            timer = 1f;
            door.transform.position -= new Vector3(0, 2, 0);
            isOpened = false;

        }
    }

}
