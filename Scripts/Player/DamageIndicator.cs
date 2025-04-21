using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer; // arraste o SpriteRenderer no inspetor
    [SerializeField] private Color corDano = new Color(1f, 0.3f, 0.3f); // vermelho claro
    [SerializeField] private float duracaoDano = 0.2f;
    [SerializeField] private GameObject damagePrefab;

    private Color corOriginal;
    private Coroutine rotinaDano;

    private void Start()
    {
        corOriginal = spriteRenderer.color;
    }

    public void MostrarIndicadorDeDano(float amount)
    {
        if (rotinaDano != null)
            StopCoroutine(rotinaDano);

        rotinaDano = StartCoroutine(PiscarCor());
        if (damagePrefab != null)
        {
            GameObject damageInstance = Instantiate(damagePrefab, transform.position, Quaternion.identity);
            damageInstance.GetComponent<DamagePopup>().Setup(amount);
        }
    }

    private IEnumerator PiscarCor()
    {
        spriteRenderer.color = corDano;
        yield return new WaitForSeconds(duracaoDano);
        spriteRenderer.color = corOriginal;
    }

}
