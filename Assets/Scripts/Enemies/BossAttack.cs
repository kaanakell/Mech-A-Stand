using UnityEngine;

public abstract class BossAttack : MonoBehaviour
{
    [SerializeField] protected float damage;
    [SerializeField] protected float chance;
    [SerializeField] protected float attackDuration;
    public virtual void Attack()
    {

    }

    public float GetChance()
    {
        return chance;
    }

    public float GetAttackDuration()
    {
        return attackDuration;
    }
}
