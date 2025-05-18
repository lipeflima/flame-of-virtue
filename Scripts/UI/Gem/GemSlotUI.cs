using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GemSlotUI : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Image icon;
    private GemSO currentGem;

    public void SetGem(GemSO gem)
    {
        currentGem = gem;
        icon.sprite = gem.icon;
        icon.enabled = true;
    }

    public bool HasGem()
    {
        return currentGem != null;
    }

    public void ClearSlot()
    {
        currentGem = null;
        icon.enabled = false;
    }

    public void Hide()
    {
        icon.enabled = false;
    }

    public void Show(GemSO gem)
    {
        SetGem(gem);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (currentGem != null && eventData.button == PointerEventData.InputButton.Left)
        {
            GemDragManager.Instance.StartDrag(currentGem, this);
        }
    }

    public void OnDrag(PointerEventData eventData) { }

    public void OnEndDrag(PointerEventData eventData) { }
}
