using UnityEngine;
using static MagicStats;

[RequireComponent(typeof(Rigidbody2D))]
public class SoulFire : MonoBehaviour
{
    private bool colidiu = false;
    public bool destroy;
    public GameObject hitEffect;
    [SerializeField] private float turnSpeed = 200f;
    [SerializeField] private float destroyDistance = 0.3f; // distância mínima para destruir ao chegar no mouse

    private Rigidbody2D rb;
    private Vector2 currentDirection;
    private float timer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timer = MagicStatApplier.Instance.currentStatsPerMagic[MagicType.SoulFire].EffectiveLifetime;
        Vector2 randomDir = Random.insideUnitCircle.normalized;
        currentDirection = randomDir;
    }

    void FixedUpdate()
    {
        timer -= Time.fixedDeltaTime;
        if (timer <= 0f)
        {
            DestroyProjectile();
            return;
        }

        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Vector2.Distance(rb.position, mouseWorldPos) <= destroyDistance)
        {
            DestroyProjectile();
            return;
        }

        Vector2 desiredDirection = (mouseWorldPos - rb.position).normalized;
        currentDirection = Vector2.Lerp(currentDirection, desiredDirection, turnSpeed * Time.fixedDeltaTime).normalized;
        rb.MovePosition(rb.position + currentDirection * MagicStatApplier.Instance.currentStatsPerMagic[MagicType.SoulFire].EffectiveSpeed * Time.fixedDeltaTime);
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            colidiu = true;
            EnemyHP enemy = collision.gameObject.GetComponent<EnemyHP>();
            enemy.TakeDamage(MagicStatApplier.Instance.currentStatsPerMagic[MagicType.SoulFire].EffectiveDamage);
            DestroyProjectile();
        }

        if (collision.gameObject.CompareTag("Breakable"))
        {
            if(collision.gameObject.GetComponent<DamageableObject>() != null)
            {
                collision.gameObject.GetComponent<DamageableObject>().SetDamage(MagicStatApplier.Instance.currentStatsPerMagic[MagicType.SoulFire].EffectiveDamage);
            }

            DestroyProjectile();
        }        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        DestroyProjectile();
    }

    void DestroyProjectile()
    {
        if(hitEffect != null) Instantiate(hitEffect, transform.position, transform.rotation);
        if(destroy) Destroy(gameObject);
    }
}
