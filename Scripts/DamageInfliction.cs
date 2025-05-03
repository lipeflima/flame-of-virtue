using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageInfliction : MonoBehaviour
{
    public int damage = 3;
    public bool colidiu = false;
    public bool destroy;
    public GameObject hitEffect;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !colidiu)
        {
            colidiu = true;
            EnemyHP enemy = collision.gameObject.GetComponent<EnemyHP>();
            enemy.TakeDamage(damage);
        }

        if (collision.gameObject.CompareTag("Breakable") && !colidiu)
        {
            colidiu = true;
            if(collision.gameObject.GetComponent<DamageableObject>() != null)
            {
                collision.gameObject.GetComponent<DamageableObject>().SetDamage(damage);
            }
        }        
        
        SpawnEffects();
        if(destroy) Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !colidiu)
        {
            colidiu = true;
            EnemyHP enemy = collision.gameObject.GetComponent<EnemyHP>();
            enemy.TakeDamage(damage);
        }

        if (collision.gameObject.CompareTag("Breakable") && !colidiu)
        {
            colidiu = true;
            if(collision.gameObject.GetComponent<DamageableObject>() != null)
            {
                collision.gameObject.GetComponent<DamageableObject>().SetDamage(damage);
            }
        }        
        
        SpawnEffects();
        if(destroy) Destroy(gameObject);
    }

    void SpawnEffects()
    {
        if(hitEffect != null) Instantiate(hitEffect, transform.position, transform.rotation);
    }
}
