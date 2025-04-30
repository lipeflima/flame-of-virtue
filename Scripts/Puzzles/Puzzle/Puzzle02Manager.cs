using UnityEngine;

public class Puzzle02Manager : MonoBehaviour
{
    public PuzzleArea area1;
    public PuzzleArea area2;
    public PuzzleArea area3;
    public PuzzleArea area4;
    public GameObject doorToOpen;
    public GameObject chest;

    [SerializeField] private bool puzzleCompleted = false;

    void Update()
    {
        if (!puzzleCompleted && area1.IsComplete && area2.IsComplete && area3.IsComplete && area4.IsComplete 
            && area1.blocksInside.Count == area1.requiredBlocks && area2.blocksInside.Count == area2.requiredBlocks 
            && area3.blocksInside.Count == area3.requiredBlocks && area4.blocksInside.Count == area4.requiredBlocks)
        {
            
            chest.SetActive(true);
            OpenDoor();
            puzzleCompleted = true;
        }
    }

    void OpenDoor()
    {       
        GetComponent<HintUIManager>().MostrarAvisoSucesso();
        // Se quiser apenas desativar
        doorToOpen.SetActive(false);
    }
}