using UnityEngine;

public class CreateHordeAfterObjective : MonoBehaviour
{
    public GameObject horde;
    public PlayerInventory inventory;

    // Update is called once per frame
    void Update()
    {
        if (HasThreeElements()) horde.SetActive(true);
    }

    public bool HasThreeElements()
    {
        // Lista dos quatro elementos obrigatórios
        string[] requiredItems = { "Dove", "Lamb", "Water" };

        // Verifica se todos os itens obrigatórios estão contidos na lista 'itens'
        foreach (string item in requiredItems)
        {
            if (!inventory.GetItens().Contains(item))
                return false; // Se faltar qualquer um, retorna falso
        }

        return true; // Todos os itens estão presentes
    }
}
