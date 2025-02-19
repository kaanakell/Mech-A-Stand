using UnityEngine;

[CreateAssetMenu(fileName = "RangedEnemy", menuName = "Enemies/RangedEnemy")]
public class RangedEnemyData : EnemyData
{
    public float rangedDamage;
    public float attackCooldown;
    public float attackDistance;
    public float movementSpeed;
}
