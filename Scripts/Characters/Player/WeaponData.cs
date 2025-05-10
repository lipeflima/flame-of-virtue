using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "newWeaponData", menuName = "Data/Weapon Data/Base Data")]

public class WeaponData : ScriptableObject
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

    public enum ShootType
    {
        Raycast,
        Instantiation,
        Pooling
    }

    [Header("Modificadores dinâmicos")]
    public float fireRateMultiplier = 1f;

    public float EffectiveFireRate => fireRate * fireRateMultiplier;
}
