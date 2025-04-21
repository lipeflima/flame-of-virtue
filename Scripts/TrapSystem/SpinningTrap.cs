using UnityEngine;

public class SpinningTrap : MonoBehaviour
{
    [Header("Ativação")]
    public bool ativa = true;

    [Header("Rotação")]
    public bool enableRotation = true;
    public float rotationSpeed = 90f;

    [Header("Movimento Ping-Pong")]
    public bool enablePingPong = true;
    public float moveDistance = 2f;       // Distância para frente
    public float moveSpeed = 2f;
    public float waitTime = 2f;

    private Vector3 pointA;
    private Vector3 pointB;
    private Vector3 targetPosition;
    private bool waiting = false;

    [Header("Interaction")]
    [SerializeField] private bool colidido = false;
    private EnergySystem energia;
    public float cooldown = 1f;  
    private float nextDamageTime = 0f;
    public float damage = 5f;

    void Start()
    {
        pointA = transform.position;
        pointB = pointA + transform.right * moveDistance; // Direção "para frente"
        targetPosition = pointB;
    }

    void Update()
    {
        if (!ativa) return;

        if (enableRotation)
        {
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        }

        if (enablePingPong && !waiting)
        {
            MovePingPong();
        }

        if (ativa && colidido && Time.time > nextDamageTime)
        {
            energia.DecreaseEnergy(damage);
            nextDamageTime = Time.time + cooldown;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            colidido = true;
            energia = other.GetComponent<EnergySystem>();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            nextDamageTime = 0f;
            colidido = false;
        }
    }

    void MovePingPong()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.05f)
        {
            StartCoroutine(WaitAndSwitch());
        }
    }

    System.Collections.IEnumerator WaitAndSwitch()
    {
        waiting = true;
        yield return new WaitForSeconds(waitTime);
        targetPosition = (targetPosition == pointA) ? pointB : pointA;
        waiting = false;
    }
}
