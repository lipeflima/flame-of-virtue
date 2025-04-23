using System.Collections;
using UnityEngine;

public class Spectrum : MonoBehaviour
{
    public float BaseSpeed = 2f;
    private float MoveSpeed;
    private Transform Target;
    private int damage = 1;
    private EnergySystem energia;
    private bool colidido = false;
    public float cooldown = 2f; // Tempo entre danos
    public float nextDamageTime = 0f;

    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        MoveSpeed = BaseSpeed + Random.Range(-0.5f, 0.5f);
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, Target.position, MoveSpeed * Time.deltaTime);   

        if (colidido && Time.time > nextDamageTime)
        {
            energia.DecreaseEnergy(damage);
            nextDamageTime = Time.time + cooldown;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            colidido = true;
            energia = other.GetComponent<EnergySystem>();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            nextDamageTime = 0f;
            colidido = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack"))
        {
            EnemyHP enemy = GetComponent<EnemyHP>();
            enemy.TakeDamage(collision.gameObject.GetComponent<PlayerProjectile>().damage);
        }
    }
}
