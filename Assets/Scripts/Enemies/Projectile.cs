using UnityEngine;

public class Projectile : MonoBehaviour
{
    private bool isPlayerCreated;
    private bool shouldDestroyOnContact;
    private float speed;
    private float damage;
    private float knockbackForce;
    private Vector2 direction;
    public virtual void Initialize(Vector2 targetPosition, bool isPlayerCreated, bool shouldDestroyOnContact, float speed, float damage, float knockbackForce, float lifetimeDuration)
    {
        this.isPlayerCreated = isPlayerCreated;
        this.shouldDestroyOnContact = shouldDestroyOnContact;
        this.speed = speed;
        this.damage = damage;
        this.knockbackForce = knockbackForce;
        Destroy(gameObject, lifetimeDuration);
        direction = (targetPosition - (Vector2)transform.position).normalized;
        GetComponent<Rigidbody2D>().linearVelocity = direction * speed;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (isPlayerCreated && collision.CompareTag("Player"))
        {
            return;
        }
        if (!isPlayerCreated && collision.CompareTag("Enemy"))
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Weapons_NoAmmo");
            return;
        }
        IDamageable damageable;
        damageable = collision.GetComponent<IDamageable>();

        if (damageable != null && knockbackForce == 0f)
        {
            damageable.TakeDamage(damage);
            if (shouldDestroyOnContact)
            {
                Destroy(gameObject);
            }
        }
        else if (damageable != null && knockbackForce > 0f)
        {
            damageable.TakeDamageWithKnockBack(damage, direction.normalized, knockbackForce);
            if (shouldDestroyOnContact)
            {
                Destroy(gameObject);
            }
        } 
    }
}
