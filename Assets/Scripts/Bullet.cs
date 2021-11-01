using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int Damage;
    public int Speed;
    public Health Target;
    public GameObject Burst;

    private List<Health> EnemiesInRange;
    private int RotationSpeed = 360;

    // Start is called before the first frame update
    void Awake()
    {
        EnemiesInRange = new List<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Target == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, Time.deltaTime * Speed);
        //transform.rotation = new Quaternion(0, 0, transform.rotation.z + RotationSpeed * Time.deltaTime, 0);

        if (Vector3.SqrMagnitude(transform.position - Target.transform.position) < float.Epsilon)
        {
            if (gameObject.tag == "Explosive")
            {
                for (int i = 0; i < EnemiesInRange.Count; i++)
                {
                    EnemiesInRange[i].InflictDamage(Damage);
                    i--;
                }
                Instantiate(Burst, transform.position, transform.rotation, null);
            }
            else
            {
                Target.InflictDamage(Damage);
            }
            Destroy(gameObject);
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
