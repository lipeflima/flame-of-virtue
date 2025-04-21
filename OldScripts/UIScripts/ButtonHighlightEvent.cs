using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class ButtonHighlightEvent : MonoBehaviour
{
    [Header("Highlight Events")]
    public UnityEvent onHighlight;
    public UnityEvent onHighlightExit;

    private Button button;

    void Awake()
    {
        // Obt�m o componente Button
        button = GetComponent<Button>();

        // Adiciona os listeners para os eventos de Highlight e Exit
        EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>();

        // Configura��o para o evento PointerEnter (Highlight)
        EventTrigger.Entry entryEnter = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerEnter
        };
        entryEnter.callback.AddListener((data) => OnButtonHighlight());

        // Configura��o para o evento PointerExit (Highlight Exit)
        EventTrigger.Entry entryExit = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerExit
        };
        entryExit.callback.AddListener((data) => OnButtonHighlightExit());

        // Adiciona os eventos ao EventTrigger
        trigger.triggers.Add(entryEnter);
        trigger.triggers.Add(entryExit);
    }

    // M�todo chamado ao destacar o bot�o
    private void OnButtonHighlight()
    {
        onHighlight?.Invoke();
    }

    // M�todo chamado ao sair do destaque do bot�o
    private void OnButtonHighlightExit()
    {
        onHighlightExit?.Invoke();
    }
}
