using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite nullHeart;
    public Sprite halfHeart;
    PlayerHealth health;
    private void Awake()
    {
        health = FindObjectOfType<PlayerHealth>();
        health.onHealthChanged += HandleHealthChange;
    }


    private void HandleHealthChange(float damage)
    {
          
        for(int i = 0; i < hearts.Length; i++)
        {


            //Subtract the amount of health leading up to this heart, what's left over tells us what state this heart should be in.
            //We clamp it since we know any value <= 0 would mean an empty heart, and any greater than 4 would be more than a full heart.
            //We multiply "i" by 4 because we are working with quarters and need to make the math match our health.
            // int remainderHealth = Mathf.Clamp(PlayerHealth - (i * 4), 0, 4);
            int remainderHealth = Mathf.Clamp(health.GetHealth() - (i * 2), 0, 2);
            hearts[i].sprite = remainderHealth switch
            {
                0 => nullHeart,
                1 => halfHeart,
                2 => fullHeart,
                _ => null
            };
            

        }
    }
}
