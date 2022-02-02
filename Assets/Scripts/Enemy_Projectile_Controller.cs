using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Projectile_Controller : MonoBehaviour, Ilauncher
{
    Enemy enemy;
    [SerializeField] Enemy_Projectile prefab_kinematic;
    [SerializeField] Enemy_Projectile prefab_dynamic;
    Transform spawn;
    [SerializeField] [Range(1, 20)] float height;
    public float msBetweenShots = 1000;
    float nextShotTime;

   public Transform target;

    public float howLongToShoot = 5f;

    public bool fireMode;
    private float howLongToShoot_2 ;
    [Range(1f,5f)]
    public float timeOfWaiting;


    public bool haveIntervalsOfShooting;
    void Awake()
    {
        enemy = GetComponent<Enemy>();
        spawn =gameObject.transform.GetChild(2).transform;
        
            howLongToShoot_2 = howLongToShoot;
            
        
        
    }
    public bool GetInput()
    {
        if (enemy.state == Enemy.State.Attacking)
            return true;
        else return false;
    }

    public void Launch()
    {
        
        if (fireMode)
        {
            float _height;
            if (enemy.playerPos != null)
            {
                if(enemy.state == Enemy.State.JustShooting) _height = Mathf.Clamp(Mathf.Abs(target.position.y- transform.position.y), 1f, height);
                else 
                _height = Mathf.Clamp(Mathf.Abs(enemy.playerPos.position.y - transform.position.y), 1f, height);
            
            if (Time.time > nextShotTime)
            {
                nextShotTime = Time.time + msBetweenShots / 1000;
                
                var enemyProjectile = Instantiate(prefab_dynamic);
                enemyProjectile.transform.position = spawn.position;
                
                if (enemy.state == Enemy.State.Attacking)
                Projectile_Proccesor.Launch(enemyProjectile, enemy.playerPos, spawn, _height);
                else if (enemy.state == Enemy.State.JustShooting)
                {
                    Projectile_Proccesor.Launch(enemyProjectile, target, spawn, _height);
                }
            }

            }
        }
        else
            FireWeapon();
    }

    private void FireWeapon()
    {
        if (haveIntervalsOfShooting == true)
        {
            
            howLongToShoot -=  Time.deltaTime;
        
            if (howLongToShoot > 0)
            {
            
                if (Time.time > nextShotTime  )
                {
                   // print("Shooting");
                    nextShotTime = Time.time + msBetweenShots / 1000;

                    var enemyProjectile = Instantiate(prefab_kinematic);
                    //enemyProjectile.rd.isKinematic = true; 
                    enemyProjectile.transform.position = spawn.position;
                    enemyProjectile.rd.velocity = transform.right *5 ;
                }
            }
            else
            {
                Invoke("SetTime", timeOfWaiting);
               //print("Cant shoot");
            }
        }
        else
        {

            if (Time.time > nextShotTime)
            {
               // print("Shooting");
                nextShotTime = Time.time + msBetweenShots;

                var enemyProjectile = Instantiate(prefab_kinematic);
                //enemyProjectile.rd.isKinematic = true; 
                enemyProjectile.transform.position = spawn.position;
                enemyProjectile.rd.velocity = transform.right * 5;
            }
        }
    }

    void SetTime()
    {
        howLongToShoot = howLongToShoot_2;
    }
}