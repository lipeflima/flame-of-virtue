using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    public int currentSlot, currentLevel, currentHP, maxHP, currentMana, maxMana, currentAmber, currentTerabitia, currentIron, currentCobalt, currentWood, currentErb;
    public float playerPosX, playerPosY, playerPosZ;
    public List<string> levels;
    public int chestAmount = 1, minesAmount = 50;
    private SaveDataManager sDataManager;
    private void Start()
    {
        sDataManager = GetComponent<SaveDataManager>();
    }
    public void SetSlot(int slot)
    {
        switch(slot)
        {
            case 1: break; 
            case 2: break; 
            case 3: break; 
            case 4: break; 
            case 5: break;
        }
    }
    public void ResetAllData()
    {
        ResetChests();
        ResetMines();
        ResetDoors();
        ResetMineralsAndCards();
        ResetDayStatus();
        ResetEncounters();
    }
    void ResetDoors()
    {
        PlayerPrefs.SetInt("DoorStatusID", 0);
    }
    void ResetChests()
    {
        for (int i = 0; i < chestAmount; i++)
        {
            PlayerPrefs.SetInt("Openned" + "Chest" + i, 0);
        }
    }
    void ResetMines()
    {
        for (int i = 0; i < 10; i++)
        {
            PlayerPrefs.SetInt("CurrentAmountLeft" + "" + i, 1000);
        }
    }
    void ResetMineralsAndCards()
    {
        sDataManager.ResetData("minerals");
        sDataManager.ResetData("cards");
        sDataManager.ResetData("EquipedTools");
    }
    public void ResetDayStatus()
    {
        PlayerPrefs.SetFloat("TimeElapsed", 6);
        PlayerPrefs.SetInt("IsDay", 1);
        PlayerPrefs.SetFloat("SunRotation", 0);
    }
    public void ResetEncounters()
    {
        foreach(string name in levels)
        {
            for(int i = 0; i < 50; i++)
            {
                PlayerPrefs.SetString("Encounter" + "" + name + "" + i, "False");
            }              
        }             
    }
}
