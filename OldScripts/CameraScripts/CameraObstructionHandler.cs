using System.Collections.Generic;
using UnityEngine;

public class CameraObstructionHandler : MonoBehaviour
{
    [Header("Player Reference")]
    public Transform player;

    [Header("Transparent Material")]
    public Material transparentMaterial;

    [Header("Laser Settings")]
    public Color laserColor = Color.red;

    [Header("Detection Settings")]
    public DetectionType detectionType = DetectionType.Raycast; // Tipo de detecção
    public LayerMask layerMask;

    [Header("Raycast Settings")]
    public float playerHeight = 1;

    [Header("Sphere Check Settings")]
    public float sphereRadius = 1.0f; // Raio da checagem esférica
    public float sphereCheckDistance = 10.0f; // Distância máxima do SphereCheck

    // Enum para selecionar o tipo de detecção
    public enum DetectionType
    {
        Raycast,
        SphereCheck
    }

    // Dicionário para armazenar os materiais originais das obstruções
    private Dictionary<Renderer, Material[]> originalMaterials = new Dictionary<Renderer, Material[]>();

    // Lista para rastrear as obstruções atuais
    private List<Renderer> currentObstructions = new List<Renderer>();
    private Character character;

    private void Start()
    {
        character = Character.Instance;
        player = character.transform;
    }

    void Update()
    {
        HandleObstructions();
    }

    private void HandleObstructions()
    {
        if (player == null) return;

        // Limpa obstruções que não estão mais bloqueando a visão
        ClearObstructions();

        if (detectionType == DetectionType.Raycast)
        {
            HandleRaycastObstructions();
        }
        else if (detectionType == DetectionType.SphereCheck)
        {
            HandleSphereCheckObstructions();
        }
    }

    private void HandleRaycastObstructions()
    {
        Vector3 direction = player.position - transform.position;
        float distance = direction.magnitude;

        // Realiza o Raycast para detectar obstruções
        RaycastHit[] hits = Physics.RaycastAll(transform.position, new Vector3(direction.x, direction.y + playerHeight, direction.z), distance);

        // Processa os objetos detectados pelo Raycast
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.gameObject.layer != layerMask) ProcessObstruction(hit.collider.GetComponent<Renderer>());
        }
    }

    private void HandleSphereCheckObstructions()
    {
        Vector3 direction = player.position - transform.position;
        float distance = Mathf.Min(direction.magnitude, sphereCheckDistance);

        // Realiza uma checagem esférica ao longo do caminho
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, sphereRadius, direction.normalized, distance);

        // Processa os objetos detectados pelo SphereCast
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.gameObject.layer != layerMask) ProcessObstruction(hit.collider.GetComponent<Renderer>());
        }
    }

    private void ProcessObstruction(Renderer renderer)
    {
        if (renderer == null) return;

        // Salva os materiais originais
        if (!originalMaterials.ContainsKey(renderer))
        {
            originalMaterials[renderer] = renderer.materials;
        }

        // Substitui os materiais pelos transparentes
        ApplyTransparentMaterial(renderer);

        // Adiciona à lista de obstruções atuais
        currentObstructions.Add(renderer);
    }

    private void ClearObstructions()
    {
        for (int i = currentObstructions.Count - 1; i >= 0; i--)
        {
            Renderer renderer = currentObstructions[i];

            // Restaura os materiais originais
            if (originalMaterials.ContainsKey(renderer))
            {
                renderer.materials = originalMaterials[renderer];
                originalMaterials.Remove(renderer);
            }

            // Remove da lista de obstruções
            currentObstructions.RemoveAt(i);
        }
    }

    private void ApplyTransparentMaterial(Renderer renderer)
    {
        // Cria um array de materiais transparentes para substituir os materiais atuais
        Material[] transparentMaterials = new Material[renderer.materials.Length];
        for (int i = 0; i < renderer.materials.Length; i++)
        {
            transparentMaterials[i] = transparentMaterial;
        }
        renderer.materials = transparentMaterials;
    }

    private void OnDrawGizmos()
    {
        if (player == null) return;

        // Configura a cor do laser
        Gizmos.color = laserColor;

        if (detectionType == DetectionType.Raycast)
        {
            // Desenha o laser como uma linha entre a câmera e o jogador
            Gizmos.DrawLine(transform.position, player.position);
        }
        else if (detectionType == DetectionType.SphereCheck)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            Gizmos.color = Color.blue;

            // Desenha a esfera ao longo do caminho do SphereCast
            float step = sphereRadius * 2;
            for (float distance = 0; distance < sphereCheckDistance; distance += step)
            {
                Vector3 position = transform.position + direction * distance;
                Gizmos.DrawWireSphere(position, sphereRadius);
            }
        }
    }
}
