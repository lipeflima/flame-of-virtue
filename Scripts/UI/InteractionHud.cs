using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionHud : MonoBehaviour
{
    public GameObject hud;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hud.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hud.SetActive(false);
        }
    }
}
