using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "newWeaponData", menuName = "Data/Weapon Data/Base Data")]

public class WeaponData : ScriptableObject
{
    public string projectileName;
    public float damage = 20f;
    public float fireRate = 1;
    public float projectileSpeed = 150;
    public float projectilesToSpawn = 1;
    public float projectileDistance = 1;
    public float recoilOffsetX = 1;
    public float recoilOffsetY = 1;
    public float recoilFactor = 50;
    public Vector3 spawnPoint;
    public GameObject projectile;
    public GameObject shootSoundPrefab;
    public GameObject special;

    public enum ShootType
    {
        Raycast,
        Instantiation,
        Pooling
    }

    [Header("Modificadores dinâmicos")]
    public float damageMultiplier = 1f;
    public float speedMultiplier = 1f;
    public float fireRateMultiplier = 1f;
    public float projectileDistanceMultiplier = 1f;

    public float EffectiveDamage => damage * damageMultiplier;
    public float EffectiveSpeed => projectileSpeed * speedMultiplier;
    public float EffectiveFireRate => fireRate * fireRateMultiplier;
    public float EffectiveProjectileDistance => projectileDistance * projectileDistanceMultiplier;
}
