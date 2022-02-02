using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dust_Effect : MonoBehaviour
{
    [SerializeField]
    ParticleSystem dust;
    Player player;
    Sprite_Controller sprite;
    ForceJump force;

    
    void Awake()
    {
        player = FindObjectOfType<Player>();
        sprite= FindObjectOfType<Sprite_Controller>();
        sprite.Dust += CreateDust;
        sprite.DustPos += ChangingPlaceOfEffect;
        force = FindObjectOfType<ForceJump>();
        force.Dust += CreateDust;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") && player.controller.collisions.below && !player.HaveObjToThrow)

        {
            dust.Play();
        }
    }
    void CreateDust()
    {
        dust.Play();
    }

    void ChangingPlaceOfEffect()
    {
        float yPos;
        if (transform.localPosition.y == -0.5f)
        {
             yPos = Mathf.Sign(transform.localPosition.y) * 0.25f;
        }
        else  yPos = Mathf.Sign(transform.localPosition.y) * 0.5f;

        gameObject.transform.localPosition = new Vector3(transform.localPosition.x, yPos, transform.localPosition.z);
    }
}
