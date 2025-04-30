using System.Collections;
using UnityEngine;

public class HintUIManager : MonoBehaviour
{
    public GameObject hintSucess;
    public GameObject hintFailure;
    private GameObject HintWarningUI;
    public float tempoExibicao = 2f;
    public bool shouldDestroy = true;

    public void MostrarAviso()
    {
        HintWarningUI = hintFailure;
        StartCoroutine(ExibirTextoTemporario());
    }

    public void MostrarAvisoSucesso()
    {
        HintWarningUI = hintSucess;
        StartCoroutine(ExibirTextoTemporario());
    }

    IEnumerator ExibirTextoTemporario()
    {
        HintWarningUI.SetActive(true);
        yield return new WaitForSeconds(tempoExibicao);
        HintWarningUI.SetActive(false);
        if (shouldDestroy == true) this.enabled = false;
    }
}
