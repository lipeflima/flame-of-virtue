using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public PuzzleArea area1;
    public PuzzleArea area2;
    public GameObject doorToOpen;

    private bool puzzleCompleted = false;

    void Update()
    {
        if (!puzzleCompleted && area1.IsComplete && area2.IsComplete && area1.blocksInside.Count == area2.blocksInside.Count)
        {
            OpenDoor();
            puzzleCompleted = true;
        }
    }

    void OpenDoor()
    {
        // Se quiser apenas desativar
        doorToOpen.SetActive(false);

        // Se tiver animação, pode chamar um Animator aqui
        // doorToOpen.GetComponent<Animator>().SetTrigger("Open");
        GetComponent<HintUIManager>().MostrarAviso();
    }
}
