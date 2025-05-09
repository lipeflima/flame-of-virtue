using UnityEngine;
using UnityEngine.AI;

public class Spectrum : MonoBehaviour
{
    public float BaseSpeed = 2f;
    public float MoveSpeed = 3f;
    public int damage = 1;
    public float cooldown = 1f;
    private float nextDamageTime;

    public Transform Target;
    private NavMeshAgent agent;
    private EnergySystem energia;
    private bool colidido;

    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        MoveSpeed = BaseSpeed + Random.Range(-0.5f, 0.5f);
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        if (Target == null) return;

        agent.SetDestination(Target.position);

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
