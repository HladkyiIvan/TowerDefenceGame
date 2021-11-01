using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Events 
{
    public static event Action<TowerData> OnTowerSelected;
    public static void SelectTower(TowerData data) => OnTowerSelected?.Invoke(data);

    public static event Action<int> OnEnemyKilled;
    public static void RegisterEnemyKill(int reward) => OnEnemyKilled?.Invoke(reward);

    public static event Action<int> OnTowerPurchased;
    public static void PayForTower(int cost) => OnTowerPurchased?.Invoke(cost);

    public static event Action<int> OnBaseDamageReceived;
    public static void RegisterBaseDamage(int damage) => OnBaseDamageReceived?.Invoke(damage);

    public static event Action<List<WaveData>> OnLevelStarted;
    public static void InitializeWaves(List<WaveData> Waves) => OnLevelStarted?.Invoke(Waves);
}
