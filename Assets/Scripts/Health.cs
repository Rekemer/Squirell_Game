using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Health : MonoBehaviour
{
    [HideInInspector]
     public  int health;

    protected DeathEffect death;
    public int currHealth;
    protected BoxCollider2D collider;
    public virtual  void TakeDamage(int damage)
    {
       health-= damage;
     
       
    }
    protected virtual void Awake()
    {
        health = currHealth;
        death = GetComponent<DeathEffect>();
        collider = GetComponent<BoxCollider2D>();
    }
    protected virtual  void Update()
    {
        if (health <= 0)
        {
            if (death == null) Destroying();
            if (death != null) {
                death.deathEffectActivated = true;
                Invoke("Destroying", 1f);
                
               // collider.enabled = false;
            }

        }
       
    }

    protected virtual void Destroying()
    {
        Destroy(gameObject);
    }

}
