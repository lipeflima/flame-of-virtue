using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LevelUpManager : MonoBehaviour
{
    public GameObject levelUpTextPrefab;
    public GameObject levelUpEffect;
    public Image xpBarFill;

    void Start()
    {
        levelUpEffect.SetActive(false);
    }

    public void ShowText(Transform playerTransform)
    {
        Vector3 offset = new Vector3(0, 2f, 0);
        GameObject text = Instantiate(levelUpTextPrefab, playerTransform.position + offset, Quaternion.identity);

        text.transform.SetParent(playerTransform); 
        text.transform.localPosition = offset; 
        
        Destroy(text, 3f);
    }

    public void PlayLevelUpEffect()
    {
        levelUpEffect.SetActive(true);
        levelUpEffect.GetComponent<ParticleSystem>().Play();
    }
}
