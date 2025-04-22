using UnityEngine;
public class EnemyBase : MonoBehaviour
{
    public float health;
    public float moveSpeed;
    public GameObject player;
    public float attackCooldown;

    protected bool canAttack = true;

    public virtual void Move() {}
    public virtual void Attack() {}
    public virtual void TakeDamage(float amount) {
        health -= amount;
        if (health <= 0) Die();
    }

    protected virtual void Die() {
        // Drop item, animação de morte, etc.
        Destroy(gameObject);
    }
}