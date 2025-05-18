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
    
    public void EquipGem(MagicType magicType, GemSO gemSO)
    {
        if (!equippedGemsPerMagic.ContainsKey(magicType))
        {
            equippedGemsPerMagic[magicType] = new List<EquippedGem>();
        }

        // Cria nova instância de EquippedGem
        EquippedGem equipped = new EquippedGem
        {
            gemData = gemSO,
            socketedFragments = new List<GemFragment>()
        };

        equippedGemsPerMagic[magicType].Add(equipped);

        // Atualiza UI (caso esteja em uso)
        FindObjectOfType<MagicUpgradeUI>()?.UpdateTabForType(magicType); // método auxiliar opcional
    }

    public List<EquippedGem> GetEquippedGems(MagicType slotType)
    {
        if (equippedGemsPerMagic.TryGetValue(slotType, out var gems))
        {
            return gems;
        }

        return new List<EquippedGem>(); // retorna lista vazia
    }

    public bool IsGemTypeAlreadyEquipped(MagicType slotType, GemSO gem)
    {
        return GetEquippedGems(slotType).Exists(g => g.gemData == gem);
    }

    public void UpdateAllMagicStats()
    {
        Dictionary<MagicType, List<GemEffect>> allEffects = new();

        foreach (var equippedGem in equippedGemsPerMagic)
        {
            var magic = equippedGem.Key;
            var gems = equippedGem.Value;

            List<GemEffect> effects = new();

            foreach (var gem in gems)
                effects.AddRange(gem.GetTotalEffects());

            allEffects[magic] = effects;
        }

        statApplier.ApplyMagicEffects(allEffects);
    }
}
