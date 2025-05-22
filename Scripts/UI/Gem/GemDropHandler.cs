using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using static MagicStats;

public class GemDropHandler : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public MagicType slotType;
    public int slotIndex;
    public GameObject equipedTypeWarningUI;
    public GameObject equipedSlotWarningUI;

    public void OnDrop(PointerEventData eventData)
    {
        if (GemDragManager.Instance.draggedGem != null)
        {
            GemDragManager.Instance.CompleteDrop(this);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // feedback visual opcional
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // feedback visual opcional
    }

    public bool EquipGem(GemSO gem)
    {
        var slotUI = GetComponent<GemSlotUI>();

        if (slotUI != null && slotUI.HasGem())
        {
            GemDragManager.Instance.CancelDrag();
            equipedSlotWarningUI.SetActive(true);
            return false;
        }

        if (GemManager.Instance.IsGemTypeAlreadyEquipped(slotType, gem))
        {
            GemDragManager.Instance.CancelDrag();
            equipedTypeWarningUI.SetActive(true);
            return false;
        }

        GemManager.Instance.EquipGem(slotType, gem);
        slotUI.SetGem(gem);
        return true;
    }
}
