using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerActivator : MonoBehaviour
{
    public string checkTag;
    public GameObject interactHud;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag(checkTag))
        {
            interactHud.SetActive(true);
            OnEnter();
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.CompareTag(checkTag))
        {
            OnStay();
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.CompareTag(checkTag))
        {
            interactHud.SetActive(false);
            OnExit();
        }
    }

    public virtual void OnEnter(){}
    public virtual void OnStay(){}
    public virtual void OnExit(){}
}
