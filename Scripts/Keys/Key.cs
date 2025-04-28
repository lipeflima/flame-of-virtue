using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private KeyType keyType;
    public enum KeyType { RustedKey, GoldKey, SilverKey, RedKey }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();
            if (inventory != null)
            {
                switch(keyType)
                {
                    case KeyType.RustedKey:
                        inventory.hasRustedKey = true;
                        inventory.AddItem("RustedKey");
                    break;
                    case KeyType.SilverKey:
                        inventory.hasSilverKey = true;
                        inventory.AddItem("SilverKey");
                    break;
                    case KeyType.GoldKey:
                        inventory.hasGoldKey = true;
                        inventory.AddItem("GoldKey");
                    break;
                    case KeyType.RedKey:
                        inventory.hasRedKey = true;
                        inventory.AddItem("RedKey");
                    break;
                }
                
                Destroy(gameObject); // Remove a chave do mapa
            }
        }
    }
}
