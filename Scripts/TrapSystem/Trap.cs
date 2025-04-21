using System;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public float damage = 10f;
    public float cooldown = 2f; // Tempo entre danos
    public float nextDamageTime = 0f;
    private EnergySystem energia;
    private bool colidido = false;

    void Update()
    {
        if (colidido && Time.time > nextDamageTime)
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
}
