using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    public TMP_Text damageText;
    private float lifetime = 0.5f;
    private float floatSpeed = 1f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Sobe levemente
        transform.position += new Vector3(0, floatSpeed * Time.deltaTime, 0);
    }

    public void Setup(float damage)
    {
        damageText.text = "-" + damage.ToString();
    }
}