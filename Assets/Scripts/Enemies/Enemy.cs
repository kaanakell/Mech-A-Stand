using System.Collections;
using UnityEngine;

public enum EnemyType
{
    MeleeEnemy,
    RangedEnemy
}
public abstract class Enemy : MonoBehaviour, IDamageable
{
    public EntityType EntityType => EntityType.Enemy;
    [SerializeField] protected EnemyData enemyData;
    [SerializeField] protected float currentHp;
    [HideInInspector] public GameObject playerObj;
    protected Rigidbody2D rigidBody2D;
    protected bool isKnockedBack = false;
    protected float knockbackDuration = 0.1f;
    protected float knockbackTimer = 0f;

    private Material material;
    private Color materialTintColor;
    private float tintFadeSpeed;
    private SpriteRenderer spriteRenderer;

    public bool canDealMeleeDamage = true;
    protected Vector2 spriteSize;
    [SerializeField] GameObject damageTextPrefab;
    private Vector3 originalFaceDirection;

    private float maxHp;
    private float meleeDamage;
    private int experienceReward;
    private float knockbackResistance;

    protected bool spawnedBySpawner = false;

    protected float stunned;
    protected virtual void Start()
    {
        if (!spawnedBySpawner)
        {
            maxHp = enemyData.maxHp;
            meleeDamage = enemyData.meleeDamage;
            experienceReward = enemyData.experienceReward;
            knockbackResistance = enemyData.knockbackResistance;
            currentHp = maxHp;
        }
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteSize = spriteRenderer.bounds.size / 2;
        rigidBody2D = GetComponent<Rigidbody2D>();
        
        materialTintColor = new Color(1, 0, 0, 0);
        SetMaterial(spriteRenderer.material);
        SetTintFadeSpeed(5f);
        originalFaceDirection = transform.localScale;


        if (playerObj == null)
        {
            playerObj = GameObject.FindGameObjectWithTag("Player");
        }
    }

    public void Initialize(GameObject player, int difficulty)
    {
        spawnedBySpawner = true;
        playerObj = player;
        AdjustStatsBasedOnDifficulty(difficulty);
    }

    protected virtual void AdjustStatsBasedOnDifficulty(int difficulty)
    {
        meleeDamage = enemyData.meleeDamage * (Mathf.Pow(1.1f, difficulty - 1));
        maxHp = enemyData.maxHp * (Mathf.Pow(1.3f, difficulty - 1));
        currentHp = maxHp;
        experienceReward = (int)(enemyData.experienceReward * (Mathf.Pow(1.2f, difficulty - 1)));
    }

    protected virtual void Update()
    {        
        if (materialTintColor.a > 0)
        {
            materialTintColor.a = Mathf.Clamp01(materialTintColor.a - tintFadeSpeed * Time.deltaTime);
            material.SetColor("_Tint", materialTintColor);
        }

        if (playerObj.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-originalFaceDirection.x, originalFaceDirection.y, originalFaceDirection.z);
        }
        else
        {
            transform.localScale = originalFaceDirection;
        }

        
    }
    public virtual void TakeDamage(float damage)
    {
        Debug.Log($"Taking: {damage} damage! Ouch!");
        currentHp -= damage;

        DamageText damageText = Instantiate(damageTextPrefab, GetRandomSpritePosition(), Quaternion.identity).GetComponent<DamageText>();
        damageText.SetText(damage, DamageTextColors.EnemyDamaged);

        if (currentHp <= 0)
        {
            Die(); // Handle death in a centralized method
        }
    }

    public virtual void TakeDamageWithKnockBack(float damage, Vector2 knockbackDirection, float knockbackForce)
    {
        Debug.Log($"Taking: {damage} damage with knockback!");

        currentHp -= damage;

        DamageText damageText = Instantiate(damageTextPrefab, GetRandomSpritePosition(), Quaternion.identity).GetComponent<DamageText>();
        damageText.SetText(damage, DamageTextColors.EnemyDamaged);

        SetTintColor(new Color(1, 0, 0, 1f));

        if (currentHp <= 0)
        {
            Die(); // Handle death in a centralized method
        }
        else
        {
            ApplyKnockback(knockbackDirection, knockbackForce);
        }
    }

    private void ApplyKnockback(Vector2 knockbackDirection, float knockbackForce)
    {
        float actualKnockbackForce = Mathf.Max(0, knockbackForce - enemyData.knockbackResistance);
        if (actualKnockbackForce > 0)
        {
            Vector2 knockbackVector = knockbackDirection.normalized * actualKnockbackForce;
            isKnockedBack = true;
            rigidBody2D.linearVelocity = Vector2.zero; // Reset velocity before applying knockback
            rigidBody2D.AddForce(knockbackVector, ForceMode2D.Impulse);
            knockbackTimer = knockbackDuration;
        }
    }

    private Vector3 GetRandomSpritePosition()
    {
        float randomOffsetX = Random.Range(-spriteSize.x / 1f, spriteSize.x / 1f);
        float randomOffsetY = Random.Range(0, spriteSize.y * 1.2f);
        return transform.position + new Vector3(randomOffsetX, randomOffsetY, 0);
    }

    protected virtual void Die()
    {
        // Add experience points here
        playerObj.GetComponent<PlayerLevel>().AddExperience(experienceReward);

        // Handle drop logic
        GetComponent<DropOnDestroy>().CheckDrop();
        FMODUnity.RuntimeManager.PlayOneShot("event:/EnemyDeath");


        // Destroy the enemy
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (canDealMeleeDamage)
            {
                collision.GetComponent<IDamageable>().TakeDamage(enemyData.meleeDamage);
                canDealMeleeDamage = false;
                StartCoroutine(ResetMeleeDamageCooldown());
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (canDealMeleeDamage)
            {
                collision.GetComponent<IDamageable>().TakeDamage(enemyData.meleeDamage);
                canDealMeleeDamage = false;
                StartCoroutine(ResetMeleeDamageCooldown());
            }
        }
    }

    IEnumerator ResetMeleeDamageCooldown()
    {
        yield return new WaitForSeconds(enemyData.meleeDamageCooldown);
        canDealMeleeDamage = true;
    }


    public void SetMaterial(Material material)
    {
        this.material = material;
    }
    public void SetTintColor(Color color)
    {
        materialTintColor = color;
        material.SetColor("_Tint", materialTintColor);
    }
    public void SetTintFadeSpeed(float tintFadeSpeed)
    {
        this.tintFadeSpeed = tintFadeSpeed;
    }

    public void Stun(float stun)
    {
        stunned = stun;
    }
}
