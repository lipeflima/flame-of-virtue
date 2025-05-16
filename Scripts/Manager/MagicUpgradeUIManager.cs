using UnityEngine;

public class MagicUpgradeUIManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject magicUpdateUI;
    // Start is called before the first frame update
    void Update()
    {
        // Verifica se o jogador apertou a tecla "O"
        if (Input.GetKeyDown(KeyCode.U))
        {
            if (magicUpdateUI != null)
        {
            magicUpdateUI.SetActive(!magicUpdateUI.activeSelf);
        }
        }
    }
}
