using System;
using UnityEngine;

public class StageEventManager : MonoBehaviour
{
    [SerializeField] StageData stageData;
    EnemySpawner enemySpawner; //***After this line you need to attach enemy spawner to WORLD game object***
    [SerializeField] GameObject bossSpawnPosition;

    StageTimer stageTimer;
    int eventIndexer;
    PlayerWinManager playerWinManager;
    WaveAnnouncer waveAnnouncer;

    private void Awake()
    {
        stageTimer = GetComponent<StageTimer>();
    }

    void Start()
    {
        playerWinManager = FindAnyObjectByType<PlayerWinManager>();
        waveAnnouncer = FindAnyObjectByType<WaveAnnouncer>();
        enemySpawner = FindAnyObjectByType<EnemySpawner>();
    }

    void Update()
    {
        if (eventIndexer >= stageData.stageEvents.Count)
        {
            return;
        }

        if (stageTimer.time > stageData.stageEvents[eventIndexer].time)
        {
            switch (stageData.stageEvents[eventIndexer].eventType)
            {
                case StageEventType.SpawnEnemy:
                    SpawnEnemy();
                    break;

                case StageEventType.SpawnObject:
                    SpawnObject();
                    break;

                case StageEventType.WinStage:
                    WinStage();
                    break;

                case StageEventType.SpawnEnemyBoss:
                    SpawnEnemyBoss();
                    break;
                case StageEventType.AnnounceWave:
                    waveAnnouncer.DisplayText(stageData.stageEvents[eventIndexer].message);
                    break;
            }

            Debug.Log(stageData.stageEvents[eventIndexer].message);


            
            eventIndexer++;
        }
    }

    private void SpawnEnemyBoss()
    {
        enemySpawner.SpawnEnemyOnPosition(stageData.stageEvents[eventIndexer].enemyToSpawn, stageData.stageEvents[eventIndexer].difficulty, bossSpawnPosition.transform.position);//you can add small bool check for if enemy is boss or not 
    }

    public void WinStage()
    {
        playerWinManager.Win(stageData.stageID);
    }

    private void SpawnObject()
    {
        for (int i = 0; i < stageData.stageEvents[eventIndexer].count; i++)
        {
            Vector3 positionToSpawn = GameManager.instance.playerTransform.position;
            positionToSpawn += UtilityTools.GenerateRandomPositionSquarePattern(new Vector2(5f, 5f));

            SpawnManager.instance.SpawnObject(positionToSpawn, stageData.stageEvents[eventIndexer].objectToSpawn);
        }
    }

    private void SpawnEnemy()
    {
        for (int i = 0; i < stageData.stageEvents[eventIndexer].count; i++)
        {
            enemySpawner.SpawnEnemy(stageData.stageEvents[eventIndexer].enemyToSpawn, stageData.stageEvents[eventIndexer].difficulty);//**Don't forget to make it public also you can delete the timer type of components if you got any in enemy spawner script**
        }
    }
}
