using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public float speed = 8f;
    public float lifeTime = 5f;
    public int damage = 3;
    public bool colidiu = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            colidiu = true;
            EnemyHP enemy = collision.gameObject.GetComponent<EnemyHP>();
            enemy.TakeDamage(damage);
        }

        if (collision.gameObject.CompareTag("Breakable"))
        {
            if(collision.gameObject.GetComponent<DamageableObject>() != null)
            {
                collision.gameObject.GetComponent<DamageableObject>().SetDamage(damage);
            }
        }
        
        Destroy(gameObject);
    }
}
