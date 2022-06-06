﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troll_Attack : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRange;
    public int damage;
    public Animator playerAnim;
    public KeyCode AttackButton;



    // Update is called once per frame
    void Update()
    {
        if (timeBtwAttack <= 0)
        {
            if (Input.GetKey(AttackButton))
            {
                playerAnim.SetTrigger("Throw");
                SoundManagerScript.PlaySound("hit");


                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                }
                timeBtwAttack = startTimeBtwAttack;
            }

        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
