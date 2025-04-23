using UnityEngine;
public class JudgeProjectile : MonoBehaviour
{
    public float damage = 4f;
    public float speed = 5f;
    private Vector2 direction;

    public void Initialize(Vector2 dir)
    {
        direction = dir.normalized;
        Destroy(gameObject, 5f); // destrói após 5s
    }

    void Update()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            EnergySystem energia = collision.GetComponent<EnergySystem>();
            energia.DecreaseEnergy(damage);
            Destroy(gameObject);
        }
    }
}
