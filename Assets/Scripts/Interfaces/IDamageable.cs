using UnityEngine;

public enum EntityType
{
    Enemy,
    Player,
    Chest,
    Other
}

public interface IDamageable
{
    EntityType EntityType { get; }

    void Stun(float stun);
    void TakeDamage(float damage);
    void TakeDamageWithKnockBack(float damage, Vector2 knockbackDirection, float knockbackForce);
}

