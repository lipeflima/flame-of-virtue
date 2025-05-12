using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public float speed = 8f;
    public float lifeTime = 5f;
    public int damage = 3;
    public bool colidiu = false;
    public bool canMove, destroy;
    public GameObject hitEffect;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            colidiu = true;
            EnemyHP enemy = collision.gameObject.GetComponent<EnemyHP>();
            enemy.TakeDamage(damage);
            DestroyProjectile();
        }

        if (collision.gameObject.CompareTag("Breakable"))
        {
            if(collision.gameObject.GetComponent<DamageableObject>() != null)
            {
                collision.gameObject.GetComponent<DamageableObject>().SetDamage(damage);
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
