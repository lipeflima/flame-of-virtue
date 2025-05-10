using System.Collections;
using UnityEngine;

public class GuardPatroll : MonoBehaviour
{

    public float baseSpeed = 2f;
    private float moveSpeed;

    public float dashSpeed = 6f;
    public float detectionRange = 6f;
    public float dashCooldown = 2f;
    public float dashDuration = 0.4f;
    public int damage = 1;

    private GameObject player;
    public Transform model; // modelo visual
    private Rigidbody2D rb;
    private bool isDashing = false;

    // Patrulha
    private bool isPatrolling = false;
    private float patrolTimer = 0f;
    private float patrolInterval = 0f;
    private Vector2 patrolDirection;

    [SerializeField] private SpriteRenderer spriteRenderer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = baseSpeed + Random.Range(0f, 0.5f);
        SetNewPatrolState();
    }

    void Update()
    {
        LookAtPlayer();
        
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer < 2)
        {
            rb.velocity = Vector2.zero;
        }

        if (!isDashing && distanceToPlayer <= detectionRange)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            rb.velocity = direction * moveSpeed;
        }
        else if (!isDashing && distanceToPlayer > detectionRange)
        {
            PatrolBehavior();
        }

        AvoidOtherEnemies();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            EnergySystem playerHealth = collision.gameObject.GetComponent<EnergySystem>();
            if (playerHealth != null)
            {
                playerHealth.DecreaseEnergy(damage);
            }
        }
    }

    void LookAtPlayer()
    {
        if (model != null)
        {
            Vector3 scale = model.localScale;
            scale.x = player.transform.position.x < transform.position.x ? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);
            model.localScale = scale;
        }

        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = player.transform.position.x < transform.position.x;
        }
    }

    void AvoidOtherEnemies()
    {
        Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(transform.position, 1f);

        Vector2 separationForce = Vector2.zero;

        foreach (var col in nearbyEnemies)
        {
            if (col.gameObject != this.gameObject && col.CompareTag("Enemy"))
            {
                Vector2 away = transform.position - col.transform.position;
                separationForce += away.normalized / away.magnitude;
            }
        }

        rb.velocity += separationForce * 0.1f;
    }

    // Lógica da patrulha
    void PatrolBehavior()
    {
        patrolTimer -= Time.deltaTime;

        if (patrolTimer <= 0f)
        {
            SetNewPatrolState();
        }

        if (isPatrolling)
        {
            rb.velocity = patrolDirection * moveSpeed * 0.5f;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    // Alterna entre parado e patrulha
    void SetNewPatrolState()
    {
        isPatrolling = !isPatrolling;
        patrolInterval = Random.Range(3f, 5f);
        patrolTimer = patrolInterval;

        if (isPatrolling)
        {
            float angle = Random.Range(0f, 360f);
            patrolDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack"))
        {
            EnemyHP enemy = GetComponent<EnemyHP>();
            // enemy.TakeDamage(collision.gameObject.GetComponent<PlayerProjectile>().damage);
        }
    }
}
