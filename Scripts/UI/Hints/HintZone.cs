using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<HintUIManager>().ShowHint(true);
        }
    }
}
