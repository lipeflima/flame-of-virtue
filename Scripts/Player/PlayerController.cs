using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _playerRigidbody2D;
    [SerializeField] private float _playerSpeed;
    private Vector2 _playerDirection;
    [SerializeField] private GameObject model;
    private EnergySystem energySystem;
    private Animator anim;
    public PlayerStates playerStates;
    public enum PlayerStates
    {
        Idle,
        Moving,
        Damaged,
        Dead
    }
    
    void Start()
    {
        _playerRigidbody2D = GetComponent<Rigidbody2D>(); 
        anim = model.GetComponent<Animator>();  
        energySystem = GetComponent<EnergySystem>();
        playerStates = PlayerStates.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();        
    }

    void FixedUpdate()
    {
        CheckStates();

        if(!energySystem.GetLifeStatus())
        {
            playerStates = PlayerStates.Dead;
        }
        else
        {
            if(energySystem.Get)
            {

            }
            else
            {
                if(_playerDirection != Vector2.zero)
                {
                    playerStates = PlayerStates.Moving;
                }
                else
                {
                    playerStates = PlayerStates.Idle;
                }
            }
        }
    }

    void AnimationSetup(bool animBoolName, bool status)
    {
        anim.SetBool(animBoolName, status);
    }   

    private void HandleInput()
    {
        _playerDirection.x = Input.GetAxisRaw("Horizontal");
        _playerDirection.y = Input.GetAxisRaw("Vertical");
    }

    private void CheckStates()
    {
        switch(playerStates)
        {
            case PlayerStates.Idle:
            AnimationSetup("Idle", true);
            AnimationSetup("Moving", false);
            break; 

            case PlayerStates.Moving:
            AnimationSetup("Idle", false);
            AnimationSetup("Moving", true);
            break; 
        }
    }

    private void SetMovement()
    {
        _playerRigidbody2D.MovePosition(_playerRigidbody2D.position + _playerSpeed * Time.fixedDeltaTime * _playerDirection.normalized);
    }
}
