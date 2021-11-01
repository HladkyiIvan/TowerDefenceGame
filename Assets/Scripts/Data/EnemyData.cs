using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/EnemyData")]
public class EnemyData : ScriptableObject
{
    public int HP;
    public int Reward;
    public int Damage;
    public float MovementSpeed;
    public Sprite Sprite;
    public Health EnemyPrefab;
}
