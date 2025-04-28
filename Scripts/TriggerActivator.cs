using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerActivator : MonoBehaviour
{
    public string checkTag;
    public GameObject interactHud;
    public bool interactInput = false;

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
            interactInput = col.gameObject.GetComponent<PlayerController>().interactInput;
            OnStay();
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.CompareTag(checkTag))
        {
            interactInput = false;
            interactHud.SetActive(false);
            OnExit();
        }
    }

    public virtual void OnEnter(){}
    public virtual void OnStay(){}
    public virtual void OnExit(){}
}
