using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    [SerializeField] private float health = 30f;
    public GameObject xpOrbPrefab;
    public GameObject goldPrefab;
    private PlayerController controller;
    public GameObject hitEffect;

    private LootDropper lootDropper;

    void Start()
    {
        controller = PlayerController.Instance;
        lootDropper = GetComponent<LootDropper>();
    }

    public void TakeDamage(float amount)
    {
        int multiplier = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<ComboSystem>().GetMultiplier();  
        amount *= multiplier;
        health -= amount;

        GetComponent<DamageIndicator>().MostrarIndicadorDeDano(amount);
        controller.comboSystem.AddHit(amount);
        Instantiate(hitEffect, transform.position, transform.rotation);

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        DropXP();
        DropGold();

        if (lootDropper != null)
        {
            lootDropper.DropLoot(transform.position);
        }

        Destroy(gameObject);
    }

    void DropXP()
    {
        Vector2 randomOffset = Random.insideUnitCircle * 0.5f; // raio de até 0.5 unidades
        Vector3 dropPosition = transform.position + new Vector3(randomOffset.x, randomOffset.y, 0f);

        Instantiate(xpOrbPrefab, dropPosition, Quaternion.identity);
    }

    void DropGold()
    {
        Vector2 randomOffset = Random.insideUnitCircle * 0.5f; // raio de até 0.5 unidades
        Vector3 dropPosition = transform.position + new Vector3(randomOffset.x, randomOffset.y, 0f);
        
        Instantiate(goldPrefab, dropPosition, Quaternion.identity);
    }
}
