using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    private EnemySpriteController spriteController;
    private Enemy enemy;

    private void Awake()
    {
        base.Awake();
        spriteController = GetComponent<EnemySpriteController>();
      enemy = GetComponent<Enemy>();
    }
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        spriteController.OnEnemyDamage();

    }
    
    protected override void Update()
    {
        if (health <= 0)
        {
            if (death == null) Destroying();
            if (death != null)
            {
                //Projectile_Launcher launcher = GetComponent<Projectile_Launcher>();
                //if (launcher!= null)
                //{
                //    launcher.enabled = false;
                //}
                transform.GetChild(1).GetComponent<Detector_Insect>().enabled = false;
                GetComponent<Enemy_Raycast>().enabled = false;
                death.deathEffectActivated = true;
                Invoke("Destroying", 1f);
                enemy.StopAllCoroutines();
               GetComponent<Rigidbody2D>().isKinematic = true;
                GetComponent<BoxCollider2D>().enabled = false; 
                enemy.enabled = false;
                
                transform.GetChild(1).GetComponent<BoxCollider2D>().enabled = false;
                this.enabled = false;
            }

        }

    }

}
