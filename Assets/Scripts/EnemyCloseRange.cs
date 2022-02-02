using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyCloseRange : Enemy
{   
   
    [SerializeField]
    public float TurnTime = 2f;

    public float AttackRechargingTime = 0.2f;

    [SerializeField]
    float DashSpeed = 6f;
   

    float myCollisionRadius;
    float targetCollisionRadious;
    [SerializeField]
    [Range(0f, 5f)]
    private float attackThreshHold;
   
    protected override void Awake()
    {
        base.Awake();
        myCollisionRadius = GetComponent<BoxCollider2D>().size.x / 2f;
        targetCollisionRadious = playerPos.GetComponent<BoxCollider2D>().size.x / 2f;
    }
   

   
    protected override IEnumerator  Attack()
    {
        state = State.Attacking;

        float timer = 0;


        while (true)
        {
            if (!CanSeePlayer())
            {
                timer += Time.deltaTime;
            }
            else
            {
                timer = 0;
            }
            if (timer >= timeOfNotSeeing)
            {
                break;
            }
            

            

            if (isCollided)
            {
                yield return new WaitForSeconds(0.8f);
                isCollided = false;
            }
            if (playerPos == null) break;
            Vector3 dirToPlayer = (playerPos.position - transform.position).normalized;
            Vector3 player = playerPos.position - dirToPlayer * (myCollisionRadius + targetCollisionRadious + attackThreshHold / 2);
            Vector2 displacement = player - transform.position;
            Vector2 velocity = displacement.normalized * DashSpeed;
            velocity.y = 0;

            transform.Translate(transform.right * Time.deltaTime * DashSpeed, Space.World);
            if (Vector2.Dot(dirToPlayer, transform.right) < 0)
            {
                yield return Going();
                if (playerPos == null) break;
                TurnTo(playerPos.position);
                
            }
            
           
            
            yield return null;
        }
        state = State.Idle;
        yield return null;

    }

  IEnumerator Going()
    {
        float timer = 0f;
        float timeOfGoing =0.6f;
        while (timer < timeOfGoing)
        {
            timer += Time.deltaTime;
            transform.Translate( transform.right * Time.deltaTime * DashSpeed, Space.World);
           
            yield return null;
        }

    }
}
