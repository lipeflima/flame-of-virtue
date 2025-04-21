using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyHideObject : MonoBehaviour
{
    public float time = 5;
    public bool hide;
    void Start()
    {
        if (!hide) Destroy(gameObject, time);
    }
    private void OnEnable()
    {
        if (hide) HideObject();
    }
    public void HideObject()
    {
        StartCoroutine(CountDown());        
    }
    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
