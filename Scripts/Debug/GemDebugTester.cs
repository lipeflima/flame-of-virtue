using UnityEngine;
using System.Collections.Generic;
using static MagicStats;

public class GemDebugTester : MonoBehaviour
{
    [Header("Configuração de Teste")]
    public GemSO testGem;
    public MagicType magicType;
    public int fragmentCount = 2;
    public GameObject player;

    void Start()
    {
        TestGemWithFragments();
    }

    void TestGemWithFragments()
    {
        int playerLevel = player.transform.GetComponent<PlayerXP>().currentLevel;

        // Cria uma gema equipada
        EquippedGem equipped = new EquippedGem
        {
            gemData = testGem,
            socketedFragments = new List<GemFragment>()
        };

        for (int i = 0; i < fragmentCount; i++)
        {
            var frag = new GemFragment(testGem, playerLevel);
            equipped.socketedFragments.Add(frag);
            Debug.Log($"Fragmento {i + 1} - {frag.statAffected}: {frag.valueModifier}");
        }

        // Aplica essa gema na magia de teste
        if (!GemManager.Instance.equippedGemsPerMagic.ContainsKey(magicType))
        {
            GemManager.Instance.equippedGemsPerMagic[magicType] = new List<EquippedGem>();
        }

        GemManager.Instance.equippedGemsPerMagic[magicType].Clear();
        GemManager.Instance.equippedGemsPerMagic[magicType].Add(equipped);

        GemManager.Instance.UpdateAllMagicStats();

        // Visualização do resultado
        MagicStats result = GemManager.Instance.statApplier.currentStatsPerMagic[magicType];

        Debug.Log($"--- Stats finais de {magicType} ---");
        Debug.Log($"Dano: {result.damage}");
        Debug.Log($"FireRate: {result.attackRate}");
        Debug.Log($"Velocidade: {result.speed}");
        Debug.Log($"Lifetime: {result.lifetime}");
    }
}
