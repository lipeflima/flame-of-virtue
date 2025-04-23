using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    [SerializeField] private float health = 30f;
    public void TakeDamage(float amount)
    {
        health -= amount;
        gameObject.GetComponent<DamageIndicator>().MostrarIndicadorDeDano(amount);

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}