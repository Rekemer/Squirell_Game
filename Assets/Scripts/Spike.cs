using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike :Raycast_Controller
{
    private Player player;
     private float _distanceToTurnOn = 1f;
    Transform playerPos;
     float timeOfRecharging =2f;
    float time =0;
    [SerializeField]
    protected float playerBounce = 10;

    int touchDamage = 2;
    Bounds bounds;
    const float MIN_RAY = 0.062f;
    const float MAX_RAY = 0.31f;
    private void Awake()
    {
         
    
        base.Awake();
        player = FindObjectOfType<Player>();
        playerPos = player.gameObject.transform;
        SetSpike();
        UpdateRaycastOrigins();
    }
    public override void UpdateRaycastOrigins()
    {
       bounds = collider.bounds;
       

        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }
   private  void SetSpike()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (GameUI.gameOver == false && playerPos != null)
        {

        if ((transform.position- playerPos.position).sqrMagnitude < Mathf.Pow(_distanceToTurnOn, 2))
        {
        UpdateRaycastOrigins();

        for (int i = 0; i <verticalRayCount; i++) // vertical ray Count = 11
        {
            float directionY = transform.up.y;
            float directionX = transform.up.x;
            
          //  print("blue" + transform.up);
            float rayLength =CalculateRayLength(i);
            Vector2 rayOrigin = Vector2.zero;
            if (transform.rotation.eulerAngles.z == 0)
            {
                rayOrigin = raycastOrigins.bottomLeft;
            }
            else if (transform.rotation.eulerAngles.z == 90)
            {
                rayOrigin = raycastOrigins.topRight;
            }
            else if (transform.rotation.eulerAngles.z == 180)
            {
                rayOrigin = raycastOrigins.topLeft;
            }
            else if (transform.rotation.eulerAngles.z == 270)
            {
                rayOrigin = raycastOrigins.topLeft;
            }

            if (directionY == 1)
            {
                rayOrigin += Vector2.right * (verticalRaySpacing * i);
            }

            else if (directionY == -1)
            {
                rayOrigin += Vector2.right * (verticalRaySpacing * i);
            }

            else
            {
                if (directionX > 0)
                {
                    rayOrigin -= Vector2.up * (verticalRaySpacing * i);
                }
                else if (directionX < 0)
                {
                    rayOrigin -= Vector2.up * (verticalRaySpacing * i);
                }
            }

           
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, transform.up * rayLength, rayLength, collisionMask); // min - 0.062, 0.31 - max
            
            Debug.DrawRay(rayOrigin,  transform.up* rayLength, Color.red);

            if (hit)
            {
                
                if(Time.time  > time)
                {
                hit.transform.GetComponent<Health>().TakeDamage(touchDamage);
                   // print(true);
                if (hit.transform.CompareTag("Player"))
                {

                        //Vector2 normal = Vector2.Perpendicular(player.velocity);
                        //normal.x *= Mathf.Sign (player.velocity.x);
                        //normal.y *= Mathf.Sign (player.velocity.y);
                        //        Debug.DrawLine(transform.position, normal);
                        ////print("normal" +normal);
                        ////player.velocity = normal;
                        ///
                        //if (transform.rotation.x == 180)
                        //{
                        //}
                        //else if (transform.rotation.x == 0)
                        //{
                        //    playerPos.position = new Vector3(playerPos.position.x, bounds.max.y +0.5f, playerPos.position.z);

                        //}
                        //print(bounds.max.y);
                
                player.velocity = -player.velocity;
                //player.velocity *= 0.8f; // bounce coeff
                }
                // print("PLayer" + player.velocity);
                time = Time.time + timeOfRecharging;
                break;
                }

            }
        }
        }
        }
        
    }
    float CalculateRayLength(int i)
    {   if (i == 4 || i == 5 || i == 6)
        {
            return MAX_RAY;
        }
        else if (i < 4)
        {
            return (i+1) *MIN_RAY;
        }
        else if (i > 6)
        {
            float a = 2 * ( (i+1) - 6);
            return ((i+1)-a) * MIN_RAY;
        }
        return 0f;

    }
    public override void CalculateRaySpacing()
    {
         bounds = collider.bounds;
        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);
        verticalRaySpacing =0.062f;
    }
}
