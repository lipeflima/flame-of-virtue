using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GemDragManager : MonoBehaviour
{
    public static GemDragManager Instance;

    [Header("Referências Visuais")]
    public GameObject dragIconPrefab;
    private GameObject currentIcon;
    private Image iconImage;

    public GemSO draggedGem;
     public GemSlotUI originSlot;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Update()
    {
        if (currentIcon != null)
        {
            currentIcon.transform.position = Input.mousePosition;

            if (Input.GetMouseButtonUp(0)) // botão esquerdo solto
            {
                TryDrop();
            }
        }
    }

    public void StartDrag(GemSO gem, GemSlotUI fromSlot)
    {
        draggedGem = gem;
        originSlot = fromSlot;
        currentIcon = Instantiate(dragIconPrefab, transform);
        iconImage = currentIcon.GetComponent<Image>();
        iconImage.sprite = gem.icon;
        iconImage.raycastTarget = false;
        fromSlot.Hide();
    }

    private void TryDrop()
    {
        // Raycast na UI para detectar se soltou em um slot válido
        PointerEventData pointerData = new(EventSystem.current)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (var result in results)
        {
            GemDropHandler dropHandler = result.gameObject.GetComponent<GemDropHandler>();
            if (dropHandler != null)
            {
                CompleteDrop(dropHandler);
                return;
            }
        }

        // Não soltou em um drop válido, cancelar
        CancelDrag();
    }

    public void CancelDrag()
    {
        if (originSlot != null)
        {
            originSlot.Show(draggedGem);
        }
        EndDrag();
    }

    public void CompleteDrop(GemDropHandler targetSlot)
    {
        if (targetSlot.EquipGem(draggedGem))
        {
            originSlot.ClearSlot(); // só se encaixou com sucesso
            EndDrag();
        }
        else
        {
            CancelDrag(); // volta ao inventário
        }
    }

    private void EndDrag()
    {
        if (currentIcon != null) Destroy(currentIcon);
        draggedGem = null;
        originSlot = null;
        currentIcon = null;
    }
}
