using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprite_Controller : MonoBehaviour
{
    [SerializeField]
    private float coolDownTime;

    // Sprites 
    SpriteRenderer currentSprite;

    [SerializeField]
    Sprite Fat;
    [SerializeField]
    Sprite normal;

    public System.Action Dust;
    public System.Action DustPos;
    // Animation
    Animator animator;
    Vector2 input;
    // PlayerInput
    Player playerScript;

    // check of weight

   
    //Attack_Position
    Transform attack;
    // Lauch_Point Position
    Transform LauchPointPosition;

    public bool isFat { get; private set; }

    private Controller_2D controller2d;

    void Awake()
    {
        LauchPointPosition = gameObject.transform.GetChild(0);
        attack = gameObject.transform.GetChild(1);
        currentSprite = GetComponent<SpriteRenderer>();
        FindObjectOfType<PlayerHealth>().OnPlayerDamage = GettingHurtAnimation;
        animator = GetComponent<Animator>();
        playerScript = GetComponent<Player>();
        isFat = false;
        controller2d = GetComponent<Controller_2D>();
       
    }

    private void GettingHurtAnimation()
    {
        animator.SetTrigger("Hurt");
    }

    void Update()
    {
        
        if (playerScript.HaveObjToThrow)  
        {
            if (isFat == false)
            {
                animator.SetTrigger("Fat");
                animator.SetBool("IsFat", true);
                playerScript.damage = 3;
                isFat = true;
                 
                ChangeCollider();
            }
        }
        else
        {
            if (isFat == true)
            {
                animator.SetTrigger("Fat");
                animator.SetBool("IsFat", false);
                isFat = false;
                playerScript.damage = 1;
                ChangeCollider();

            } 
        }
       
        
       
        if (playerScript.input.x == 1 && !playerScript.disabled)
        {
            if (transform.eulerAngles.y ==180)
            {
                //currentSprite.flipX = false;
                transform.eulerAngles = new Vector3(0, 0, 0);
                Dust();
            }
            if (LauchPointPosition.localPosition.x < 0)
            {
               // ReverseLaunchAndAttackObjects();
            }
                
            
            //    throwPoint.localPosition = new Vector3(-throwPoint.localPosition.x, throwPoint.localPosition.y, throwPoint.localPosition.z);
        }
        else if (playerScript.input.x == -1 && !playerScript.disabled)
        {
            if (transform.eulerAngles.y == 0)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                Dust();
            }
            
            if (LauchPointPosition.localPosition.x > 0)
            {
                //ReverseLaunchAndAttackObjects();
            }
               
            
            //    throwPoint.localPosition = new Vector3(-throwPoint.localPosition.x, throwPoint.localPosition.y, throwPoint.localPosition.z);
        }
        
    }

    void ReverseLaunchAndAttackObjects()
    {
        LauchPointPosition.localPosition = new Vector3(-LauchPointPosition.localPosition.x, LauchPointPosition.localPosition.y, LauchPointPosition.localPosition.z);
        attack.localPosition = new Vector3(-attack.localPosition.x, attack.localPosition.y, attack.localPosition.z);
    }
    public void UpdateSprite( float velocityX, float velocityY)
    {
       
        //if (velocityY < 0)
        //{

        //    animator.SetTrigger("Down");
        //}
        //else if (velocityY > 0)
        //{
        //    animator.SetTrigger("Up");
        //}
        animator.SetFloat("SpeedY", velocityY);
            animator.SetFloat("Speed", Mathf.Abs(velocityX));
        
    }
    void ChangeCollider()
    {
        Vector2 S;
        if (isFat == true)
           {
            S = new Vector2(0.8487263f, 1.166667f);
           }
        else
            S = normal.bounds.size;

        gameObject.GetComponent<BoxCollider2D>().size = S;
        if(isFat)
        gameObject.GetComponent<BoxCollider2D>().offset =  new Vector2(0.25f,0f); 
        else
        {
            gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(0f, 0f);
        }
        controller2d.CalculateRaySpacing();
        transform.position = transform.position + new Vector3(0, 0.5f, 0);
        DustPos();
      
    }

  
}
