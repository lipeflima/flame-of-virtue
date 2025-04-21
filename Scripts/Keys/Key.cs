using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private KeyType keyType;
    private enum KeyType { RustedKey, GoldKey, SilverKey }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();
            if (inventory != null)
            {
                inventory.hasKey = true;
                Destroy(gameObject); // Remove a chave do mapa
            }
        }
    }
}
