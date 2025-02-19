using System.Collections;
using UnityEngine;



public class RangedEnemy : Enemy
{
    private enum State
    {
        Waiting,
        Chasing,
        Attacking,
    }

    private RangedEnemyData rangedEnemyData;
    //private Rigidbody2D rigidBody2D;
    [SerializeField] State currentState;
    [SerializeField] GameObject projectilePrefab;
    private bool canAttack = true;
    PlayerHealth playerHealth;

    private float rangedDamage;
    private float attackCooldown;
    private float attackDistance;
    private float movementSpeed;

    protected override void Start()
    {
        if (!spawnedBySpawner)
        {
            rangedEnemyData = enemyData as RangedEnemyData;
            rangedDamage = rangedEnemyData.rangedDamage;
        }

        base.Start();
        
        attackCooldown = rangedEnemyData.attackCooldown;
        attackDistance = rangedEnemyData.attackDistance;
        movementSpeed = rangedEnemyData.movementSpeed;
        playerHealth = playerObj.GetComponent<PlayerHealth>();
        //rigidBody2D = GetComponent<Rigidbody2D>();
        currentState = State.Chasing;
    }
    protected override void AdjustStatsBasedOnDifficulty(int difficulty)
    {
        base.AdjustStatsBasedOnDifficulty(difficulty);
        rangedEnemyData = enemyData as RangedEnemyData;
        rangedDamage = rangedEnemyData.rangedDamage * (Mathf.Pow(1.3f, difficulty - 1));
    }
    protected override void Update()
    {
        base.Update();

        switch (currentState)
        {
            case State.Waiting:
                if (canAttack)
                {
                    canAttack = false;
                    StartCoroutine(ResetAttackCooldown());
                    currentState = State.Attacking;
                }
                break;
            case State.Chasing:
                float distanceToPlayer = Vector2.Distance(playerObj.transform.position, transform.position);

                if (distanceToPlayer <= attackDistance)
                {
                    currentState = State.Waiting;
                    rigidBody2D.linearVelocity = Vector2.zero;
                }
                break;
            case State.Attacking:
                Attack();
                break;
            default:
                break;
        }

        if (GetDistanceToPlayer() > attackDistance && currentState != State.Chasing)
        {
            currentState = State.Chasing;
        }
    }
    private void FixedUpdate()
    {
        if (currentState == State.Chasing)
        {
            ChasePlayer();
        }
    }
    IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private void Attack()
    {
        if (playerHealth.currentHealth > 0)
        {
            Projectile projectileInstance = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<Projectile>();
            projectileInstance.Initialize(playerObj.transform.position, false, true, 7f, rangedDamage, 0f, 10f);
            currentState = State.Waiting;
        }
    }
    private float GetDistanceToPlayer()
    {
        return Vector2.Distance(playerObj.transform.position, transform.position);
    }
    private void ChasePlayer()
    {
        if (isKnockedBack)
        {
            knockbackTimer -= Time.fixedDeltaTime;
            if (knockbackTimer <= 0)
            {
                isKnockedBack = false;
            }
            return;
        }

        Vector2 direction = (playerObj.transform.position - transform.position).normalized;
        /*
        float distanceToPlayer = Vector2.Distance(playerObj.transform.position, transform.position);

        if (distanceToPlayer > rangedEnemyData.attackDistance)
        {
            rigidBody2D.linearVelocity = direction * rangedEnemyData.movementSpeed;
        }
        else
        {
            rigidBody2D.linearVelocity = Vector2.zero;
            currentState = State.Waiting;
        }
        */
        rigidBody2D.linearVelocity = direction * movementSpeed;
    }
}
