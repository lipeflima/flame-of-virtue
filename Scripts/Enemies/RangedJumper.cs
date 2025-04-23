using System.Collections;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    // Referência ao jogador
    public Transform player;

    // Parâmetros da visão
    public float visionAngle = 60f;
    public float visionDistance = 8f;

    // Parâmetros do ataque
    public GameObject projectilePrefab;
    public float attackRange = 6f;
    public float attackCooldown = 2f;

    // Parâmetros de rotação aleatória
    public float rotationInterval = 3f;
    public float rotationSpeed = 100f;
    private bool lockingTarget = false;

    // Fuga (salto)
    public float safeDistance = 2.5f;
    public float jumpDistance = 5f;
    public float jumpChargeTime = 2f;

    private float nextAttackTime = 0f;
    private float nextRotationTime = 0f;
    private bool isChargingJump = false;

    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;

        nextRotationTime = Time.time + rotationInterval;
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        IsPlayerInSight();

        // Se jogador está dentro do cone de visão
        if (lockingTarget)
        {
            lockingTarget = true;
            // Checar distância para iniciar pulo
            if (distanceToPlayer < safeDistance && !isChargingJump)
            {
                StartCoroutine(JumpAway());
                return;
            }
            // Se estiver dentro da distância de ataque
            else if (distanceToPlayer <= attackRange && Time.time >= nextAttackTime)
            {
                Attack();
            }
            else
            {
                // Aproxima-se do jogador
                MoveTowardsPlayer();
            }
        }
        else
        {
            // Rotaciona observando o ambiente
            RotateIdle();
        }
    }

    // Movimento em direção ao jogador
    void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position += (Vector3)direction * Time.deltaTime * 2f;
    }

    // Ataque à distância
    void Attack()
    {
        nextAttackTime = Time.time + attackCooldown;

        Vector2 directionToPlayer = (player.position - transform.position).normalized;

        // Rotaciona o inimigo para "olhar" na direção do jogador
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // Instancia o projetil apontando na direção correta
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.GetComponent<SimpleProjectile>().Initialize(directionToPlayer);
    }

    // Verifica se o jogador está dentro do cone de visão
    bool IsPlayerInSight()
    {
        Vector2 directionToPlayer = player.position - transform.position;
        float angle = Vector2.Angle(transform.up, directionToPlayer);
        return angle < visionAngle / 2f && directionToPlayer.magnitude <= visionDistance;
    }

    // Rotação aleatória quando o jogador não é visto
    void RotateIdle()
    {
        if (Time.time >= nextRotationTime)
        {
            float randomAngle = Random.Range(0f, 360f);
            StartCoroutine(SmoothRotateTo(randomAngle));
            nextRotationTime = Time.time + rotationInterval;
        }
    }

    // Rotação suave para um ângulo específico
    IEnumerator SmoothRotateTo(float targetAngle)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(0, 0, targetAngle);
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime * (rotationSpeed / 100f);
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);
            yield return null;
        }
    }

    // Salta para longe do jogador após carregar
    private IEnumerator JumpAway()
    {
        isChargingJump = true;

        // Espera o tempo de preparação
        yield return new WaitForSeconds(jumpChargeTime);

        Vector2 jumpTarget = transform.position;
        int attempts = 10;

        while (attempts > 0)
        {
            // Gera direção aleatória em círculo
            Vector2 randomDir = Random.insideUnitCircle.normalized;

            // Calcula ponto candidato
            Vector2 candidate = (Vector2)transform.position + randomDir * jumpDistance;

            float distanceToPlayer = Vector2.Distance(candidate, player.position);

            // Verifica se está dentro do intervalo desejado
            if (distanceToPlayer <= attackRange && distanceToPlayer >= safeDistance)
            {
                // Verifica se o jogador continua visível a partir do ponto candidato
                Vector2 toPlayer = (Vector2)player.position - candidate;
                float angleToPlayer = Vector2.Angle(randomDir, toPlayer);

                if (angleToPlayer < visionAngle / 2f)
                {
                    jumpTarget = candidate;
                    break;
                }
            }

            attempts--;
        }

        // Move suavemente até o ponto de salto
        float elapsed = 0f;
        Vector2 start = transform.position;

        while (elapsed < 0.2f)
        {
            transform.position = Vector2.Lerp(start, jumpTarget, elapsed / 0.2f);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = jumpTarget;

        isChargingJump = false;
    }

    // Gizmo do cone de visão no editor
    void OnDrawGizmosSelected()
    {
        if (player == null) return;

        Gizmos.color = Color.yellow;
        Vector3 rightLimit = Quaternion.Euler(0, 0, visionAngle / 2) * transform.up * visionDistance;
        Vector3 leftLimit = Quaternion.Euler(0, 0, -visionAngle / 2) * transform.up * visionDistance;

        Gizmos.DrawRay(transform.position, rightLimit);
        Gizmos.DrawRay(transform.position, leftLimit);
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
