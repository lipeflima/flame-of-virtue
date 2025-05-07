using UnityEngine;

public class AreaTrigger : MonoBehaviour
{
    public string areaTag;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<CompletedObjectiveManager>().RegisterAreaEntry(areaTag);
        }
    }
}
