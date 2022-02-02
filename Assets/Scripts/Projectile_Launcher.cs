using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Projectile_Launcher : MonoBehaviour
{
    private Ilauncher launcher;
    public bool justShoot;
    private void Awake()
    {
        launcher = GetComponent<Ilauncher>();
    }
    private void Update()
    {
        if (launcher.GetInput() || justShoot)
        {
            launcher.Launch();
        }
    }
}