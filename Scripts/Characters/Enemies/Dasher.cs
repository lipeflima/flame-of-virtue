using UnityEngine;
using UnityEngine.AI;

public class Dasher : MonoBehaviour
{
    public float activationRange = 6f;            // Raio de ativação do inimigo
    public float attackRange = 2f;                // Distância mínima para iniciar o dash
    public float chaseSpeed = 2f;                 // Velocidade ao seguir o jogador
    public float dashSpeed = 8f;                  // Velocidade do dash
    public float dashDuration = 0.3f;             // Duração do dash
    public float dashCooldown = 2f;               // Tempo de recarga entre dashes

    private Transform player;
    private Vector2 dashDirection;
    private bool isDashing = false;
    private float dashTimer = 0f;
    private float cooldownTimer = 0f;
    public Animator anim;
    public Transform model; // modelo visual

    private enum State { Idle, Chasing, Dashing }
    private State currentState = State.Idle;

    private int damage = 1;
    private EnergySystem energia;
    private bool colidido = false;
    public float cooldown = 2f; // Tempo entre danos
    public float nextDamageTime = 0f;

    [SerializeField] private SpriteRenderer spriteRenderer;
    private Color originalColor;
    [SerializeField] private Color rageColor = new Color(1f, 0.3f, 0.3f);
    private NavMeshAgent agent;
    

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        originalColor = spriteRenderer.color;

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        LookAtPlayer();

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (colidido && Time.time > nextDamageTime)
        {
            energia.DecreaseEnergy(damage);
            nextDamageTime = Time.time + cooldown;
        }

        switch (currentState)
        {
            case State.Idle:
                if (distanceToPlayer <= activationRange)
                {
                    currentState = State.Chasing;
                }
                break;

            case State.Chasing:
                if (distanceToPlayer > activationRange)
                {
                    currentState = State.Idle;
                    return;
                }

                // Se estiver em recarga, apenas persegue
                if (cooldownTimer > 0f)
                {
                    cooldownTimer -= Time.deltaTime;
                    // MoveTowards(player.position, chaseSpeed);
                }
                else if (distanceToPlayer <= attackRange)
                {
                    // Pega a posição do jogador e inicia dash
                    dashDirection = (player.position - transform.position).normalized;
                    isDashing = true;
                    dashTimer = dashDuration;
                    currentState = State.Dashing;
                }
                else
                {
                    MoveTowards(player.position, chaseSpeed);
                }
                break;

            case State.Dashing:
                if (dashTimer > 0f)
                {
                    agent.isStopped = true; // Desativa pathfinding
                    transform.position += (Vector3)(dashDirection * dashSpeed * Time.deltaTime);
                    dashTimer -= Time.deltaTime;
                    spriteRenderer.color = rageColor;
                    anim.SetBool("Dashing", true);
                }
                else
                {
                    isDashing = false;
                    cooldownTimer = dashCooldown;
                    currentState = State.Chasing;
                    dashTimer -= Time.deltaTime;
                    spriteRenderer.color = originalColor;
                    anim.SetBool("Dashing", false);
                }
                break;
        }
    }

    void LookAtPlayer()
    {
        if (model != null)
        {
            Vector3 scale = model.localScale;
            scale.x = player.position.x < transform.position.x ? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);
            model.localScale = scale;
        }

        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = player.position.x < transform.position.x;
        }
    }

    // Movimento simples com MoveTowards
    void MoveTowards(Vector2 target, float speed)
    {
        agent.isStopped = false;
        agent.speed = speed;
        agent.SetDestination(target);
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
            // enemy.TakeDamage(collision.gameObject.GetComponent<PlayerProjectile>().damage);
        }
    }
}
