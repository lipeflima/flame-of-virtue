using UnityEngine;

[CreateAssetMenu(fileName = "newMagicControllData", menuName = "DataMagicControllData/Base Data")]

public class MagicControllData : ScriptableObject
{
    public float fireRate = 1;
    public float projectilesToSpawn = 1;
    public float recoilOffsetX = 1;
    public float recoilOffsetY = 1;
    public float recoilFactor = 50;
    public Vector3 spawnPoint;
    public GameObject soulFireProjectile;
    public GameObject specialFireProjectile;

    public GameObject shootSoundPrefab;

    [Header("Modificadores dinâmicos")]
    public float fireRateMultiplier = 1f;

    public float EffectiveFireRate => fireRate * fireRateMultiplier;
}
