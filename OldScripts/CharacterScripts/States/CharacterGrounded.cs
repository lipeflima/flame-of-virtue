using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGrounded : CharacterStates
{
    protected Vector3 moveInput;
    protected float lightforceInput;
    protected bool interactInput, minningInput, dashInput, attackInput;
    protected bool lightStatus, laserStatus;

    public CharacterGrounded(Character character, StateMachine stateMachine, CharacterData characterData, string animBoolName)
        : base(character, stateMachine, characterData, animBoolName) { }

    public override void Enter()
    {
        base.Enter();
    }
    public override void DoChecks()
    {
        base.DoChecks();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(!character.isGrounded && character.CurrentVelocity.y < -8)
        {
            stateMachine.ChangeState(character.inAirState);            
        }
        else
        {
            HandleInputs();
            HandleGameStatus();
        }
    }

    private void HandleInputs()
    {
        var inputHandler = character.InputHandler;
        moveInput = inputHandler.MovementInput;
        minningInput = inputHandler.MinningInput;
        attackInput = inputHandler.LeftMouseInput;

        if(attackInput && character.inEncounterZone)
        {
            character.attacking = true;
        }
    }

    private void HandleGameStatus()
    {
        if (character.GameManager.gameStatus != GameManager.GameStatus.Normal)
        {
            stateMachine.ChangeState(character.idleState);
            return;
        }

        // Apenas continue para movimenta��o ou minera��o        
        HandleMovementState();
    }


    private void HandleMovementState()
    {
        if(!character.attacking)
        {
            if (minningInput)
            {
                if (character.inMineZone && character.hasToolEquiped)
                {
                    character.isMinning = true;
                    stateMachine.ChangeState(character.mineState);
                }
                else
                {
                    character.isMinning = false;
                    CheckMovement();
                }
            }
            else
            {
                character.isMinning = false;
                CheckMovement();
            }
        }
        else
        {
            stateMachine.ChangeState(character.attackState);
        }
    }
    private void CheckMovement()
    {
        if (moveInput == Vector3.zero)
        {
            stateMachine.ChangeState(character.idleState);
        }
        else
        {
            stateMachine.ChangeState(character.moveState);
        }
    }
}