using UnityEngine;
using UnityEngine.InputSystem;

public class TestingPlayerPlaceholder : MonoBehaviour, IDamageable
{
    public EntityType EntityType => EntityType.Player;
    [SerializeField] float maxHp;
    private float currentHp;
    private Camera mainCamera;
    [SerializeField] GameObject projectilePrefab;
    private Rigidbody2D rigidBody2D;

    protected SpriteRenderer spriteRender;
    protected Vector2 spriteSize;
    [SerializeField] GameObject damageTextPrefab;
    private void Start()
    {
        currentHp = maxHp;
        mainCamera = Camera.main;
        spriteRender = GetComponent<SpriteRenderer>();
        spriteSize = spriteRender.bounds.size / 2;
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Debug.Log("Shooting!");
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, mainCamera.nearClipPlane));
        Debug.Log(worldPosition);
        Projectile projectileInstance = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<Projectile>();
        projectileInstance.Initialize(worldPosition, true, true, 15f, Random.Range(1,4), 10f, 10f);
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        DamageText damageText = Instantiate(damageTextPrefab, GetRandomSpritePosition(), Quaternion.identity).GetComponent<DamageText>();
        damageText.SetText(damage, DamageTextColors.EnemyDamaged);
        if (currentHp <= 0)
        {
            Die();
        }
    }
    public void TakeDamageWithKnockBack(float damage, Vector2 knockbackDirection, float knockbackForce)
    {
        Debug.Log($"Taking: {damage} damage! Ouch!");
        currentHp -= damage;
        DamageText damageText = Instantiate(damageTextPrefab, GetRandomSpritePosition(), Quaternion.identity).GetComponent<DamageText>();
        damageText.SetText(damage, DamageTextColors.EnemyDamaged);
        if (currentHp <= 0)
        {
            Die();
        }
        else
        {
            Vector2 knockbackVector = knockbackDirection.normalized * knockbackForce;
            rigidBody2D.AddForce(knockbackVector, ForceMode2D.Impulse);
        }
    }
    private Vector3 GetRandomSpritePosition()
    {
        float randomOffsetX = Random.Range(-spriteSize.x / 1f, spriteSize.x / 1f);
        float randomOffsetY = Random.Range(0, spriteSize.y * 1.2f);
        return transform.position + new Vector3(randomOffsetX, randomOffsetY, 0);
    }
    private void Die()
    {
        Destroy(gameObject);
    }

    public void Stun(float stun)
    {
        
    }
}
