using UnityEngine;

public class RangedJumper : MonoBehaviour
{
    [Header("Referências")]
    public Transform player;
    public GameObject projectilePrefab;

    [Header("Parâmetros de Ataque")]
    public float attackRange = 6f;
    public float attackCooldown = 2f;
    private float lastAttackTime;
    private float viewRange = 20f;
    private float chaseSpeed = 3f;

    [Header("Distância Crítica para Pulo")]
    public float dangerRange = 2f;
    public float safeDistanceMin = 3f;
    public float safeDistanceMax = 5f;
    public float jumpDelay = 2f;
    public float jumpSpeed = 10f;

    private bool isPreparingJump = false;
    private bool isJumping = false;
    private Vector2 jumpTarget;

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        // Se estiver muito perto, iniciar preparação para pulo
        if (distance <= dangerRange && !isPreparingJump && !isJumping)
        {
            StartCoroutine(PrepareJump());
            return;
        }

        // Se estiver em alcance de ataque e não está pulando, atira
        if (distance <= attackRange && !isPreparingJump && !isJumping)
        {
            TryAttack();
        }

        if (distance < viewRange && distance > attackRange)
        {
            MoveTowards(player.position, chaseSpeed);
        }

        // Movimento do pulo
        if (isJumping)
        {
            transform.position = Vector2.MoveTowards(transform.position, jumpTarget, jumpSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, jumpTarget) < 0.1f)
            {
                isJumping = false;
            }
        }
    }

    

    void TryAttack()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            Vector2 direction = (player.position - transform.position).normalized;

            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.GetComponent<SimpleProjectile>().Initialize(direction);

            lastAttackTime = Time.time;
        }
    }

    // Movimento simples com MoveTowards
    void MoveTowards(Vector2 target, float speed)
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    System.Collections.IEnumerator PrepareJump()
    {
        isPreparingJump = true;

        yield return new WaitForSeconds(jumpDelay);

        // Calcula ponto oposto ao jogador, mas dentro da zona segura
        Vector2 awayFromPlayer = (transform.position - player.position).normalized;
        float randomDistance = Random.Range(safeDistanceMin, safeDistanceMax);
        jumpTarget = (Vector2)transform.position + awayFromPlayer * randomDistance;

        isJumping = true;
        isPreparingJump = false;
    }
}
