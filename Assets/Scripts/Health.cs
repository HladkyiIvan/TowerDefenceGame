using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int HP;
    public int Reward;
    public int Damage;

    public void InflictDamage(int dmg)
    {
        HP -= dmg;
        
        if (HP <= 0)
        {
            Events.RegisterEnemyKill(Reward);
            Destroy(gameObject);
        }
    }
}
