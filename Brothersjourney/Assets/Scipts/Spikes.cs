﻿
using UnityEngine;
using System.Collections;

public class Spikes : MonoBehaviour
{
    public LayerMask enemyMask;
    public float speed;
    private float newSpeed;
    public float duration;


    Rigidbody2D myBody;
    Transform myTrans;
    float myWidth, myHeight;
    public int health = 2;
    private float dazedTime;
    public float startDazedTime;

    public GameObject bloodEffect;

    void Start()
    {
        myTrans = this.transform;
        myBody = this.GetComponent<Rigidbody2D>();
        SpriteRenderer mySprite = this.GetComponent<SpriteRenderer>();
        myWidth = mySprite.bounds.extents.x;
        myHeight = mySprite.bounds.extents.y;
    }
    private void Update()
    {
        if (dazedTime <= 0)
        {
            newSpeed = speed;
        }
        else
        {
            newSpeed = 0;
            dazedTime -= Time.deltaTime;
        }


        if (health <= 0)
        {

            Destroy(gameObject);
        }
    }
    void FixedUpdate()
    {
        //NOTE: This script makes use of the .toVector2() extension method.
        //Be sure you have the following script in your project to avoid errors
        //http://www.devination.com/2015/07/unity-extension-method-tutorial.html

        //Use this position to cast the isGrounded/isBlocked lines from
        Vector2 lineCastPos = myTrans.position.toVector2() - myTrans.right.toVector2() * myWidth + Vector2.up * myHeight;
        //Check to see if there's ground in front of us before moving forward
        //NOTE: Unity 4.6 and below use "- Vector2.up" instead of "+ Vector2.down"
        Debug.DrawLine(lineCastPos, lineCastPos + Vector2.down);
        bool isGrounded = Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down, enemyMask);
        //Check to see if there's a wall in front of us before moving forward
        Debug.DrawLine(lineCastPos, lineCastPos - myTrans.right.toVector2() * 0.2f);
        bool isBlocked = Physics2D.Linecast(lineCastPos, lineCastPos - myTrans.right.toVector2() * 0.2f, enemyMask);

        //If theres no ground, turn around. Or if I hit a wall, turn around
        if (!isGrounded || isBlocked)
        {
            Vector3 currRot = myTrans.eulerAngles;
            currRot.y += 180;
            myTrans.eulerAngles = currRot;
        }

        //Always move forward
        Vector2 myVel = NewMethod();
        myVel.x = -myTrans.right.x * newSpeed;
        myBody.velocity = myVel;



    }


    private Vector2 NewMethod()
    {
        return myBody.velocity;
    }

    public void Hurt()
    {
        if (health < 1)
            Destroy(this.gameObject);


    }

    public void TakeDamage(int damage)
    {
        dazedTime = startDazedTime;
        Instantiate(bloodEffect, transform.position, Quaternion.identity);
        health -= damage;
        Debug.Log("damage TAKEN!");

    }



}


