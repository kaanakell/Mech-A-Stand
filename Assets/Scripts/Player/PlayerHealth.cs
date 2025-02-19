using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public EntityType EntityType => EntityType.Player;
    public float maximumHealth = 1000f;
    public float currentHealth = 1000f;

    public float armor;
    public float maxArmor;
    public float armorRegenerationRate;
    public float armorRegenerationCooldown;

    public float hpRegenerationRate;
    public float hpRegenerationCooldown;
    private float lastDamageTime;

    private bool isStunned = false;

    public float damageBonus;
    public HealthStatusBar hpBar;
    public ArmorStatusBar armorBar;

    private bool isDead;
    [SerializeField] DataContainer dataContainer;
    [SerializeField] GameObject damageTextPrefab;
    public bool isInvulnerable { get; private set; }

    private void Start()
    {
        ApplyPersistentUpgrades();
        hpBar.SetState(currentHealth, maximumHealth);
        armorBar.SetState(armor, maxArmor);
    }

    private void Update()
    {
        if (Time.time - lastDamageTime >= hpRegenerationCooldown)
        {
            RegenerateHealth();
        }

        if (Time.time - lastDamageTime >= armorRegenerationCooldown && maxArmor > 0)
        {
            RegenerateArmor();
        }
    }

    private void ApplyPersistentUpgrades()
    {
        int hpUpgradeLevel = dataContainer.GetUpgradeLevel(PlayerPersistentUpgrades.HP);

        maximumHealth += maximumHealth / 10 * hpUpgradeLevel;
        currentHealth = maximumHealth;

        int damageUpgradeLevel = dataContainer.GetUpgradeLevel(PlayerPersistentUpgrades.Damage);
        damageBonus = 1f + 0.1f * damageUpgradeLevel;
    }

    public void TakeDamage(float damage)
    {
        if (isDead || isInvulnerable) return;

        ApplyArmor(ref damage);
        currentHealth -= damage;
        lastDamageTime = Time.time;

        DamageText damageText = Instantiate(damageTextPrefab, transform.position, Quaternion.identity).GetComponent<DamageText>();
        damageText.SetText(damage, DamageTextColors.PlayerDamaged);

        if (currentHealth <= 0)
        {
            GetComponent<PlayerGameOver>().GameOver();
            isDead = true;
            Die();
        }
        hpBar.SetState(currentHealth, maximumHealth);
    }

    public void SetInvulnerability(bool state)
    {
        isInvulnerable = state;
    }

    private void ApplyArmor(ref float damage)
    {
        if (armor > 0)
        {
            float mitigatedDamage = Mathf.Min(armor, damage);
            armor -= mitigatedDamage;
            damage -= mitigatedDamage;
            armorBar.SetState(armor, maxArmor);
        }

        if (damage < 0)
        {
            damage = 0;
        }
    }


    private void RegenerateArmor()
    {
        if (armor < maxArmor)
        {
            armor += armorRegenerationRate * Time.deltaTime;
            if (armor > maxArmor)
            {
                armor = maxArmor;
            }
            armorBar.SetState(armor, maxArmor);
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void RegenerateHealth()
    {
        if (currentHealth < maximumHealth)
        {
            currentHealth += hpRegenerationRate * Time.deltaTime;
            if (currentHealth > maximumHealth)
            {
                currentHealth = maximumHealth;
            }
            hpBar.SetState(currentHealth, maximumHealth);
        }
    }

    public void Heal(float amount)
    {
        if (currentHealth <= 0) return;

        currentHealth += amount;
        if (currentHealth > maximumHealth)
        {
            currentHealth = maximumHealth;
        }

        hpBar.SetState(currentHealth, maximumHealth);
    }

    public void Stun(float stun)
    {
        
    }

    IEnumerator RemoveStun(float duration)
    {
        yield return new WaitForSeconds(duration);
        isStunned = false;
    }

    public bool GetStunStatus()
    {
        return isStunned;
    }

    public void TakeDamageWithKnockBack(float damage, Vector2 knockbackDirection, float knockbackForce)
    {

    }
}
