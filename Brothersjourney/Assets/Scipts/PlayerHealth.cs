using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public int numberOfHearts;
    
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;


   
    void Update()
    {
        if (PlayerMov.health > numberOfHearts)
        {
            PlayerMov.health = numberOfHearts;
        }




        for(int i = 0; i < hearts.Length; i++)
        {
            if(i< PlayerMov.health)
            {
                hearts[i].sprite = fullHeart;

            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if (i < numberOfHearts)
            {
                hearts[i].enabled=true;
            }
            else
            {
                hearts[i].enabled = false;

            }

        }
      
        }
}
