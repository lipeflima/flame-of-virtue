using UnityEngine;
using System.Collections.Generic;

public class PuzzleArea : MonoBehaviour
{
    public int requiredBlocks = 1;
    public List<GameObject> blocksInside = new List<GameObject>();

    public bool IsComplete = false;

    void Update()
    {
         IsComplete = blocksInside.Count >= requiredBlocks;   
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PuzzleBlock") && !blocksInside.Contains(other.gameObject))
        {
            blocksInside.Add(other.gameObject);   
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PuzzleBlock") && blocksInside.Contains(other.gameObject))
        {
            blocksInside.Remove(other.gameObject);
            if (blocksInside.Count < requiredBlocks) IsComplete = false;      
        }
    }
}
