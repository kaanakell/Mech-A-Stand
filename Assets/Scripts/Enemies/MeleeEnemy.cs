using UnityEngine;

public class MeleeEnemy : Enemy
{
    //private Rigidbody2D rigidBody2D;
    private MeleeEnemyData meleeEnemyData;

    private float movementSpeed;

    protected override void Start()
    {
        base.Start();
        //rigidBody2D = GetComponent<Rigidbody2D>();
        if (!spawnedBySpawner)
        {
            meleeEnemyData = enemyData as MeleeEnemyData;
            movementSpeed = meleeEnemyData.movementSpeed;
        }
        
    }

    protected override void AdjustStatsBasedOnDifficulty(int difficulty)
    {
        base.AdjustStatsBasedOnDifficulty(difficulty);
        meleeEnemyData = enemyData as MeleeEnemyData;
        movementSpeed = meleeEnemyData.movementSpeed * (Mathf.Pow(1.1f, difficulty - 1));
    }
    private void FixedUpdate()
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


        float distanceToPlayer = Vector2.Distance(playerObj.transform.position, transform.position);

        if (distanceToPlayer > 0.5f)
        {
            //rigidBody2D.AddForce(direction * chasingEnemyData.movementSpeed);
            if (stunned > 0f)
            {
                rigidBody2D.linearVelocity = Vector2.zero;
                stunned -= Time.deltaTime;

            }
            else
            {
                rigidBody2D.linearVelocity = direction * meleeEnemyData.movementSpeed;
            }
        }
        else
        {
            rigidBody2D.linearVelocity = Vector2.zero;
        }
    }
}
