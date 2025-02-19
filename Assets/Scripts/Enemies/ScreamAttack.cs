using System.Collections;
using UnityEngine;

public class ScreamAttack : BossAttack
{
    [SerializeField] float range;
    [SerializeField] float stunDuration;
    [SerializeField] Animator sonicWaveAnimator;
    [SerializeField] Animator bossAnimator;
    
    public override void Attack()
    {
        base.Attack();
        bossAnimator.SetTrigger("Scream");

    }

    public IEnumerator CreateSonicWave()
    {    
        FMODUnity.RuntimeManager.PlayOneShot("event:/MiniBoss_StunAttack");
        sonicWaveAnimator.SetTrigger("Scream");
        yield return new WaitForSeconds(0.3f);
        if (GetComponent<RangedBoss>().GetDistanceToPlayer() <= range)
        {
            GetComponent<RangedBoss>().playerObj.GetComponent<IDamageable>().Stun(stunDuration);
            GetComponent<RangedBoss>().playerObj.GetComponent<IDamageable>().TakeDamage(damage);
            FMODUnity.RuntimeManager.PlayOneShot("event:/Weapons_EMP_Explode");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
