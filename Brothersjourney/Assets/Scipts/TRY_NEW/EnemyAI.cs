using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class EnemyAI : MonoBehaviour
{
    [Header("Variáveis do inimigo")]
    public float speed=2;
    public float size;
    public int hp;
    public Animator animator;
    [Header("Pontos de patrulha")]
    public List<Transform> points;
    private int idChangeValue = 1;
    private int nextID=0;


    //inicializações
    public void Awake()
    {
        animator = GetComponent<Animator>();

    }
    //Atualização constante do pathing dos inimigos
    private void Update()
    {
        MoveToNextPoint();

    }
    //Movimentação dos inimigos atraves de checkpoints no mapa
    void MoveToNextPoint()
    {
        Transform goalPoint = points[nextID];
        if(goalPoint.transform.position.x >transform.position.x)
        {
            transform.localScale = new Vector3(size, size, size);
        }
        else
            transform.localScale = new Vector3(-size, size, size);

        transform.position = Vector2.MoveTowards(transform.position,goalPoint.position,speed*Time.deltaTime);

        if (Vector2.Distance(transform.position, goalPoint.position) < 0.2f)
        {
            if (nextID == points.Count - 1)
                idChangeValue = -1;

            if (nextID == 0)
                idChangeValue = 1;

            nextID += idChangeValue;

        }
    }
    //Ativar animação de atacar caso colida com o player
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            animator.SetBool("isAttacking", true);
        }
    }
    //Retornar à animaçao de andar caso o inimigo não esteja em contacto com o player
    public void WalkAgain()
    {
        animator.SetBool("isAttacking", false);
        
    }

}
