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
        // Obtém o componente Button
        button = GetComponent<Button>();

        // Adiciona os listeners para os eventos de Highlight e Exit
        EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>();

        // Configuração para o evento PointerEnter (Highlight)
        EventTrigger.Entry entryEnter = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerEnter
        };
        entryEnter.callback.AddListener((data) => OnButtonHighlight());

        // Configuração para o evento PointerExit (Highlight Exit)
        EventTrigger.Entry entryExit = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerExit
        };
        entryExit.callback.AddListener((data) => OnButtonHighlightExit());

        // Adiciona os eventos ao EventTrigger
        trigger.triggers.Add(entryEnter);
        trigger.triggers.Add(entryExit);
    }

    // Método chamado ao destacar o botão
    private void OnButtonHighlight()
    {
        onHighlight?.Invoke();
    }

    // Método chamado ao sair do destaque do botão
    private void OnButtonHighlightExit()
    {
        onHighlightExit?.Invoke();
    }
}
