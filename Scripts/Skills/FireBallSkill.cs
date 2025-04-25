using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallSkill : MonoBehaviour
{
    public static FireBallSkill instance;
    public GameObject fireballPrefab;
    public int quantidade = 1;
    public float intervaloEntreFireballs = 5f;
    public float alcance = 360f;
    private bool hasFireBallSkill = false;

    void Awake()
    {
        instance = this;   
    }

    public void Ativar()
    {
        StartCoroutine(DispararFireballs());
    }

    IEnumerator DispararFireballs()
    {
        for (int i = 0; i < quantidade; i++)
        {
            float angle = Random.Range(0, alcance);
            Vector2 dir = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));

            GameObject f = Instantiate(fireballPrefab, transform.position, Quaternion.identity);
            f.GetComponent<Fireball>().SetDirection(dir);

            yield return new WaitForSeconds(intervaloEntreFireballs);
        }
    }

    public bool HasFireBallSkill()
    {
        return hasFireBallSkill;
    }

    public void SetFireBallSkill(bool status)
    {
        hasFireBallSkill = status;
    }
}
