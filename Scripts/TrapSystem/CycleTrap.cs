using UnityEngine;

public class CycleTrap : MonoBehaviour
{
    public float damage = 10f;
    public float cooldown = 1f;        // Cooldown entre danos
    public float tempoAtiva = 2f;           // Quanto tempo ela fica ativa
    public float tempoInativa = 3f;         // Quanto tempo ela fica desativada
    
    private float nextDamageTime = 0f;
    public SpriteRenderer sr;
    public ParticleSystem particulas;
    private EnergySystem energia;
    [SerializeField] private bool colidido = false;
    [SerializeField] private bool ativa = true;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        StartCoroutine(AlternarEstado());
    }

    void Update()
    {
        if (sr != null)
        {
            sr.color = ativa ? Color.red : Color.gray;
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

    System.Collections.IEnumerator AlternarEstado()
    {
        while (true)
        {
            Ativar();
            yield return new WaitForSeconds(tempoAtiva);
            Desativar();
            yield return new WaitForSeconds(tempoInativa);
        }
    }

    private void Ativar()
    {
        ativa = true;
        if (particulas != null) particulas.Play();
    }

    private void Desativar()
    {
        ativa = false;
        if (particulas != null) particulas.Stop();
    }
}
