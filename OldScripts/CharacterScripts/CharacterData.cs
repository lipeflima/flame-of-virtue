using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newCharacterData", menuName = "Data/Character Data/Base Data")]

public class CharacterData : ScriptableObject
{
    [Header("Movement")]
    public float moveVelocity = 8;
    public float gravity = 9.8f;
    public float sprintMult = 1.5f;
    public float positionCheckRate = 0.5f;
    public float overWeightedFactor = 0.5f;
    public float staminaConsume = 1;
    public float staminaOverWeightedConsume = 2;
    public float hungerConsume = 1;
    public float hungerOverWeightedConsume = 2;

    [Header("Dash State")]
    public float dashCooldown = 0.5f;
    public float dashTime = 0.2f;
    public float dashVelocity = 0f;
    public float drag = 10f;
    public float dashEndYMultiplier = 0.2f;
    public float distBetweenAfterImages = 0.5f;
    public float recoverTime = 0.2f;
    public bool dashTriggerActivated = false;
    //public bool isDashing = false;
    public float maxMovementVelocity = 900;

    [Header("Health")]
    public float maxHealth = 100;
    public float recoverDmgTime = 0.25f;
    public float h_restoreRate = 0.5f;
    public float h_restoreAmount; 

    [Header("Collect")]
    public float checkRate = 0.1f;
    public float checkRadius = 1;
    public float atractionForce = 10;
    public float collectDistance = 1;
    public int collectLayer;

    [Header("Others")]
    public float interactionDistance = 0.25f;
    public float meleeAttackTime = 0.25f;
    public float raycastDistance = 1.2f;
    public float attackTime = 0.3f;
    public LayerMask groundLayer;
    public Vector3 raycastOffset = Vector3.zero;
}
