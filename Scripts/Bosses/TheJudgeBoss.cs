using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TheJudgeBoss : MonoBehaviour
{
    public enum BossPhase { Phase1, Phase2, Phase3 }
    public BossPhase currentPhase = BossPhase.Phase1;

    public float maxHealth = 300f;
    private float currentHealth;

    public Transform player;
    // private Animator anim;
    private bool isTransitioning = false;

    // Fase 1
    public GameObject projectilePrefab;
    public float projectileCooldown = 2f;
    private float projectileTimer;

    // Fase 2
    public float dashSpeed = 10f;
    public float dashCooldown = 3f;
    private float dashTimer;
    private bool isDashing = false;
    public float dashTime = 2f;
    public GameObject minionPrefab;
    public Transform[] spawnPoints;
    public float invokeCooldown = 10f;
    private float invokeTimer;

    // Fase 3
    public GameObject explosionEffect;
    public float teleportCooldown = 5f;
    private float teleportTimer;

    // Dano ao player
    
    private EnergySystem energia;
    private bool colidido = false;
    public float cooldown = 2f; // Tempo entre danos
    public float nextDamageTime = 0f;
    public float damage = 8f;

    void Start()
    {
        currentHealth = maxHealth;
        // anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null) return;

        HandlePhases();

        switch (currentPhase)
        {
            case BossPhase.Phase1:
                Phase1_Behavior();
                break;
            case BossPhase.Phase2:
                Phase2_Behavior();
                break;
            case BossPhase.Phase3:
                Phase3_Behavior();
                break;
        }

        if (colidido && Time.time > nextDamageTime)
        {
            energia.DecreaseEnergy(damage);
            nextDamageTime = Time.time + cooldown;
        }
    }

    void HandlePhases()
    {
        if (isTransitioning) return;

        if (currentHealth <= maxHealth * 0.66f && currentPhase == BossPhase.Phase1)
        {
            StartCoroutine(TransitionToPhase(BossPhase.Phase2));
        }
        else if (currentHealth <= maxHealth * 0.33f && currentPhase == BossPhase.Phase2)
        {
            StartCoroutine(TransitionToPhase(BossPhase.Phase3));
        }
    }

    void Phase1_Behavior()
    {
        projectileTimer += Time.deltaTime;
        if (projectileTimer >= projectileCooldown)
        {
            FireJudgmentProjectile();
            projectileTimer = 0;
        }
    }

    void FireJudgmentProjectile()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        proj.GetComponent<JudgeProjectile>().Initialize(direction);
    }

    void FirePenitenceProjectile()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
    }

    void Phase2_Behavior()
    {
        Phase1_Behavior();
        dashTimer += Time.deltaTime;
        invokeTimer += Time.deltaTime;

        if (!isDashing && dashTimer >= dashCooldown)
        {
            StartCoroutine(DashAtPlayer());
            dashTimer = 0;
        }

        if (invokeTimer >= invokeCooldown)
        {
            SummonMinions();
            invokeTimer = 0;
        }
    }

    private IEnumerator DashAtPlayer()
    {
        isDashing = true;
        Vector2 direction = (player.position - transform.position).normalized;

        float elapsed = 0;
        while (elapsed < dashTime)
        {
            transform.position += (Vector3)(direction * dashSpeed * Time.deltaTime);
            elapsed += Time.deltaTime;
            yield return null;
        }

        isDashing = false;
    }

    void Phase3_Behavior()
    {
        teleportTimer += Time.deltaTime;
        if (teleportTimer >= teleportCooldown)
        {
            TeleportNearPlayer();
            teleportTimer = 0;
        }
    }

    void TeleportNearPlayer()
    {
        Vector2 randomOffset = Random.insideUnitCircle.normalized * 3f;
        Vector3 teleportPos = player.position + (Vector3)randomOffset;
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        transform.position = teleportPos;
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
    }

    private IEnumerator TransitionToPhase(BossPhase newPhase)
    {
        isTransitioning = true;
        // anim.SetTrigger("PhaseTransition");
        yield return new WaitForSeconds(2f); // tempo de transição
        currentPhase = newPhase;
        isTransitioning = false;
    }

    void SummonMinions()
    {
        foreach (Transform point in spawnPoints)
        {
            Instantiate(minionPrefab, point.position, Quaternion.identity);
        }

        // anim.SetTrigger("Summon");
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        gameObject.GetComponent<DamageIndicator>().MostrarIndicadorDeDano(amount);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // anim.SetTrigger("Die");
        // Desativar coliders, parar ações etc.
        Destroy(gameObject, 3.1f);
        Invoke("GameOver", 3);
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
            TakeDamage(collision.gameObject.GetComponent<PlayerProjectile>().damage);
        }
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}
