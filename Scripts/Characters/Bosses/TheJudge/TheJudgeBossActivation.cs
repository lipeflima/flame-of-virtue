using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheJudgeBossActivation : MonoBehaviour
{
    public GameObject boss;
    public GameObject bossHpUI;
    public bool colidiu = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            colidiu = true;
            boss.SetActive(true);
            bossHpUI.SetActive(true);
            Destroy(gameObject);
        }
    }
}
