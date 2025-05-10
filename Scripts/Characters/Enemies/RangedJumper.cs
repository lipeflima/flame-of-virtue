using System.Collections;
using UnityEngine.AI;
using UnityEngine;

public class RangedJumper : MonoBehaviour
{
    [Header("Referências")]
    public Transform player;
    public GameObject projectilePrefab;
    public Transform model;
    public SpriteRenderer spriteRenderer;

    [Header("Parâmetros de Ataque")]
    public float attackRange = 6f;
    public float attackCooldown = 2f;
    private float lastAttackTime;
    public float waitInitialTime = 0.2f;

    [Header("Visão e Movimento")]
    public float viewRange = 20f;
    public float chaseSpeed = 3f;

    [Header("Pulo")]
    public bool canJump = true;
    public float dangerRange = 2f;
    public float safeDistanceMin = 3f;
    public float safeDistanceMax = 5f;
    public float jumpDelay = 0.5f;
    public float jumpSpeed = 10f;
    public float maxJumpDuration = 1.5f;
    public LayerMask obstacleLayer;

    private bool isPreparingJump = false;
    private bool isJumping = false;
    private Vector3 jumpTarget;
    private float jumpDurationTimer = 0f;

    public Animator anim;
    private PlayerController controller;
    private NavMeshAgent agent;

    void Start()
    {
        SetAnimations("Idle");
        controller = PlayerController.Instance;
        player = controller.transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false; // fundamental para 2D
        agent.speed = chaseSpeed;
    }

    void Update()
    {
        if (isJumping)
        {
            jumpDurationTimer -= Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, jumpTarget, jumpSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, jumpTarget) < 0.1f || jumpDurationTimer <= 0f)
            {
                isJumping = false;
                SetAnimations("Idle");
            }
            return;
        }

        if (player == null) return;

        LookAtPlayer();
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= dangerRange && !isPreparingJump)
        {
            if (canJump)
            {
                StartCoroutine(PrepareJump());
                return;
            }
        }

        if (distance <= attackRange && !isPreparingJump)
        {
            TryAttack();
        }

        if (distance < viewRange && distance > attackRange)
        {
            MoveTowards(player.position, chaseSpeed);
        }
    }

    IEnumerator PrepareJump()
    {
        isPreparingJump = true;
        yield return new WaitForSeconds(jumpDelay);

        int attempts = 10;
        bool found = false;

        for (int i = 0; i < attempts; i++)
        {
            Vector2 awayDir = (transform.position - player.position).normalized;
            Vector2 randomDir = Quaternion.Euler(0, 0, Random.Range(-90f, 90f)) * awayDir;
            float distance = Random.Range(safeDistanceMin, safeDistanceMax);
            Vector3 tentativePoint = transform.position + (Vector3)(randomDir.normalized * distance);

            if (NavMesh.SamplePosition(tentativePoint, out NavMeshHit hit, 1f, NavMesh.AllAreas))
            {
                float distToTarget = Vector3.Distance(transform.position, hit.position);

                RaycastHit2D hitObstacle = Physics2D.Raycast(
                    transform.position,
                    (hit.position - transform.position).normalized,
                    distToTarget,
                    obstacleLayer
                );

                Debug.DrawRay(transform.position, (hit.position - transform.position).normalized * distToTarget, Color.red, 1f);

                if (hitObstacle.collider == null && distToTarget <= safeDistanceMax)
                {
                    jumpTarget = hit.position;
                    isJumping = true;
                    jumpDurationTimer = maxJumpDuration;
                    SetAnimations("Jump");
                    found = true;
                    break;
                }
            }

            yield return null;
        }

        if (!found)
        {
            Debug.Log("Pulo cancelado: nenhum ponto seguro encontrado.");
            SetAnimations("Idle");
        }

        isPreparingJump = false;
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

    void TryAttack()
    {
        if (Time.time - lastAttackTime >= attackCooldown && Time.time > waitInitialTime)
        {
            SetAnimations("Attack");

            Vector2 direction = (player.position - transform.position).normalized;

            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.GetComponent<SimpleProjectile>().Initialize(direction);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            projectile.transform.rotation = Quaternion.Euler(0, 0, angle);

            lastAttackTime = Time.time;
        }
    }

    void MoveTowards(Vector2 target, float speed)
    {
        SetAnimations("Moving");
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    void SetAnimations(string animBoolName)
    {
        anim.SetBool("Attack", false);
        anim.SetBool("Moving", false);
        anim.SetBool("Jump", false);
        anim.SetBool("Idle", false);
        anim.SetBool(animBoolName, true);
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
