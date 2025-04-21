using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public Canvas canvas; // Refer�ncia ao Canvas onde o objeto UI est�

    private RectTransform rectTransform;

    private void Start()
    {
        // Obt�m o RectTransform do objeto
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        Follow();
    }

    private void Follow()
    {
        // Converte a posi��o do mouse para coordenadas locais no Canvas
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            Input.mousePosition,
            canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera,
            out Vector2 localPoint);

        // Atualiza a posi��o local do objeto UI
        rectTransform.localPosition = localPoint;
    }
}
