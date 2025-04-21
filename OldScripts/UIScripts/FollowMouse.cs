using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public Canvas canvas; // Referência ao Canvas onde o objeto UI está

    private RectTransform rectTransform;

    private void Start()
    {
        // Obtém o RectTransform do objeto
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        Follow();
    }

    private void Follow()
    {
        // Converte a posição do mouse para coordenadas locais no Canvas
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            Input.mousePosition,
            canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera,
            out Vector2 localPoint);

        // Atualiza a posição local do objeto UI
        rectTransform.localPosition = localPoint;
    }
}
