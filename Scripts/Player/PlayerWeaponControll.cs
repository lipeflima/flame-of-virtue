using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponControll : MonoBehaviour
{
    public WeaponType weaponType;
    private bool shooting;
    float nextTimeToShoot, recoverRecoilCounter;
    Vector3 shootDirection;
    public Transform spawn;
    public Transform model { get; private set; }
    private PlayerController controller;
    [SerializeField] private WeaponData wData;

    public enum WeaponType
    {
        Burst,
        Automatic,
        Manual,
    }

    void Start()
    {
        controller = PlayerController.Instance;
    }

    private void Update()
    {
        if (shooting)
        {
            //SetRecoil(true);
        }
        else
        {
            //SetRecoil(false);
        }

        //CheckTriggerRelease();
    }

    public void Shoot()
    {
        StartShooting();
    }

    private void StartShooting()
    {
        if (weaponType == WeaponType.Automatic)
        {
            if (Time.time >= nextTimeToShoot)
            {
                nextTimeToShoot = Time.time + 1f / wData.shootRate;
                shooting = true;

                FireWeapon();
            }
        }
    }

    public void ShootSpecial()
    {
        GameObject special = Instantiate(wData.special, transform.position, Quaternion.identity, transform);
        controller.comboSystem.UseSpecial();
    }

    void FireWeapon()
    {        
        for (int i = 0; i < wData.projectilesToSpawn; i++)
        {
            GameObject projectile = Instantiate(wData.projectile, spawn.transform.position + wData.spawnPoint + new Vector3(Random.Range(-wData.recoilOffsetX, wData.recoilOffsetX), Random.Range(-wData.recoilOffsetX, wData.recoilOffsetX), 0), spawn.rotation * Quaternion.Euler(0, 0, Random.Range(-wData.recoilOffsetY, wData.recoilOffsetY)));
            //GameObject projectile = Instantiate(wData.projectile, spawn.transform.position, spawn.transform.rotation);            
            model = projectile.transform;
            //ammo -= 1;
            //ammoBar.value = ammo;
            
            if (controller.isFacingRight)
            {
                shootDirection = projectile.transform.right;
                
                if(model.transform.localScale.x < 0)
                {
                    model.transform.localScale = new Vector3(model.transform.localScale.x * -1, model.transform.localScale.y, model.transform.localScale.z);
                }
            }
            else
            {
                shootDirection = -projectile.transform.right;

                if (model.transform.localScale.x > 0)
                {
                    model.transform.localScale = new Vector3(model.transform.localScale.x * -1, model.transform.localScale.y, model.transform.localScale.z);
                }
            }

            //projectile.GetComponent<Rigidbody2D>().AddForce(shootDirection * wData.projectileSpeed, ForceMode2D.Impulse);
            //Destroy(projectile, wData.projectileSpeed * wData.projectileDistance * Time.deltaTime);

            if (i == 0)
            {
                if (controller.isFacingRight)
                {
                    SpawnEffects(1);
                }
                else
                {
                    SpawnEffects(-1);
                }
            }
        }
    }

    void SpawnEffects(int dir)
    {
        //GameObject prefab = Instantiate(wData.muzzleFlash, spawn.transform.position + wData.spawnPoint, spawn.rotation);
        //prefab.transform.localScale = new Vector3(prefab.transform.localScale.x * dir, prefab.transform.localScale.y, prefab.transform.localScale.z);
        //objectPool.SpawnFromPool(wData.shootSound, spawn.transform.position, Quaternion.identity);
        //Instantiate(wData.shootSoundPrefab);
        //objectPool.SpawnFromPool(wData.bulletCapsule, spawn.transform.position, Quaternion.identity);
        //GameObject capsule = Instantiate(wData.bulletCapsulePrefab, spawn.transform.position + wData.spawnPoint, spawn.rotation);
        //capsule.GetComponent<ObjectManager>().SetParticles(wData.capsuleIndex);
    }

    public void StopShoot()
    {
        //if(shooting) startTriggerRelease = true;

        shooting = false;
        //SetRecoil(false);
    }

    void SetRecoil(bool status)
    {
        if (status)
            controller.weapon.transform.position = Vector3.Lerp(controller.turret.transform.position, controller.weapon.transform.position + new Vector3(Random.Range(-wData.recoilOffsetX, wData.recoilOffsetX), Random.Range(-wData.recoilOffsetX, wData.recoilOffsetX), 0), wData.shootRate/wData.recoilFactor);
        else
            controller.weapon.transform.position = controller.turret.transform.position;
    }

    public void FlipModel()
    {
        //scale = new Vector3(scale.x * -1, scale.y, scale.z);
    }

    public WeaponData GetWeaponData()
    {
        return wData;
    }
}
