using UnityEngine;

public class PlayeProjectileControll : MonoBehaviour
{
    float nextTimeToShoot;
    public Transform spawn;
    public Transform model { get; private set; }
    private PlayerController controller;
    [SerializeField] private ProjectileControllData data;

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
            nextTimeToShoot = Time.time + 1f / data.EffectiveFireRate;
            FireWeapon();
        }
    }

    public void ShootSpecial()
    {
        Instantiate(data.special, transform.position, Quaternion.identity, transform);
        controller.comboSystem.UseSpecial();
    }

    void FireWeapon()
    {        
        for (int i = 0; i < data.projectilesToSpawn; i++)
        {
            GameObject projectile = Instantiate(data.projectile, spawn.transform.position + data.spawnPoint + new Vector3(Random.Range(-data.recoilOffsetX, data.recoilOffsetX), Random.Range(-data.recoilOffsetX, data.recoilOffsetX), 0), spawn.rotation * Quaternion.Euler(0, 0, Random.Range(-data.recoilOffsetY, data.recoilOffsetY)));
                  
            model = projectile.transform;
            Instantiate(data.shootSoundPrefab);
            
            if (controller.isFacingRight)
            {
                if(model.transform.localScale.x < 0)
                {
                    model.transform.localScale = new Vector3(model.transform.localScale.x * -1, model.transform.localScale.y, model.transform.localScale.z);
                }
            }
            else
            {
                if (model.transform.localScale.x > 0)
                {
                    model.transform.localScale = new Vector3(model.transform.localScale.x * -1, model.transform.localScale.y, model.transform.localScale.z);
                }
            }
        }
    }

    public void StopShoot()
    {

    }

    public ProjectileControllData GetProjectileControllData()
    {
        return data;
    }
}
