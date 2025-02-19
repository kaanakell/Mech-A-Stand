using System.Collections;
using UnityEngine;

public class RangedBoss : Enemy
{
    private enum State
    {
        Idling,
        Waiting,
        Chasing,
        Attacking,
        Repositioning,
        
    }

    [SerializeField] State currentState;
    [SerializeField] float movementSpeed;
    [SerializeField] float chaseDistance;
    [SerializeField] float attackCooldown;
    [SerializeField] bool canAttack = true;

    [SerializeField] BossAttack[] attacks;
    [SerializeField] Animator bossAnimator;

    [SerializeField] HealthStatusBar healthBar;

    private PlayerWinManager playerWinManager;
    protected override void Start()
    {
        base.Start();
        healthBar.SetState(currentHp, enemyData.maxHp);
        currentState = State.Idling;
        StartCoroutine(ChangeStateAfterCooldown(State.Waiting,3f));
        playerWinManager = FindAnyObjectByType<PlayerWinManager>();
    }

    protected override void Update()
    {
        base.Update();

        switch (currentState)
        {
            case State.Waiting:
                bossAnimator.SetBool("IsMoving", false);
                 
                    currentState = State.Attacking;
                
                break;
            case State.Chasing:
                bossAnimator.SetBool("IsMoving", true);
                if (GetDistanceToPlayer() <= chaseDistance) 
                {
                    currentState = State.Waiting;
                }
                break;
            case State.Attacking:
                if (canAttack)
                {
                    canAttack = false;
                    UseRandomAttack();
                }             
                break;
            case State.Repositioning:
                break;
            case State.Idling:
                break;
            default:
                break;
        }


        if (GetDistanceToPlayer() > chaseDistance && currentState != State.Chasing && currentState != State.Idling)
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
    IEnumerator ResetAttackCooldown(float extraDelay)
    {
        yield return new WaitForSeconds(attackCooldown + extraDelay);
        canAttack = true;
    }

    public float GetDistanceToPlayer()
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
        rigidBody2D.linearVelocity = direction * movementSpeed;
    }

    private void UseRandomAttack()
    {
        float totalChance = 0f;
        foreach (var attack in attacks)
        {
            totalChance += attack.GetChance();
        }
        float randomValue = Random.Range(0, totalChance);
        float cumulativeChance = 0f;
        foreach (var attack in attacks)
        {
            cumulativeChance += attack.GetChance();
            if (randomValue <= cumulativeChance)
            {
                attack.Attack();
                //currentState = State.Waiting;
                //canAttack = false;
                StartCoroutine(ResetAttackCooldown(attack.GetAttackDuration()));
                StartCoroutine(ChangeStateAfterCooldown(State.Waiting, attack.GetAttackDuration()));
                return;
            }
        }
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        healthBar.SetState(currentHp, enemyData.maxHp);
    }
    public override void TakeDamageWithKnockBack(float damage, Vector2 knockbackDirection, float knockbackForce)
    {
        base.TakeDamageWithKnockBack(damage, knockbackDirection, knockbackForce);
        healthBar.SetState(currentHp, enemyData.maxHp);
    }
    IEnumerator ChangeStateAfterCooldown(State state, float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        currentState = state;
    }
    protected override void Die()
    {
        playerWinManager.Win("Hangar");
        base.Die();
    }
}
