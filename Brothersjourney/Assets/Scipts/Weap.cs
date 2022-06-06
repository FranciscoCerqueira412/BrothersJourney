using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weap : MonoBehaviour
{
      public GameObject projectile;
    public GameObject shotEffect;
    public Transform shotPoint;
    public Animator camAnim;

    private float timeBtwShots;
    public float startTimeBtwShots;

    private void Update()
    {
        

        if (timeBtwShots <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                Instantiate(shotEffect, shotPoint.position, Quaternion.identity);
                
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }


    }
}