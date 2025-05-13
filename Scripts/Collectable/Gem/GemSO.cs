using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gems/PowerGem")]
public class GemSO : ScriptableObject
{
    public enum FlameEffectType { Recovery, Burn, Intangibility, Ricochet, Fragmentation, Freeze }
    public enum GemType { Magma, Ice, Rubber, Crystal, Espiritual, Life }
    public GemType gemType;
    public string gemName;
    public Sprite icon;
    [TextArea]
    public string description;

    public List<GemEffect> effects;
    public bool enablesEffect; // ex: cura, queima, etc.
}
