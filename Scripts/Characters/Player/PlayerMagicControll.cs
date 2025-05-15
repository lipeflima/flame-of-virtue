using UnityEngine;

public class PlayerMagicControll : MonoBehaviour
{
    float nextTimeToShoot;
    public Transform spawn;
    public Transform model { get; private set; }
    private PlayerController controller;
    [SerializeField] private MagicControllData data;
    private ComboSystem comboSystem;

    void Start()
    {
        controller = PlayerController.Instance;
        comboSystem = GameObject.FindGameObjectWithTag("Player").GetComponent<ComboSystem>();
    }

    public void ShootSoulFire()
    {
        if (Time.time >= nextTimeToShoot)
        {
            nextTimeToShoot = Time.time + 1f / data.EffectiveFireRate;

            for (int i = 0; i < data.projectilesToSpawn; i++)
            {
                GameObject projectile = Instantiate(data.SoulFireProjectile, spawn.transform.position + data.spawnPoint + new Vector3(Random.Range(-data.recoilOffsetX, data.recoilOffsetX), Random.Range(-data.recoilOffsetX, data.recoilOffsetX), 0), spawn.rotation * Quaternion.Euler(0, 0, Random.Range(-data.recoilOffsetY, data.recoilOffsetY)));

                model = projectile.transform;
                Instantiate(data.shootSoundPrefab);

                if (controller.isFacingRight)
                {
                    if (model.transform.localScale.x < 0)
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
    }

    public void ShootSpecialFire()
    {
        if (comboSystem.GetSpecialStatus())
        {
            Vector2[] directions = new Vector2[]
            {
                Vector2.up,
                Vector2.down,
                Vector2.left,
                Vector2.right
            };

            foreach (Vector2 dir in directions)
            {
                SpawnSpecialFireProjectile(dir);
            }

            controller.comboSystem.UseSpecial();
        }
    }

    private void SpawnSpecialFireProjectile(Vector2 direction)
    {
        GameObject projectile = Instantiate(data.specialFireProjectile, spawn.position, Quaternion.identity);
        SpecialFire fireScript = projectile.GetComponent<SpecialFire>();

        if (fireScript != null)
        {
            fireScript.Initialize(direction);
        }
        else
        {
            Debug.LogWarning("Prefab não possui o script SpecialFireProjectile.");
        }
    }

    public MagicControllData GetMagicControllData()
    {
        return data;
    }
}
