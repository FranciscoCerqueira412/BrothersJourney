using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Troll_Health_GREEN : MonoBehaviour
{
    [Header("Vida_Player")]
    public Image[] lives;
    public int currentHealth;
    public int maxHealth;
    private bool dead;

    [Header("Hurt")]
    [SerializeField] private float iframesDuration;
    [SerializeField] private float numberOffFlashes;
    public GameObject bloodEffect;

    private Animator animator;
    private Rigidbody2D rb;


    //Inicializãçoes
    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }


    private void Update()
    {
        Physics2D.IgnoreLayerCollision(21, 22, true);
    }
    //Função para o player perder vida 
    public void LoseLife()
    {

        currentHealth--;
        GameObject sangue = Instantiate(bloodEffect, transform.position, Quaternion.identity);
        Destroy(sangue, 2f);
        lives[currentHealth].enabled = false;
        if (currentHealth > 0)
            StartCoroutine(Inv());

        if (currentHealth == 0)
        {
            if (!dead)
                CheckDead();


        }

    }
    //Função para o player ganhar vida
    public void GainHP()
    {
        if (currentHealth<maxHealth)
        {
            currentHealth += 1;
            lives[currentHealth - 1].enabled = true;
        }
        


    }
    //Timer Invulnerabilidade depois de ser atingido
    private IEnumerator Inv()
    {
        SpriteRenderer[] All = GetComponentsInChildren<SpriteRenderer>();


        for (int i = 0; i < numberOffFlashes; i++)
        {
            Physics2D.IgnoreLayerCollision(9, 10, true);
            foreach (var sr in All)
            {
                sr.color = new Color(0.1603774f, 0.1603774f, 0.1603774f, 0.6745098f);

            }

            yield return new WaitForSeconds(iframesDuration / (numberOffFlashes * 2));

            foreach (var sr in All)
            {
                sr.color = new Color(0.3264753f, 0.5849056f, 0.1076006f, 1f);
            }
            yield return new WaitForSeconds(iframesDuration / (numberOffFlashes * 2));
            foreach (var sr in All)
            {
                sr.color = new Color(0.3264753f, 0.5849056f, 0.1076006f, 1f);
            }
            yield return new WaitForSeconds(iframesDuration / (numberOffFlashes * 2));
            Physics2D.IgnoreLayerCollision(9, 10, false);
        }

    }
    //Tempo antes de dar reset na scene
    private IEnumerator TimePreDead()
    {

        yield return new WaitForSeconds(2);
        FindObjectOfType<LevelManager>().Restart();
    }
    //Player está morto?
    public void CheckDead()
    {

        animator.SetTrigger("isDead");
        dead = true;
        GetComponent<Troll_GREEN>().enabled = false;
        rb.simulated = false;
        //rb.bodyType = RigidbodyType2D.Static;
        StartCoroutine(TimePreDead());
    }

}
