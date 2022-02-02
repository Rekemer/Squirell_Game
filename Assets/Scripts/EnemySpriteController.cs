using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpriteController : MonoBehaviour
{
    Enemy enemy;
    Animator animator;
    public System.Action OnEnemyDamage;
    public Health health { get; private set; }

    [SerializeField]
    [Range(1f,5f)]
    float multiplierAnimation;
    //public System.Action<Vector3> updatingAnimation;
    private void Awake()
    {
        enemy = GetComponent<Enemy>();
       // enemy.updatingAnimation += SetSpeedInAnimator;
        animator = GetComponent<Animator>();
       health = GetComponent<Health>();
        OnEnemyDamage += GettingHurtAnimation;
    }


    void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(enemy.currVel.x));
        if (enemy.currVel.x > 3f)
        {
            animator.SetFloat("Multiplier", multiplierAnimation);
        }
       
      
    }

    void GettingHurtAnimation()
    {
        animator.SetTrigger("Hurt");
    }
}
