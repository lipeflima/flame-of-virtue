using UnityEngine;

public class ItemIndicator : MonoBehaviour
{
    [SerializeField] private GameObject itemIndicatorPrefab;

    public void ShowItemIndicator(string item, int amount)
    {
        if (itemIndicatorPrefab != null)
        {
            Vector3 offset = new Vector3(0f, 3f, 0f);
            GameObject itemInstance = Instantiate(itemIndicatorPrefab, transform.position + offset, Quaternion.identity);
            itemInstance.GetComponent<ItemPopup>().Setup(item, amount);
        }
    }

}
