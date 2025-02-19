using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] BoxCollider2D mapBounds;
    [SerializeField] LayerMask noSpawnLayer;
    [SerializeField] int maxSpawnAttempts = 30;
    [SerializeField] GameObject playerObj;
    [SerializeField] float minSpawnRadius;
    [SerializeField] float maxSpawnRadius;
    //[SerializeField] GameObject meleeEnemyPrefab;
    //[SerializeField] GameObject rangedEnemyPrefab;
    [SerializeField] float waveCooldown;

    Vector2 GetRandomPointInCircle(Vector2 center, float minRadius, float maxRadius)
    {
        float angle = Random.Range(0f, Mathf.PI * 2f);
        float radius = Random.Range(minRadius, maxRadius);
        Vector2 point = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
        return center + point;
    }
    bool IsValidSpawnPoint(Vector2 point, LayerMask noSpawnLayer, Bounds mapBounds)
    {
        if (!mapBounds.Contains(point))
        {
            return false;
        }
        Collider2D hit = Physics2D.OverlapPoint(point, noSpawnLayer);
        if (hit != null)
        {
            return false;
        }
        return true;
    }

    Vector2 FindValidSpawnPoint(Vector2 playerPosition, float minRadius, float maxRadius, LayerMask noSpawnLayer, Bounds mapBounds)
    {
        for (int i = 0; i < maxSpawnAttempts; i++)
        {
            Vector2 point = GetRandomPointInCircle(playerPosition, minRadius, maxRadius);
            if (IsValidSpawnPoint(point, noSpawnLayer, mapBounds))
            {
                return point;
            }
        }
        return Vector2.zero;
    }
    public void SpawnEnemy(EnemyData enemyData, int difficulty)
    {
         Vector2 potentialSpawnPosition = FindValidSpawnPoint(playerObj.transform.position, minSpawnRadius, maxSpawnRadius, noSpawnLayer, mapBounds.bounds);
         if (potentialSpawnPosition != Vector2.zero)
         {
            Enemy enemyInstance = Instantiate(enemyData.prefab, potentialSpawnPosition, Quaternion.identity).GetComponent<Enemy>();
            enemyInstance.Initialize(playerObj, difficulty);

         }
    }
    public void SpawnEnemyOnPosition(EnemyData enemyData, int difficulty, Vector2 position)
    {
        Enemy enemyInstance = Instantiate(enemyData.prefab, position, Quaternion.identity).GetComponent<Enemy>();
        enemyInstance.Initialize(playerObj, difficulty);
    }
}
