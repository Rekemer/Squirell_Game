using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Raycast : Raycast_Controller
{	[SerializeField]
	[Range(0f,5f)]
    private float rayLength = 1;
    private Player player;
    [SerializeField]
    protected float playerBounce = 10;
    [SerializeField]
    [Range(0f, 50f)]
    protected float enemyBounce;
    Enemy enemy;
    protected int touchDamage = 2;
    public Easing.Type easingType;
    public float easingMod;
    IEnumerator currentCourotine;
    [SerializeField]
    float timeDuration = 1.2f;
    private float _distanceToTurnOn = 1f;
    float time;
    public  override void Awake()
    {
        base.Awake();
        player = FindObjectOfType<Player>();
        
        enemy = GetComponent<Enemy>();
    }


    void Update()
    {
       

        UpdateRaycastOrigins();
       

		for (int i = 0; i < horizontalRayCount; i++)
		{
            float directionX = Mathf.Sign(transform.right.x);
            Vector2 rayOrigin = (transform.right.x > 0) ? raycastOrigins.bottomRight : raycastOrigins.bottomLeft;
            
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
			RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

			//Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.red);

            if (hit)
            {
                //print(hit.collider.tag);
                if (hit.transform.CompareTag("Player"))
                {
                   
                   // print("player got damage");
                    //if (player.HaveObjToThrow)
                    //{
                        if (Time.time > time)
                        {
                            time = Time.time + timeDuration;
                        hit.transform.GetComponent<Health>().TakeDamage(touchDamage);
                        player.velocity = transform.right * enemyBounce;
                        StartCoroutine(Bouncing());
                            break;
                        }
                    //}
                    //else
                    //{
                    //    player.velocity = transform.right * enemyBounce;
                    //}
                   
                   
                    
                   
                    enemy.isCollided = true;
                    break;
                }
                if (hit.transform.CompareTag("Ground") )
                {
                    //if (currentCourotine != null)
                    //{
                    //    //StopCoroutine(currentCourotine);
                    //    break;
                    //}
                    //else
                    //{
                    //    currentCourotine = Bouncing();
                    //    StartCoroutine(currentCourotine);
                    //}
                    
                    if (Time.time > time)
                    {
                        time = Time.time + timeDuration;
                        
                    StartCoroutine(Bouncing());
                        break;
                    }
                   
                   
                }
               
               
               
            }
        
        }
	}

   IEnumerator Bouncing()
    {
       
        float timeStart = Time.time;
        Vector3 start = transform.position;
        Vector3 finish = new Vector3(transform.position.x+ 3f * Mathf.Sign(enemy.currVel.x),transform.position.y,transform.position.z);
        
       
        while (true)
        {
            float u = (Time.time - timeStart) / timeDuration;
            if( u >= 1)
            {
                break;
            }
            
            u = Easing.Ease(u, easingType, easingMod);
            Vector3 p01 = (1 - u) * start + u * finish;
            transform.position = p01;
            yield return null;
        }
        
       
    }
}
   
