using UnityEngine;

public class PlayerWeaponControll : MonoBehaviour
{
    private bool shooting;
    float nextTimeToShoot;
    Vector3 shootDirection;
    public Transform spawn;
    public Transform model { get; private set; }
    private PlayerController controller;
    [SerializeField] private WeaponData wData;

    void Start()
    {
        controller = PlayerController.Instance;
    }

    public void Shoot()
    {
        StartShooting();
    }

    private void StartShooting()
    {
        if (Time.time >= nextTimeToShoot)
        {
            nextTimeToShoot = Time.time + 1f / wData.fireRate;
            shooting = true;

            FireWeapon();
        }
    }

    public void ShootSpecial()
    {
        Instantiate(wData.special, transform.position, Quaternion.identity, transform);
        controller.comboSystem.UseSpecial();
    }

    void FireWeapon()
    {        
        for (int i = 0; i < wData.projectilesToSpawn; i++)
        {
            GameObject projectile = Instantiate(wData.projectile, spawn.transform.position + wData.spawnPoint + new Vector3(Random.Range(-wData.recoilOffsetX, wData.recoilOffsetX), Random.Range(-wData.recoilOffsetX, wData.recoilOffsetX), 0), spawn.rotation * Quaternion.Euler(0, 0, Random.Range(-wData.recoilOffsetY, wData.recoilOffsetY)));
                  
            model = projectile.transform;
            Instantiate(wData.shootSoundPrefab);
            
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
        }
    }

    public void StopShoot()
    {
        shooting = false;
    }

    public WeaponData GetWeaponData()
    {
        return wData;
    }
}
