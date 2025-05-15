using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MagicStats;

public class MagicStatApplier : MonoBehaviour
{
    public static MagicStatApplier Instance;
    public List<MagicMappings> magicMappings;
    public Dictionary<MagicType, MagicStats> baseStatsPerMagic = new();
    public Dictionary<MagicType, MagicStats> currentStatsPerMagic = new();

    void Awake()
    {
        Instance = this;  
        foreach (var entry in magicMappings)
        {
            baseStatsPerMagic[entry.type] = entry.data.baseStats;
            currentStatsPerMagic[entry.type] = new MagicStats(entry.data.baseStats);
        }
    }

    public void ApplyMagicEffects(Dictionary<MagicType, List<GemEffect>> allEffects)
    {
        foreach (var kvp in allEffects)
        {
            MagicType magic = kvp.Key;
            List<GemEffect> effects = kvp.Value;

            if (!baseStatsPerMagic.ContainsKey(magic)) continue;

            var current = new MagicStats(baseStatsPerMagic[magic]);

            foreach (var effect in effects)
            {
                switch (effect.statAffected)
                {
                    case MagicStat.Damage:
                        current.damage += effect.valueModifier;
                        break;
                    case MagicStat.AttackRate:
                        current.attackRate += effect.valueModifier;
                        break;
                    case MagicStat.Speed:
                        current.speed += effect.valueModifier;
                        break;
                    case MagicStat.Lifetime:
                        current.lifetime += effect.valueModifier;
                        break;
                    case MagicStat.CoolDown:
                        current.coolDown += effect.valueModifier;
                        break;
                }
            }

            currentStatsPerMagic[magic] = current;
        }
    }
}
