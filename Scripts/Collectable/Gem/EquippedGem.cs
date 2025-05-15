using System.Collections.Generic;

public class EquippedGem
{
    public GemSO gemData;
    public List<GemFragment> socketedFragments = new();

    public List<GemEffect> GetTotalEffects()
    {
        List<GemEffect> combined = new();

        // Adiciona os efeitos base da gema
        if (gemData.effects != null)
            combined.AddRange(gemData.effects);

        // Adiciona efeitos de cada fragmento
        foreach (var frag in socketedFragments)
            combined.Add(new GemEffect { statAffected = frag.statAffected, valueModifier = frag.valueModifier });

        return combined;
    }
}
