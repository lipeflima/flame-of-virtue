using UnityEngine;

public class SimpleProjectile : MonoBehaviour
{
    public float speed = 8f;
    public float lifeTime = 5f;
    public int damage = 3;

    private Vector2 moveDirection;

    public void Initialize(Vector2 direction)
    {
        moveDirection = direction.normalized;
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            EnergySystem energia = collision.GetComponent<EnergySystem>();
            energia.DecreaseEnergy(damage);
        }
        else if (!collision.isTrigger)
        {
            // Se bater em parede ou obstáculo, destrói
            Destroy(gameObject);
        }
    }
}
