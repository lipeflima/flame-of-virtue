using UnityEngine;

public class XPOrb : MonoBehaviour
{
    public int xpAmount = 1;
    public float moveSpeed = 5f;
    public float triggerDistance = 8f;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) < triggerDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            int multiplier = GameObject.FindGameObjectWithTag("Player").GetComponent<ComboSystem>().GetMultiplier(); 
            other.GetComponent<PlayerXP>().AddXP(xpAmount*multiplier);
            GetComponent<ItemIndicator>().ShowItemIndicator("XP", xpAmount*multiplier);
            Destroy(gameObject);
        }
    }
}
