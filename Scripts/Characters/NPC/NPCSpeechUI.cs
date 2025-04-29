using UnityEngine;
using UnityEngine.UI;
using TMPro; // Se usar TextMeshPro

public class SpeechBubbleUI : MonoBehaviour
{
    public GameObject speechBubblePanel;
    public TextMeshProUGUI speechText; // Ou "Text speechText;" se estiver usando UI Text normal
    public GameObject continueHint; // O objeto [E] para continuar

    public void ShowSpeech(string text)
    {
        speechBubblePanel.SetActive(true);
        speechText.text = text;
        continueHint.SetActive(true); // Mostra o "[E] Para continuar"
    }

    public void HideSpeech()
    {
        speechBubblePanel.SetActive(false);
        continueHint.SetActive(false); // Esconde o "[E] Para continuar"
    }
}
