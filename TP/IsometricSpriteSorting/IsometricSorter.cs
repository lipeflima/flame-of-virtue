using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Transform))]
public class IsometricSorter : MonoBehaviour
{
    public static int PrecisionValue => -10;

    SpriteRenderer spriteRenderer;
    TilemapRenderer tilemapRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        tilemapRenderer = GetComponent<TilemapRenderer>();
    }

    void LateUpdate()
    {
        int order = (int)(transform.position.y * PrecisionValue);

        if (spriteRenderer != null)
            spriteRenderer.sortingOrder = order;

        if (tilemapRenderer != null)
            tilemapRenderer.sortingOrder = order;
    }
}
