using UnityEngine;


public abstract class EnemyData : ScriptableObject
{
    public string enemyName;
    public float maxHp;
    public EnemyType enemyType;
    public float knockbackResistance;
    public float meleeDamage;
    public float meleeDamageCooldown;
    public int experienceReward;
    public GameObject prefab;
}
