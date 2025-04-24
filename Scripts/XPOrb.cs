using UnityEngine;

public class XPOrb : MonoBehaviour
{
    public int xpAmount = 1;
    public float moveSpeed = 5f;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) < 3f)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerXPSystem.Instance.GainXP(xpAmount);
            Destroy(gameObject);
        }
    }
}
