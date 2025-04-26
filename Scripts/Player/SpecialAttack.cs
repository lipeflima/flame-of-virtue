using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttack : MonoBehaviour
{
    public float projectilesToSpawn;
    public GameObject projectile;
    public Transform spawn;
    bool spawned;
    float rot;

    void Update()
    {
        if(!spawned)
        {
            spawned = true;
            SpawnProjectiles();
        }
    }

    void SpawnProjectiles()
    {
        for(int i = 0; i < projectilesToSpawn; i++)
        {            
            Instantiate(projectile, spawn.position, spawn.rotation);
            spawn.rotation = Quaternion.Euler(0, 0, rot);
            rot += 90;
        }
    }
}
