using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttack : MonoBehaviour
{
    public int projectilesToSpawn = 4;
    public GameObject projectile;
    public Transform spawn;
    private bool spawned = false;

    void Update()
    {
        if (!spawned)
        {
            spawned = true;
            SpawnProjectiles();
        }
    }

    void SpawnProjectiles()
    {
        float rot = 0f;
        for (int i = 0; i < projectilesToSpawn; i++)
        {
            Quaternion rotation = Quaternion.Euler(0, 0, rot);
            Instantiate(projectile, spawn.position, rotation);
            rot += 360f / projectilesToSpawn;
        }
    }
}
