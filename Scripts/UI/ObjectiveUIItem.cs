using TMPro;
using UnityEngine;

public class ObjectiveUIItem : MonoBehaviour
{
    public TMP_Text objectiveText;

    public void SetText(string text)
    {
        objectiveText.text = text;
    }

    public void MarkCompleted()
    {
        objectiveText.text = $"<s>{objectiveText.text}</s>";
        objectiveText.color = Color.gray;
    }
}
