using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using static MagicStats;

public class MagicUpgradeUI : MonoBehaviour
{
    [System.Serializable]
    public class MagicTab
    {
        public MagicType magicType;
        public Button tabButton;
        public GameObject panelContent; // Painel com os detalhes da magia
        // public Image icon;
        public TMP_Text inputKeyText;
        public List<StatBar> statBars;
        public List<GemSlotUI> gemSlots;
    }

    [System.Serializable]
    public class StatBar
    {
        public MagicStat statType;
        public TMP_Text label;
        public Slider bar;
    }

    [Header("Abas de Magias")]
    public List<MagicTab> magicTabs;

    [Header("Fonte de Stats")]
    public MagicStatApplier statSource;

    [Header("Gerenciador de Gemas")]
    public GemManager gemManager;

    void Start()
    {
        foreach (var tab in magicTabs)
        {
            MagicTab localTab = tab; // necessário para evitar closures incorretos
            tab.tabButton.onClick.AddListener(() => ShowTab(localTab));
        }

        ShowTab(magicTabs[0]);
    }

    public void ShowTab(MagicTab selected)
    {
        foreach (var tab in magicTabs)
        {
            bool active = tab == selected;
            tab.panelContent.SetActive(active);
        }

        UpdateStatBars(selected);
        UpdateGemSlots(selected);
    }

    public void UpdateTabForType(MagicType selectedType)
    {
        UpdateStatBars(GetMagicTabByType(selectedType));
        UpdateGemSlots(GetMagicTabByType(selectedType));
    }

    void UpdateStatBars(MagicTab tab)
    {
        if (!statSource.currentStatsPerMagic.ContainsKey(tab.magicType)) return;
        MagicStats stats = statSource.currentStatsPerMagic[tab.magicType];

        foreach (var bar in tab.statBars)
        {
            int value = GetStatValue(stats, bar.statType);
            bar.bar.value = value;
            bar.label.text = $"{value:F1}";
        }
    }

    void UpdateGemSlots(MagicTab tab)
    {
        if (!gemManager.equippedGemsPerMagic.ContainsKey(tab.magicType)) return;
        List<EquippedGem> equipped = gemManager.equippedGemsPerMagic[tab.magicType];

        for (int i = 0; i < tab.gemSlots.Count; i++)
        {
            if (i < equipped.Count)
            {
                tab.gemSlots[i].SetGem(equipped[i].gemData);
            }
            else
            {
                tab.gemSlots[i].ClearSlot();
            }
        }
    }


    int GetStatValue(MagicStats stats, MagicStat type)
    {
        return type switch
        {
            MagicStat.Damage => (int)stats.damage,
            MagicStat.AttackRate => (int)stats.attackRate,
            MagicStat.Speed => (int)stats.speed,
            MagicStat.Lifetime => (int)stats.lifetime,
            MagicStat.CoolDown => (int)stats.coolDown,
            _ => 0
        };
    }

    public MagicTab GetMagicTabByType(MagicType type)
    {
        return magicTabs.Find((tab) => tab.magicType == type);
    }
}