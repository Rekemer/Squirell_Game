using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Rigidbody2D))]
public class Nut :MonoBehaviour, IThrowable, IEatable
{
    //public Rigidbody2D GetPhysics()
    //{
    //    return GetComponent<Rigidbody2D>();
    //}
    
    public Rigidbody2D rd { get =>  GetComponent<Rigidbody2D>(); set => GetComponent<Rigidbody2D>(); } // Lambda is like a function

    private void Awake()
    {

        rd = GetComponent<Rigidbody2D>();
        //rd.isKinematic = true;
        GetComponent<Collider2D>().enabled = false;
        Invoke("EnableCollider2D", 0.1f);
    }

   public void EnableCollider2D()
    {
        GetComponent<Collider2D>().enabled = true;
    }

    



}
