using UnityEngine;
using TMPro;

public class ItemPopup : MonoBehaviour
{
    public TMP_Text itemText;
    private float lifetime = 0.8f;
    private float floatSpeed = 3f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Sobe levemente
        transform.position += new Vector3(0, floatSpeed * Time.deltaTime, 0);
    }

    public void Setup(string item, int amount)
    {
        itemText.text = "+" + amount + " " + item;
    }
}