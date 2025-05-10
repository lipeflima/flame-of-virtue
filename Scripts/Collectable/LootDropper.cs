using UnityEngine;

public class LootDropper : MonoBehaviour
{
    [Header("Loot Config")]
    public LootDropData[] possibleLoot;

    public void DropLoot(Vector3 position)
    {
        foreach (var loot in possibleLoot)
        {
            if (Random.value <= loot.dropChance)
            {
                Instantiate(loot.itemPrefab, position, Quaternion.identity);
            }
        }
    }
}
