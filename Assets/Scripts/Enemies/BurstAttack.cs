using System.Collections;
using UnityEngine;

public class BurstAttack : BossAttack
{
    [SerializeField] int numberOfShots;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float timeBetweenShots;
    [SerializeField] float projectileSpeed;
    [SerializeField] GameObject attackPoint;
    public override void Attack()
    {
        base.Attack();
        StartCoroutine(ShootProjectiles(numberOfShots));
    }

    IEnumerator ShootProjectiles(int numberOfShots)
    {
                FMODUnity.RuntimeManager.PlayOneShot("event:/miniBoss_ProjectileSHOOT");
        for (int i = 0; i < numberOfShots; i++)
        {
            Projectile projectileInstance = Instantiate(projectilePrefab, attackPoint.transform.position, Quaternion.identity).GetComponent<Projectile>();
            projectileInstance.Initialize(GetComponent<RangedBoss>().playerObj.transform.position, false, true, projectileSpeed, damage, 0f, 10f);
            yield return new WaitForSeconds(timeBetweenShots);
        }
    }
}
