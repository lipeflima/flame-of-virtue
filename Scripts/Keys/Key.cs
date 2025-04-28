using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private KeyType keyType;
    public enum KeyType { RustedKey, GoldKey, SilverKey }
    
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
                    break;
                    case KeyType.SilverKey:
                        inventory.hasSilverKey = true;
                    break;
                    case KeyType.GoldKey:
                        inventory.hasGoldKey = true;
                    break;
                }
                
                Destroy(gameObject); // Remove a chave do mapa
            }
        }
    }
}
