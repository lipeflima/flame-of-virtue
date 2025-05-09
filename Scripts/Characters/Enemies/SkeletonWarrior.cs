using UnityEngine;
using UnityEngine.AI;

public class SkeletonWarrior : MonoBehaviour
{
    public enum State
    {
        Patrol,
        Wait,
        Chase,
        DashAttack
    }

    [Header("Referências")]
    public Transform player;
    public NavMeshAgent agent;
    public Transform model;
    public Animator anim;
    public LayerMask obstacleLayer;

    [Header("Configuração de Patrulha")]
    public float patrolRadius = 5f;
    public float patrolPauseTime = 2f;

    [Header("Detecção")]
    public float viewRange = 10f;
    public float attackRange = 2f;

    [Header("Movimento")]
    public float chaseSpeed = 3.5f;
    public float dashSpeed = 8f;
    public float dashDuration = 0.3f;
    public float dashCooldown = 2f;

    private State currentState = State.Patrol;
    private Vector3 patrolCenter;
    private Vector3 dashTarget;
    private float waitTimer;
    private float dashTimer;
    private float dashCooldownTimer;
    private bool isDashing;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        patrolCenter = transform.position;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        GoToRandomPatrolPoint();
    }

    private void Update()
    {
        if (player == null) return;

        LookAtPlayer();

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Atualiza cooldown do dash
        if (dashCooldownTimer > 0f)
            dashCooldownTimer -= Time.deltaTime;

        switch (currentState)
        {
            case State.Patrol:
                HandlePatrol(distanceToPlayer);
                break;

            case State.Wait:
                HandleWait();
                break;

            case State.Chase:
                HandleChase(distanceToPlayer);
                break;

            case State.DashAttack:
                HandleDash();
                break;
        }
    }

    private void HandlePatrol(float dist)
    {
        SetAnimations("Moving");

        if (dist < viewRange)
        {
            currentState = State.Chase;
            return;
        }

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            currentState = State.Wait;
            waitTimer = patrolPauseTime;
        }
    }

    private void HandleWait()
    {
        SetAnimations("Idle");

        waitTimer -= Time.deltaTime;
        if (waitTimer <= 0f)
        {
            currentState = State.Patrol;
            GoToRandomPatrolPoint();
        }
    }

    private void HandleChase(float dist)
    {
        SetAnimations("Moving");

        if (dist > viewRange)
        {
            currentState = State.Patrol;
            GoToRandomPatrolPoint();
            return;
        }

        if (dist <= attackRange && dashCooldownTimer <= 0f)
        {
            if (HasLineOfSightToPlayer())
            {
                StartDash();
                return;
            }
        }

        agent.isStopped = false;
        agent.speed = chaseSpeed;
        agent.SetDestination(player.position);
    }

    private void StartDash()
    {
        currentState = State.DashAttack;
        dashTarget = player.position;
        dashTimer = dashDuration;
        isDashing = true;
        agent.isStopped = true;
        SetAnimations("Attack");
    }

    private void HandleDash()
    {
        if (isDashing)
        {
            transform.position = Vector3.MoveTowards(transform.position, dashTarget, dashSpeed * Time.deltaTime);
            dashTimer -= Time.deltaTime;

            if (Vector3.Distance(transform.position, dashTarget) < 0.1f || dashTimer <= 0f)
            {
                isDashing = false;
                dashCooldownTimer = dashCooldown;
                currentState = State.Chase;
                SetAnimations("Idle");
            }
        }
    }

    private void GoToRandomPatrolPoint()
    {
        Vector2 randomPoint = patrolCenter + (Vector3)(Random.insideUnitCircle * patrolRadius);
        if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, 2f, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }

    private bool HasLineOfSightToPlayer()
    {
        Vector3 dir = (player.position - transform.position).normalized;
        float dist = Vector3.Distance(transform.position, player.position);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, dist, obstacleLayer);

        Debug.DrawRay(transform.position, dir * dist, hit.collider == null ? Color.green : Color.red, 1f);
        return hit.collider == null;
    }

    private void LookAtPlayer()
    {
        if (model == null || player == null) return;

        Vector3 scale = model.localScale;
        scale.x = player.position.x < transform.position.x ? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);
        model.localScale = scale;
    }

    private void SetAnimations(string animName)
    {
        anim.SetBool("Moving", false);
        anim.SetBool("Attack", false);
        anim.SetBool("Idle", false);
        anim.SetBool(animName, true);
    }
}
