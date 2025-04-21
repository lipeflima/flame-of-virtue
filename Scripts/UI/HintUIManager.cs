using System.Collections;
using UnityEngine;

public class HintUIManager : MonoBehaviour
{
    public GameObject HintWarningUI;
    public float tempoExibicao = 2f;

    public void MostrarAviso()
    {
        StartCoroutine(ExibirTextoTemporario());
    }

    IEnumerator ExibirTextoTemporario()
    {
        HintWarningUI.SetActive(true);
        yield return new WaitForSeconds(tempoExibicao);
        HintWarningUI.SetActive(false);
    }
}
