using UnityEngine;
using static MagicStats;

public class SpecialFire : MonoBehaviour
{
    [Header("Configurações")]
    public float speed = 5f;
    public float lifetime = 3f;
    public int damage = 10;
    private bool colidiu = false;
    public GameObject explosionEffect;

    private Vector2 moveDirection;

    public void Initialize(Vector2 direction)
    {
        moveDirection = direction.normalized;

        // Rotaciona para apontar na direção
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifique aqui os tipos de colisão relevantes
        if (other.CompareTag("Enemy") || other.CompareTag("Ground"))
        {
            colidiu = true;
            EnemyHP enemy = other.gameObject.GetComponent<EnemyHP>();
            enemy.TakeDamage(MagicStatApplier.Instance.currentStatsPerMagic[MagicType.SpecialFire].EffectiveDamage);
            Explode();
        }
    }

    private void Explode()
    {
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
