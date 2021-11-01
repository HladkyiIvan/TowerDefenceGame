using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public Queue<WaveData> Waves;
    public float TimeBetweenWaves = 10f;

    private WaveData CurrentWave;
    private float NextSpawnTime = 3;
    private float NextWaveTime = 0;
    private int SpawnCount;

    private Waypoint waypoint;

    // Start is called before the first frame update
    void Awake()
    {
        waypoint = GetComponent<Waypoint>();
        Events.OnLevelStarted += InitializeWaves;
    }

    private void OnDestroy()
    {
        Events.OnLevelStarted -= InitializeWaves;
    }

    // Update is called once per frame
    void Update()
    {
        if (SpawnCount < CurrentWave.EnemiesInWave)
        {
            if (Time.time > NextSpawnTime)
            {
                SpawnEnemy();
            }
        }
        else
        {
            if (Time.time > NextWaveTime)
            {
                StartNewWave();
            }
        }
    }

    void SpawnEnemy()
    {
        Health enemy = Instantiate(CurrentWave.EnemyData.EnemyPrefab, transform.position, Quaternion.identity, null);
        enemy.HP = CurrentWave.EnemyData.HP;
        enemy.Damage = CurrentWave.EnemyData.Damage;
        enemy.Reward = CurrentWave.EnemyData.Reward;
        enemy.GetComponent<SpriteRenderer>().sprite = CurrentWave.EnemyData.Sprite;

        WaypointFollower enemyFollower = enemy.GetComponent<WaypointFollower>();
        enemyFollower.Speed = CurrentWave.EnemyData.MovementSpeed;
        enemyFollower.Waypoint = waypoint;

        NextSpawnTime = Time.time + CurrentWave.TimeBetweenSpawn;
        SpawnCount++;

        if (SpawnCount >= CurrentWave.EnemiesInWave)
        {
            NextWaveTime = Time.time + TimeBetweenWaves;
        }
    }

    void StartNewWave()
    {
        if (Waves.Count == 0) return;

        CurrentWave = Waves.Dequeue();
        SpawnCount = 0;
        NextSpawnTime = Time.time;
    }

    void InitializeWaves(List<WaveData> NewWaves)
    {
        Waves = new Queue<WaveData>(NewWaves);

        if (Waves.Count == 0) return;

        CurrentWave = Waves.Dequeue();
    }
}
