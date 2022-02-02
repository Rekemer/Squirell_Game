using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class Detector_Insect : MonoBehaviour

{
    private float _distanceToTurnOn = 5f;
    public float skinWidth = 0;
    BoxCollider2D collider;
    Vector2 topLeft, topRight;
    public int rayCount = 3;
    public float rayLength = 0.18f;
    float spaceBetwRays;
    public LayerMask collisionMask;
    Player playerScr;
    Vector2 direction;
    Vector3 previous;

    Rigidbody2D rd;
    private Health health;

    
    Transform player;
    public float bounceCooef;

    private void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        CalculateRaySpacing();
        rd = transform.parent.GetComponent<Rigidbody2D>();
        health = transform.parent.GetComponent <Health>();
        playerScr = FindObjectOfType<Player>();
        player = playerScr.gameObject.transform;
    }
    void CalculateRaySpacing()
    {
        Bounds bounds = collider.bounds;
        bounds.Expand( skinWidth * (-2f));
        rayCount = Mathf.Clamp(rayCount, 2, int.MaxValue);
        spaceBetwRays = bounds.size.x / (rayCount - 1);
    }
    void IfHitByPlayer()
    {
        

       
        for (int i = 0; i < rayCount; i++)
        {
            Vector2 rayOrigin = topLeft;
            rayOrigin +=  (spaceBetwRays * i)*Vector2.right;
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up, rayLength, collisionMask);
            
            if (hit)
            {
                Bounds bounds = collider.bounds;
                
             
                    player.position = new Vector3(player.position.x, bounds.max.y + 0.7f, player.position.z);
                    
                    playerScr.velocity.y = -playerScr.velocity.y;
                    playerScr.velocity *= bounceCooef;
                   // print("enemy got damage" + playerScr.damage);
                    health.TakeDamage(playerScr.damage);
                    break;
                
               
            }
        }
    }
    private void Update()
    {
        if (GameUI.gameOver == false && player != null)
        {
        if((transform.position - player.position).sqrMagnitude < Mathf.Pow(_distanceToTurnOn, 2))
        {
        CalculateRaySpacing();
        UpdateRayCastOrigin();
        IfHitByPlayer();

        }

        }



        
    }
    void UpdateRayCastOrigin()
    {
        Bounds bounds = collider.bounds;
        bounds.Expand(skinWidth * -2f);
        topLeft = new Vector2(bounds.min.x, bounds.max.y);
       
    }
}
