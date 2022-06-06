using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;


    public static PlayerMov instance;
    public KeyCode left;
    public KeyCode right;
    public KeyCode jump;

    private Rigidbody2D theRB;


    public static int health = 3;
    public float invincibleTimeAfterHurt = 2;


    public Transform groundCheckPoint;

    public bool isGrounded;
    public float groundCheckRadius;
    public LayerMask whatIsGround;

    [HideInInspector]
    public Collider2D[] myColls;

    private Animator animator;

    


    private void Start()
    {
        instance = this;
        myColls = this.GetComponents<Collider2D>();
        theRB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        health = 3;
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
            transform.localScale = new Vector3(-0.3f, 0.3f, 0.3f);
        }else if (theRB.velocity.x > 0)
        {
            transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        }

        animator.SetFloat("Speed",Mathf.Abs( theRB.velocity.x));
        animator.SetBool("Grounded", isGrounded);
    }

    public void TriggerHurt(float hurtTime)
    {
        StartCoroutine(Hurt(hurtTime));
    }

    IEnumerator Hurt(float hurtTime)
    {
        int enemylayer = LayerMask.NameToLayer("Enemy");
        int playerlayer = LayerMask.NameToLayer("player");

        Physics2D.IgnoreLayerCollision(enemylayer, playerlayer);
        foreach (Collider2D collider in PlayerMov.instance.myColls)
        {
            collider.enabled = false;
            collider.enabled = true;
        }
        animator.SetLayerWeight(1, 1);

        yield return new WaitForSeconds(hurtTime);
        Physics2D.IgnoreLayerCollision(enemylayer, playerlayer, false);
        animator.SetLayerWeight(1, 0);

    }

    [System.Obsolete]
    void Hurt()
    {
        health--;
        if (health <= 0)
            Application.LoadLevel(Application.loadedLevel);
        else
            TriggerHurt(invincibleTimeAfterHurt);
     
    }

    [System.Obsolete]
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

}
