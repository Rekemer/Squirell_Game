using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ForceJump : MonoBehaviour
{
   
    private Player player;
    private Controller_2D controller;
    private float holdDownStartTime;
    [SerializeField] private Transform forceTransform;
    private SpriteMask forceSpriteMask;
    public const float MAX_FORCE =20f;
    public float coolDownTime;
    [SerializeField] int JumpLimit;
    public bool CanJump ;

   public  static System.Action CanMove;
    public System.Action Dust;
    [SerializeField]
    public float coeffHowHigh;
    public bool HasJumped;

    public Transform placeForForceIndicator;
    void Awake()
    {
        CanJump = true;
        player = GetComponent<Player>();
        controller = GetComponent<Controller_2D>();
        forceSpriteMask = forceTransform.Find("Mask").GetComponent<SpriteMask>();
        HideForce();
      
    }



    void Update()
    {
      
        forceTransform.position = placeForForceIndicator.position;
       
        Vector2 dir = (GetMouseWorldPosition() - transform.position).normalized;
        forceTransform.eulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(dir));
        
            if (controller.collisions.below && player.HaveObjToThrow && CanJump)
            {
           
            player.targetVelocityX = 0f;
            if (Input.GetMouseButtonDown(0) )
            {
                holdDownStartTime = Time.time;
                if (CanMove != null)
                    CanMove();

            }
            if (Input.GetMouseButton(0))
            {
                float holdDownTime = Time.time - holdDownStartTime;
                
                ShowForce(CalculateHoldDownForce(holdDownTime));
            }
            if (Input.GetMouseButtonUp(0) && controller.collisions.below && player.HaveObjToThrow)
            {
                float holdDownTime = Time.time - holdDownStartTime;
                // print(holdDownTime);
                float targetX = player.moveSpeed * Input.GetAxis("Horizontal");

                //player.velocity.y = Mathf.Clamp(holdDownTime * player.jumpVelocity, 0, JumpLimit);
                //player.velocity.x = targetX * holdDownTime;
                
                player.velocity = Mathf.Clamp(holdDownTime * player.maxJumpVelocity*coeffHowHigh, 0, JumpLimit) * ((GetMouseWorldPosition() - transform.position).normalized * -1f);
                player.velocity.z = 0;
              
               
                Dust();
                CanJump = false;
                Invoke("EnablingJump", coolDownTime);
                EventFunc();
                
                HideForce();
                HasJumped = true;
                Invoke("HasNotJumped", 0.1f);
            }

        }
    }
  

    void EnablingJump()
    {
        CanJump = true;
        holdDownStartTime = Time.time;
    }
    void HasNotJumped()
    {
        HasJumped = false;
    }
    private void EventFunc()
    {
        CanMove -= player.Disable;
        CanMove += player.UnDisable;
        CanMove();
        CanMove -= player.UnDisable;
        CanMove += player.Disable;
    }
    public void ShowForce(float force)
    {
        forceSpriteMask.alphaCutoff = 1 - force / MAX_FORCE;
        //print(forceSpriteMask.alphaCutoff);
    }
    private void HideForce()
    {
        forceSpriteMask.alphaCutoff = 1;
    }
    private float CalculateHoldDownForce(float holdTime)
    {
        float maxForceHoldDownTime = 2f;
        float holdTimeNormalized = Mathf.Clamp01(holdTime / maxForceHoldDownTime);
        float force = holdTimeNormalized * MAX_FORCE;
       
        return force;
    }

    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }
    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
    public static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }
}

    
