using UnityEngine;

public class ItemColetavel : MonoBehaviour
{
    public float valorEnergia = 10f; // Quantidade de energia que o item recupera

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            EnergySystem energia = other.GetComponent<EnergySystem>();

            if (energia != null)
            {
                energia.AddEnergy(valorEnergia);
            }

            // Aqui você pode colocar efeitos visuais/sons antes de destruir
            Destroy(gameObject); // Remove o item do cenário
        }
    }
}
