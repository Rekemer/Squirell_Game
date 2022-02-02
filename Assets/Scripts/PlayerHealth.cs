using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth :Health
{
    public System.Action<float> onHealthChanged;
    public System.Action OnPlayerDeath; 
    public System.Action OnPlayerDamage;
    Animator animator;


    protected override void Awake()
    {
        base.Awake();
        animator =GetComponent<Animator>();
    }
    public int GetHealth()
    {
        return health;
    }
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        OnPlayerDamage();
        onHealthChanged(damage);
    }

    // Update is called once per frame
    protected override  void Update()
    {
        base.Update();
        if (health <= 0)
        {
            
            if (OnPlayerDeath != null)
            {
                OnPlayerDeath();
            }
        }
    }
}
