using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfLevel : MonoBehaviour
{
    
    void Start()
    {
        
    }

   
    void Update()
    {
        if(Mathf.Abs(transform.position.y) > 100)
        {
            gameObject.GetComponent<Health>().TakeDamage(100);
        }
    }
}
