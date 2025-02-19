using System;
using UnityEngine;

public class PlasmaArrowProjectile : ProjectileBase
{
    [SerializeField] float lifetime = 2f;

    private void Start()
    {
        
    }

    protected override void HitDetection()
    {
        base.HitDetection();
        //Custom logic
    }
}
