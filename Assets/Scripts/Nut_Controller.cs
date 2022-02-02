using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nut_Controller : MonoBehaviour,  Ilauncher
{
     public  Mouse_Controller mouse;
    private Player player;
    [SerializeField] public Nut prefab;
    [SerializeField] public Transform spawn;

    [SerializeField] public float minHeight;
    [SerializeField] public float maxHeight;
    [SerializeField] public float height;

    public  bool canAim;

    void Update()
    {
        if (Input.GetButtonDown("Aiming"))
        {
            if (player.HaveObjToThrow)
            canAim =!canAim;
        }
    }
    void Awake()
    {
        mouse = FindObjectOfType<Mouse_Controller>();
        player = GetComponent<Player>();
        canAim = false;
    }
    public void Launch()
    {
        player.HaveObjToThrow = false;
        Transform target = mouse.transform;
        var nut = Instantiate(prefab);
        nut.transform.position = spawn.position;
        canAim = false;
        Projectile_Proccesor.Launch( nut, target, spawn, height);
        
    }

    public bool GetInput()
    {
        if (player.HaveObjToThrow)
            return Input.GetButtonDown("Throwing"); // Throwing button  is f;
        else 
            return false;
    }
}
