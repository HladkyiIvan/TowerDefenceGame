using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public Bullet BulletPrefab;
    private List<Health> EnemiesInRange;
    public float ShootingDelay = 0.8f;
    
    private int TargetIndex;
    private float NextShotTime = 0f;

    private void Awake()
    {
        EnemiesInRange = new List<Health>();
    }

    void Update()
    {
        Shoot(); 
    }

    private void Shoot()
    {
        if (EnemiesInRange.Count != 0)
        {
            TargetIndex = 0;

            if (Time.time > NextShotTime)
            {
                if (EnemiesInRange[TargetIndex] != null)
                {
                    Bullet bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity, null);
                    bullet.Target = EnemiesInRange[TargetIndex];
                    NextShotTime = Time.time + ShootingDelay;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health enemy = collision.GetComponent<Health>();
        if (enemy != null)
        {
            EnemiesInRange.Add(enemy);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Health enemy = collision.GetComponent<Health>();
        if (enemy != null)
        {
            EnemiesInRange.Remove(enemy);
        }
    }
}
