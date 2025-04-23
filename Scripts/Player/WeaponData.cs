using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "newWeaponData", menuName = "Data/Weapon Data/Base Data")]

public class WeaponData : ScriptableObject
{
    public float damage = 20f;
    public float shootRate = 1;
    public float projectileSpeed = 150;
    public float projectileforce = 800;
    public float projectilesToSpawn = 1;
    public float projectileDistance = 1;
    public float recoilOffsetX = 1;
    public float recoilOffsetY = 1;
    public float magazineSize = 30;
    public float reloadTime = 1;
    public float recoilFactor = 50f;
    public float coolDownRate = 5;
    public float triggerReleaseTime = 0.25f;
    public Vector3 spawnPoint;
    public Vector3 mfPoint;
    public GameObject projectile;
    public GameObject muzzleFlash;
    public GameObject bulletCapsulePrefab;
    public GameObject shootSoundPrefab;

    public enum WeaponMode
    {
        Manual,
        Automatic,
        BurstFire
    }
    public enum ShootType
    {
        Raycast,
        Instantiation,
        Pooling
    }
}
