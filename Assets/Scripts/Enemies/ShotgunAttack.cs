using System.Collections;
using UnityEngine;

public class ShotgunAttack : BossAttack
{
    [SerializeField] int numberOfShots;
    [SerializeField] int numberOfAttacks;
    [SerializeField] float timeBetweenAttacks;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed;
    [SerializeField] GameObject attackPoint;
    [SerializeField] float spreadAngle;

    private void Start()
    {
        attackDuration = timeBetweenAttacks * numberOfAttacks;
    }
    public override void Attack()
    {

        base.Attack();
        StartCoroutine(ShootProjectiles(numberOfShots));
    }

    IEnumerator ShootProjectiles(int numberOfShots)
    {
                FMODUnity.RuntimeManager.PlayOneShot("event:/MiniBoss_Roar");
        for (int j = 0; j < numberOfAttacks; j++)
        {
            Vector2 directionToPlayer = (GetComponent<RangedBoss>().playerObj.transform.position - attackPoint.transform.position).normalized;

            float angleStep = spreadAngle / (numberOfShots - 1);
            float startingAngle = -spreadAngle / 2;

            Debug.Log(directionToPlayer);

            for (int i = 0; i < numberOfShots; i++)
            {
                float angle = startingAngle + (angleStep * i);
                
                Vector2 shootDirection = RotateVector(directionToPlayer, angle);
                Vector2 targetPosition = (Vector2)transform.position + shootDirection * 10f;
                Projectile projectileInstance = Instantiate(projectilePrefab, attackPoint.transform.position, Quaternion.identity).GetComponent<Projectile>();
                projectileInstance.Initialize(targetPosition, false, true, projectileSpeed, damage, 0f, 10f);
            }
            yield return new WaitForSeconds(timeBetweenAttacks);
        }
       
    }

    Vector2 RotateVector(Vector2 vector, float degrees)
    {
        float radians = degrees * Mathf.Deg2Rad;
        float sin = Mathf.Sin(radians);
        float cos = Mathf.Cos(radians);

        float tx = vector.x;
        float ty = vector.y;

        return new Vector2(cos * tx - sin * ty, sin * tx + cos * ty);
    }
}
