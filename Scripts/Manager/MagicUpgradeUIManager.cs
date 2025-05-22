using UnityEngine;

public class MagicUpgradeUIManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject magicUpdateUI;
    // Start is called before the first frame update
    private CanvasGroup canvasGroup;

    void Start()
    {
       canvasGroup = magicUpdateUI.GetComponent<CanvasGroup>();
    }
    void Update()
    {
        // Verifica se o jogador apertou a tecla "O"
        if (Input.GetKeyDown(KeyCode.U))
        {
            if (magicUpdateUI != null)
            {
                bool isHidden = canvasGroup.alpha <= 0f;
                canvasGroup.alpha = isHidden ? 1f : 0f;
                canvasGroup.blocksRaycasts = isHidden; 
                canvasGroup.interactable = isHidden; 

            }
        }
    }
}
