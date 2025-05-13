using UnityEngine;

[System.Serializable]
public class LootDropData
{
    public GameObject itemPrefab;
    [Range(0f, 1f)] public float dropChance = 0.5f;
}