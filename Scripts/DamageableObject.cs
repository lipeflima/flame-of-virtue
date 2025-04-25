using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableObject : MonoBehaviour
{
    public float maxHealth;
    float currentHealth;
    public GameObject objectToSpawn;
    void Start()
    {
        currentHealth = maxHealth;
    }
    void Update()
    {
        if(currentHealth <= 0)
        {
            SpawnItem();
        }
    }

    public void SetDamage(float dmg)
    {
        currentHealth -= dmg;
    }

    void SpawnItem()
    {
        Instantiate(objectToSpawn, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
