using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troll_GREEN : MonoBehaviour
{
    [Header("Movimentação")]
    public KeyCode left;
    public KeyCode right;
    public KeyCode jump;
    [Header("Variáveis movimento")]
    public float moveSpeed;
    public float jumpForce;
    public float size;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    public Transform groundCheckPoint;
    public float knockBack;
    public float knockBackLength;

    [HideInInspector] public enum State { idle, running, jumping, hurt }
    [HideInInspector] public State state = State.idle;
    [HideInInspector] public bool isGrounded;
    [HideInInspector] public float knockBackCount;
    [HideInInspector] public bool knockFromRight;
    Troll_Health_GREEN THG;


    private Rigidbody2D rb;
    private Animator animator;


    //Inicializãçoes ao iniciar
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        THG= GetComponent<Troll_Health_GREEN>();
        Physics2D.IgnoreLayerCollision(19, 20, true);

    }

    //Movimentação do player
    void Update()
    {

        if (state != State.hurt)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsGround);
            if (Input.GetKey(left))
            {
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            }
            else if (Input.GetKey(right))
            {
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            }

            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }


            if (Input.GetKeyDown(jump) && isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                state = State.jumping;
                SoundManagerScript.PlaySound("jump");

            }

            if (rb.velocity.x < 0)
            {
                transform.localScale = new Vector3(-size, size, size);
            }
            else if (rb.velocity.x > 0)
            {
                transform.localScale = new Vector3(size, size, size);
            }




        }

        VelocitySwitch();
        animator.SetInteger("state", (int)state);



    }
    //Recuperar vida ao colidir com item
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "HPGREEN" )
        {
                if (FindObjectOfType<Troll_Health_GREEN>().currentHealth < 4)
                    //Destroy(collision.gameObject);
                    THG.GainHP();




        }
    }
    //inimigo dá dano e knockback no player
    public void OnCollisionEnter2D(Collision2D collision)
    {

        if (rb.velocity.y < 0 && collision.gameObject.tag == "Spike")
        {
            rb.velocity = new Vector2(rb.velocity.x, 14);
            state = State.jumping;
        }
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Spike")
        {
            state = State.hurt;
            SoundManagerScript.PlaySound("takingdamage");
            FindObjectOfType<Troll_Health_GREEN>().LoseLife();
            knockBackCount = knockBackLength;

            if (collision.transform.position.x < transform.position.x)
            {
                knockFromRight = false;
                knock();
            }
            else
                knockFromRight = true;
                knock();

        }


    }
    //Verificar se a colisao foi da esquerda ou direita
    public void knock()
    {

        if (knockFromRight == true)
        {
            rb.velocity = new Vector2(-knockBack, knockBack);
        }
        if (!knockFromRight == true)
        {
            rb.velocity = new Vector2(knockBack, knockBack);
        }
        knockBackCount = 0;




    }
    //Escolha de estados de animação
    private void VelocitySwitch()
    {
        if (state == State.jumping)
        {
            if (rb.velocity.y > .1f)
            {
                state = State.jumping;
            }
            else if (state == State.jumping)
            {
                if (isGrounded == true)
                    state = State.idle;
            }
        }
        else if (state == State.hurt)
        {
            if (Mathf.Abs(rb.velocity.y) < .1f)
            {
                state = State.idle;
            }
        }
        else if (Mathf.Abs(rb.velocity.x) > Mathf.Epsilon)
        {
            state = State.running;
        }

        else
        {
            state = State.idle;
        }


    }


}
