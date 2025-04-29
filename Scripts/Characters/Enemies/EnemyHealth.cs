using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    [SerializeField] private float health = 30f;
    public GameObject xpOrbPrefab;
    public GameObject[] itemDrops; 
    [Range(0f, 1f)] public float dropChance = 0.25f;
    private PlayerController controller;
    public GameObject hitEffect;

    void Start()
    {
        controller = PlayerController.Instance;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        gameObject.GetComponent<DamageIndicator>().MostrarIndicadorDeDano(amount);
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
        TryDropItem();
        Destroy(gameObject);
    }

    void DropXP()
    {
        Instantiate(xpOrbPrefab, transform.position, Quaternion.identity);
    }

    void TryDropItem()
    {
        if (itemDrops.Length == 0) return;
        if (Random.value <= dropChance)
        {
            int randomIndex = Random.Range(0, itemDrops.Length);
            Instantiate(itemDrops[randomIndex], transform.position, Quaternion.identity);
        }
    }
}