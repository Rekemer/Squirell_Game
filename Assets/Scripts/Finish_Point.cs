using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish_Point : MonoBehaviour
{
    public System.Action OnReachedEndOfLevel;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<Player>();
        if (player!= null && player.HaveObjToThrow)
            if (OnReachedEndOfLevel != null)
            {
                player.Disable();
                OnReachedEndOfLevel();
                //print("Victory!");
            }
       
    }
}
