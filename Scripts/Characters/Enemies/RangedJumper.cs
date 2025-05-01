using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedJumper : MonoBehaviour
{
    [Header("Referências")]
    public Transform player;
    public GameObject projectilePrefab;
    public Transform model; // modelo visual
    public SpriteRenderer spriteRenderer; // opcional: flip do sprite

    [Header("Parâmetros de Ataque")]
    public float attackRange = 6f;
    public float attackCooldown = 2f;
    private float lastAttackTime;
    private float viewRange = 20f;
    private float chaseSpeed = 3f;
    public float waitInitialTime = 0.2f;

    [Header("Distância Crítica para Pulo")]
    public bool canJump = false;
    public float dangerRange = 2f;
    public float safeDistanceMin = 3f;
    public float safeDistanceMax = 5f;
    public float jumpDelay = 2f;
    public float jumpSpeed = 10f;

    private bool isPreparingJump = false;
    private bool isJumping = false;
    private Vector2 jumpTarget;

    public Animator anim;
    private PlayerController controller;

    void Start()
    {
        SetAnimations("Idle");
        controller = PlayerController.Instance;
        player = controller.transform;
    }

    void Update()
    {
        if (player == null) return;

        LookAtPlayer(); // sempre olha pro player

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= dangerRange && !isPreparingJump && !isJumping)
        {
            if (canJump)
            {
                StartCoroutine(PrepareJump());
                return;
            } 
        }

        if (distance <= attackRange && !isPreparingJump && !isJumping)
        {
            TryAttack();
        }

        if (distance < viewRange && distance > attackRange)
        {
            MoveTowards(player.position, chaseSpeed);
        }

        if (isJumping)
        {
            transform.position = Vector2.MoveTowards(transform.position, jumpTarget, jumpSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, jumpTarget) < 0.1f)
            {
                isJumping = false;
            }
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

    void TryAttack()
    {
        if (Time.time - lastAttackTime >= attackCooldown && Time.time > waitInitialTime)
        {
            SetAnimations("Attack");

            Vector2 direction = (player.position - transform.position).normalized;

            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.GetComponent<SimpleProjectile>().Initialize(direction);

            // Rotaciona o projétil para apontar visualmente na direção
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

    IEnumerator PrepareJump()
    {
        isPreparingJump = true;

        yield return new WaitForSeconds(jumpDelay);

        Vector2 awayFromPlayer = (transform.position - player.position).normalized;
        float randomDistance = Random.Range(safeDistanceMin, safeDistanceMax);
        jumpTarget = (Vector2)transform.position + awayFromPlayer * randomDistance;

        isJumping = true;
        isPreparingJump = false;
        SetAnimations("Jump");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack"))
        {
            EnemyHP enemy = GetComponent<EnemyHP>();
            enemy.TakeDamage(collision.gameObject.GetComponent<PlayerProjectile>().damage);
        }
    }

    void SetAnimations(string animBoolName)
    {
        anim.SetBool("Attack", false);
        anim.SetBool("Moving", false);
        anim.SetBool("Jump", false);
        anim.SetBool("Idle", false);
        anim.SetBool(animBoolName, true);
    }
}
