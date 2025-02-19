using System;
using System.Collections;
using UnityEngine;

public class PlayerWhipTypeWeapon : WeaponBase
{
    [SerializeField] GameObject leftWhipObject;
    [SerializeField] GameObject rightWhipObject;

    PlayerMovement playerMove;
    [SerializeField] Vector2 attackSize = new Vector2(4f, 2f);

    private void Awake()
    {
        playerMove = GetComponentInParent<PlayerMovement>();
    }


    public override void Attack()
    {
        StartCoroutine(AttackProcess());
    }

    IEnumerator AttackProcess()
    {
        for (int i = 0; i < weaponStats.numberOfAttacks; i++)
        {
            if (playerMove.lastHorizontalDeCoupledVector > 0)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/Blade_Swing");
                rightWhipObject.SetActive(true);
                Collider2D[] colliders = Physics2D.OverlapBoxAll(rightWhipObject.transform.position, attackSize, 0f);
                ApplyDamage(colliders);
            }
            else
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/Blade_Swing");
                leftWhipObject.SetActive(true);
                Collider2D[] colliders = Physics2D.OverlapBoxAll(leftWhipObject.transform.position, attackSize, 0f);
                ApplyDamage(colliders);
            }
            yield return new WaitForSeconds(0.3f);
        }
    }
}
