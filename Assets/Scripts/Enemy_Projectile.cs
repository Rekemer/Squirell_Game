using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]

public class Enemy_Projectile : MonoBehaviour, IThrowable
{
    public int damage = 1;
    public Rigidbody2D rd { get => GetComponent<Rigidbody2D>(); set => GetComponent<Rigidbody2D>(); } // Lambda is like a function

  public void EnableCollider2D()
    {
        GetComponent<Collider2D>().enabled = true;
        //rd.isKinematic = false;
    }

    // Start is called before the first frame update
    void Awake()
    {
        rd = GetComponent<Rigidbody2D>();
        //rd.isKinematic = true;
        GetComponent<Collider2D>().enabled = false;
        Invoke("EnableCollider2D", 0.35f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Health>() != null)
        {
           
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
           
        }
        Destroy(gameObject);
    }
}
