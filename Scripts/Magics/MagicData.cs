using UnityEngine;
using static MagicStats;

[System.Serializable]
[CreateAssetMenu(fileName = "newMagicData", menuName = "Data/Magic/Magic Data")]
public class MagicData : ScriptableObject
{
    public MagicStats baseStats;
    public MagicType magicType;
}

[System.Serializable]
public class MagicMappings
{
    public MagicType type;
    public MagicData data;
}
