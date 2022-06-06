using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : MonoBehaviour
{

    #region Public Variables;
    public Transform rayCast;
    public LayerMask raycastMask;
    public float rayCastLength;
    public float attackDistance;
    public float moveSpeed;
    public float timer;
    #endregion

    #region private Variables
    private RaycastHit2D hit;
    private GameObject target;
    private Animator anim;
    private float distance;
    private bool attackMode;
    private bool inRange;
    private bool cooling;
    private float intTimer;
    #endregion


    void RaycastDebugger()
    {
        if(distance > attackDistance)
        {
            Debug.DrawRay(rayCast.position, Vector2.right * rayCastLength, Color.red);
        }
        else if(attackDistance>distance)
        {
            Debug.DrawRay(rayCast.position, Vector2.right * rayCastLength, Color.green);
        }
    }
    private void Awake()
    {
        intTimer = timer;
        anim = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        if (inRange)
        {
            hit = Physics2D.Raycast(rayCast.position, Vector2.right, rayCastLength, raycastMask);
            RaycastDebugger();
        }

        if (hit.collider != null)
        {
            EnemyLogic();
        }
        else if(hit.collider == null)
        {
            inRange = true;
        }
        if (inRange == false)
        {
            anim.SetBool("canWalk", false);
            StopAttack();
        }
    }

    void EnemyLogic()
    {
        NewMethod();
        if (distance > attackDistance)
        {
            Move();
            StopAttack();
        }
        else if (attackDistance >= distance && cooling == false)
        {
            Attack();
        }
        if (cooling)
        {
            anim.SetBool("Attack", false);
        }
    }

    private void NewMethod()
    {
        distance = Vector2.Distance(transform.position, target.transform.position);
    }

    void Move()
    {
        anim.SetBool("canWalk", true);
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Golem_attack"))
        {
            Vector2 targetPosition = new Vector2(target.transform.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    void Attack()
    {
        timer = intTimer;
        attackMode = true;
        anim.SetBool("canWalk", false);
        anim.SetBool("Attack", true);
    }
    void StopAttack()
    {
        cooling = false;
        attackMode = false;
        anim.SetBool("Attack", false);
    
    }



    private void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.tag == "player" )
        {
            target = trig.gameObject;
            inRange = true;
        }
    }
}

    
