using UnityEngine;

public class PlayerObjectCarrier : MonoBehaviour
{
    public Transform carryPoint; // Ponto onde o objeto carregado ficará preso (geralmente um filho do player)
    public float pickUpDistance = 1.5f; // Distância máxima para pegar objetos
    public LayerMask pickUpLayer; // Layer dos objetos que podem ser pegos

    private GameObject carriedObject = null; // O objeto atualmente carregado
    private bool isCarrying = false; // Se o player está carregando algo

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isCarrying)
            {
                TryPickUp();
            }
            else
            {
                DropObject();
            }
        }
    }

    void TryPickUp()
    {
        // Faz um raio para detectar um objeto próximo
        Collider2D hit = Physics2D.OverlapCircle(transform.position, pickUpDistance, pickUpLayer);

        if (hit != null)
        {
            carriedObject = hit.gameObject;
            carriedObject.transform.SetParent(carryPoint); // Faz o objeto seguir o ponto de carga
            carriedObject.transform.localPosition = Vector3.zero; // Centraliza no ponto
            // carriedObject.GetComponent<Rigidbody2D>().isKinematic = true; // Desativa a física para não sair voando
            isCarrying = true;
        }
    }

    void DropObject()
    {
        if (carriedObject != null)
        {
            carriedObject.transform.SetParent(null); // Solta do player
            // carriedObject.GetComponent<Rigidbody2D>().isKinematic = false; // Volta a física
            carriedObject = null;
            isCarrying = false;
        }
    }

    // Opcional: desenhar o raio de alcance no editor para facilitar ajustes
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, pickUpDistance);
    }
}
