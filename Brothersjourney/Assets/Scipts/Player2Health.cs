using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player2Health : MonoBehaviour
{

    public int numberOfHearts;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;



    void Update()
    {
        if (Player2Mov.health > numberOfHearts)
        {
            Player2Mov.health = numberOfHearts;
        }




        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < Player2Mov.health)
            {
                hearts[i].sprite = fullHeart;

            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if (i < numberOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;

            }

        }
    }
}
