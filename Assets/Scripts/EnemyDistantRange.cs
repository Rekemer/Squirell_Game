using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDistantRange : Enemy
{
    //override protected void Awake()
    //{
    //    base.Awake();
        


    //}
    protected override IEnumerator Attack()
    {
        
        if (state == State.Attacking )
        TurnTo(playerPos.position);
       
        float timer = 0;
        while (true)
        {
           
            if (!CanSeePlayer())
            {
                timer += Time.deltaTime;
                state = State.Idle;
            }
            else
            {
                timer = 0;
                state = State.Attacking;
            }
            if (timer >= timeOfNotSeeing)
            {
                state = State.Idle;
                break;
            }
            yield return null;
        }
        
        
        yield return null;
    }
}

