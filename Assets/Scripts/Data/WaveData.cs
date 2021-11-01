using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/WaveData")]
[System.Serializable]
public class WaveData : ScriptableObject
{
    public int EnemiesInWave;
    public EnemyData EnemyData;
    public float TimeBetweenSpawn;
}
