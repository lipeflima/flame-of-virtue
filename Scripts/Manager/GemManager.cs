using System.Collections.Generic;
using UnityEngine;
using static MagicStats;

public class GemManager : MonoBehaviour
{
    public static GemManager Instance;
    public Dictionary<MagicType, List<EquippedGem>> equippedGemsPerMagic = new();
    public MagicStatApplier statApplier;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void UpdateAllMagicStats()
    {
        Dictionary<MagicType, List<GemEffect>> allEffects = new();

        foreach (var kvp in equippedGemsPerMagic)
        {
            var magic = kvp.Key;
            var gems = kvp.Value;

            List<GemEffect> effects = new();
            
            foreach (var gem in gems)
                effects.AddRange(gem.GetTotalEffects());

            allEffects[magic] = effects;
        }

        statApplier.ApplyMagicEffects(allEffects);
    }
}
