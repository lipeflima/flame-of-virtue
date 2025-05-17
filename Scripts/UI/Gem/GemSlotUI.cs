using UnityEngine;
using UnityEngine.UI;

public class GemSlotUI : MonoBehaviour
{
    public Image icon;
    // public GameObject emptyIcon;
    private GemSO currentGem;

    public void SetGem(GemSO gem)
    {
        currentGem = gem;
        icon.sprite = gem.icon;
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        currentGem = null;
        icon.enabled = false;
    }

    // Suporte a drag-and-drop pode ser adicionado aqui
}
