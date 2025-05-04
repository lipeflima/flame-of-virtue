using UnityEngine;

public class PlayerSword : MonoBehaviour
{
    [SerializeField] private GameObject damageEffectPrefab;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackCooldown = 0.3f;
    [SerializeField] private Transform modelTransform; // Referência ao modelo visual

    private float lastAttackTime = 0f;
    private Camera mainCamera;

    Vector3 mouseWorldPos;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;
        HandleFlip(mouseWorldPos);
    }

    public void Attack()
    {
        if (Time.time < lastAttackTime + attackCooldown)
            return;

        lastAttackTime = Time.time;        

        // Calcula direção e ponto de ataque respeitando o alcance
        Vector3 direction = (mouseWorldPos - transform.position).normalized;
        Vector3 clampedPos = transform.position + Vector3.ClampMagnitude(direction * attackRange, attackRange);

        // Instancia o efeito de dano
        Instantiate(damageEffectPrefab, clampedPos, Quaternion.identity);        
    }

    public void StopAttack()
    {
        // Se necessário, implementar lógica de parada
    }

    private void HandleFlip(Vector3 mouseWorldPos)
    {
        if (modelTransform == null) return;

        float deltaX = mouseWorldPos.x - transform.position.x;

        Vector3 scale = modelTransform.localScale;

        if (deltaX < 0 && scale.x > 0)
        {
            scale.x *= -1;
        }
        else if (deltaX > 0 && scale.x < 0)
        {
            scale.x *= -1;
        }

        modelTransform.localScale = scale;
    }
}
