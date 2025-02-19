using System;
using System.Collections.Generic;
using UnityEngine;


public enum StageEventType
{
    SpawnEnemy,
    SpawnEnemyBoss,
    SpawnObject,
    WinStage,
    AnnounceWave,
}


[Serializable]
public class StageEvent
{
    public StageEventType eventType;
    public float time;
    public string message;
    public EnemyData enemyToSpawn;
    public GameObject objectToSpawn;
    public int count;
    public int difficulty;
}


[CreateAssetMenu]
public class StageData : ScriptableObject
{
    public List<StageEvent> stageEvents;
    public string stageID;
    public List<int> stageCompletionToUnlock;
}
