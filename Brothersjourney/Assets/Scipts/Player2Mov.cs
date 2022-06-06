using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player2Mov : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    
   
    public static int health;

    public static Player2Mov instance2;
    public KeyCode left;
    public KeyCode right;
    public KeyCode jump;
    
    
    private Rigidbody2D theRB;

    public Transform groundCheckPoint;

    
    public float invincibleTimeAfterHurt = 2;


    public bool isGrounded;
    public float groundCheckRadius;
    public LayerMask whatIsGround;

    [HideInInspector]
    public Collider2D[] myColls2;

    private Animator animator;

    

    private void Start()
    {
        instance2 = this;
        myColls2 = this.GetComponents<Collider2D>();
        theRB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        

    }

    private void Update()
    {

        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsGround);
        if (Input.GetKey(left))
        {
            theRB.velocity = new Vector2(-moveSpeed, theRB.velocity.y);
      
        }
        else if (Input.GetKey(right))
        {
            theRB.velocity = new Vector2(moveSpeed, theRB.velocity.y);
           
        }

        else { theRB.velocity = new Vector2(0, theRB.velocity.y); }

        if (Input.GetKeyDown(jump) && isGrounded)
        {
            theRB.velocity = new Vector2(theRB.velocity.x,jumpForce);
            SoundManagerScript.PlaySound("jump");
        }

        if (theRB.velocity.x < 0)
        {
            transform.localScale = new Vector3(-0.2228052f, 0.2228052f, 0.2228052f);
        }else if (theRB.velocity.x > 0)
        {
            transform.localScale = new Vector3(0.2228052f, 0.2228052f, 0.2228052f);
        }

        animator.SetFloat("Speed",Mathf.Abs( theRB.velocity.x));
        animator.SetBool("Grounded", isGrounded);
    }



    public void TriggerHurt(float hurtTime)
    {
        StartCoroutine(Hurt(hurtTime));
    }

    public IEnumerator Hurt(float hurtTime)
    {
        int enemylayer = LayerMask.NameToLayer("Enemy");

        int player2layer = LayerMask.NameToLayer("player2");
        Physics2D.IgnoreLayerCollision(enemylayer, player2layer);
        foreach (Collider2D collider in Player2Mov.instance2.myColls2)
        {
            collider.enabled = false;
            collider.enabled = true;
        }
        animator.SetLayerWeight(1, 1);

        yield return new WaitForSeconds(hurtTime);
        Physics2D.IgnoreLayerCollision(enemylayer, player2layer, false);
        animator.SetLayerWeight(1, 0);

    }
    void Hurt()
    {
        health--;
        if (health <= 0)
            Application.LoadLevel(Application.loadedLevel);
        else
            TriggerHurt(invincibleTimeAfterHurt);

    }

  
    void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemy = collision.collider.GetComponent<Enemy>();
        if (enemy != null)
        {
            foreach (ContactPoint2D point in collision.contacts)
            {
                Debug.Log(point.normal);
                Debug.DrawLine(point.point, point.point + point.normal, Color.red, 10);
                
                if (point.normal.y >= 0.9f)
                {
                    Vector2 velocity = theRB.velocity;
                    velocity.y = jumpForce;
                    theRB.velocity = velocity;
                   
                    enemy.Hurt();
                }
                else
                {
                    Hurt();
                }
            }
        }
    }

    IEnumerator myWaitCoroutine()
    {
        
        yield return new WaitForSeconds(1f);// Wait for one second

        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        
            if (other.CompareTag("Spike"))
            {
                Debug.Log("batata");
                health--;
                TriggerHurt(invincibleTimeAfterHurt);
                //StartCoroutine(myWaitCoroutine());
                



            //if (counter == 0)
            //{
            //    Hurt();
            //    counter++;

            //}


        }

    }


}
