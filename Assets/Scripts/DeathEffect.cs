using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Health))]
public class DeathEffect : MonoBehaviour
{
   public  System.Action DeathAnimation;
    Material material;

    float fade = 1f;

    Health health;
    [HideInInspector]
    public bool deathEffectActivated;
    private void Awake()
    {
        health = GetComponent<Health>();
        material = GetComponent<SpriteRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (health.health < 0)
        {
            deathEffectActivated = true;
        }
        if (deathEffectActivated)
        {
            fade -= Time.deltaTime;
            if (fade <= 0)
            {
                fade = 0;
                deathEffectActivated = false;
            }
            material.SetFloat("_Fade", fade);
        }
        

    }
}
