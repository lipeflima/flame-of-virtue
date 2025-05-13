using System.Linq;
using UnityEngine;

public class ImprovementsManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AttachFragmentToGem(GemSO gem, GemFragment fragment)
    {
        if (gem.effects.Any(e => e.statAffected == fragment.statAffected))
        {
            // Soma o valor ao efeito já presente
        }
        else
        {
            // Adiciona como novo efeito extra
        }
    }
}
