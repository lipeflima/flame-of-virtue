using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CurvedProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float turnSpeed = 200f;
    [SerializeField] private float lifetime = 3f;
    [SerializeField] private float destroyDistance = 0.3f; // distância mínima para destruir ao chegar no mouse

    private Rigidbody2D rb;
    private Vector2 currentDirection;
    private float timer;
    private PlayerProjectile playerProjectile;

    void Start()
    {
        playerProjectile = GetComponent<PlayerProjectile>();
        rb = GetComponent<Rigidbody2D>();
        timer = lifetime;

        // Direção inicial aleatória
        Vector2 randomDir = Random.insideUnitCircle.normalized;
        currentDirection = randomDir;
    }

    void FixedUpdate()
    {
        // Atualiza timer de vida
        timer -= Time.fixedDeltaTime;
        if (timer <= 0f)
        {
            SpawnEffects();
            Destroy(gameObject);
            return;
        }

        // Posição do mouse no mundo
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Checar se está suficientemente próximo do mouse
        if (Vector2.Distance(rb.position, mouseWorldPos) <= destroyDistance)
        {
            SpawnEffects();
            Destroy(gameObject);
            return;
        }

        // Direção desejada
        Vector2 desiredDirection = (mouseWorldPos - rb.position).normalized;

        // Curva suavemente em direção ao mouse
        currentDirection = Vector2.Lerp(currentDirection, desiredDirection, turnSpeed * Time.fixedDeltaTime).normalized;

        // Move o projétil
        rb.MovePosition(rb.position + currentDirection * speed * Time.fixedDeltaTime);
    }

    void SpawnEffects()
    {
        if(playerProjectile.hitEffect != null) Instantiate(playerProjectile.hitEffect, transform.position, transform.rotation);
    }
}
