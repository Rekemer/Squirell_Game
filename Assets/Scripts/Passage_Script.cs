using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passage_Script : MonoBehaviour
{
    [SerializeField]float howLongNutCanbeStuck =1;

    float time = 0;
    public Vector2 force;
    public float coeff;
    private Player player;
    BoxCollider2D collider;
    Bounds bounds;
    bool isTrigger;
    private void Awake()
    {
        player = FindObjectOfType<Player>();
        collider = GetComponent<BoxCollider2D>();
        bounds = GetComponent<BoxCollider2D>().bounds;
        isTrigger = collider.isTrigger;
    }
    private void Update()
    {
       
        if (player.HaveObjToThrow && !isTrigger)
        {
            gameObject.layer = 12;
            collider.enabled = true;
        }
        else
        {
            if (!isTrigger)
            {
                collider.enabled = false;
                gameObject.layer = 0;
            }
        }
    }
    
    float FindPositionRelativeToCenter(Transform obj)
    {
        Bounds bounds = collider.bounds;
        float middle = bounds.center.x;
        
        if (obj.position.x > middle && obj.position.x < bounds.max.x)
        {
         
            return 1;
        }else
        if (obj.position.x < middle && obj.position.x > bounds.min.x)
        {

          
            return -1;
        }
        return 0;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //print(collision.transform.tag);
       if (collision.CompareTag("Nut"))
       {
            time += Time.deltaTime;
           
            if (time >= howLongNutCanbeStuck)
            {
                
                collision.GetComponent<Rigidbody2D>().AddForce(FindPositionRelativeToCenter(collision.transform)* force);
                time = 0;
            }
       }
    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<Sprite_Controller>().isFat == true)
            {
                gameObject.layer = 6;
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.layer = 0;
        }
    }
}
