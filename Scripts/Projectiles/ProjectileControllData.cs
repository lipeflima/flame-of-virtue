using UnityEngine;

[CreateAssetMenu(fileName = "newProjectileControllData", menuName = "Data/ProjectileControllData/Base Data")]

public class ProjectileControllData : ScriptableObject
{
    public float fireRate = 1;
    public float projectilesToSpawn = 1;
    public float recoilOffsetX = 1;
    public float recoilOffsetY = 1;
    public float recoilFactor = 50;
    public Vector3 spawnPoint;
    public GameObject projectile;
    public GameObject shootSoundPrefab;
    public GameObject special;

    [Header("Modificadores dinâmicos")]
    public float fireRateMultiplier = 1f;

    public float EffectiveFireRate => fireRate * fireRateMultiplier;
}
