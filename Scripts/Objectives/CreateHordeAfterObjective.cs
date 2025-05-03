using UnityEngine;

public class CreateHordeAfterObjective : MonoBehaviour
{
    public GameObject horde;
    public PlayerInventory inventory;
    public string[] requiredItems;

    // Update is called once per frame
    void Update()
    {
        if (HasRequiredItens()) horde.SetActive(true);
    }

    public bool HasRequiredItens()
    {
        // Verifica se todos os itens obrigatórios estão contidos na lista 'itens'
        foreach (string item in requiredItems)
        {
            if (!inventory.GetItens().Contains(item))
                return false; 
        }

        return true; // Todos os itens estão presentes
    }
}
