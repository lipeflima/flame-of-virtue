using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAbility : CharacterStates
{
    protected Vector2 moveInput;
    protected Vector2 dashDirectionInput;
    protected bool isAbilityDone;
    protected bool isExitingState;
    bool isGrounded;
    public CharacterAbility(Character character, StateMachine stateMachine, CharacterData characterData, string animBoolName) : base(character, stateMachine, characterData, animBoolName) { }
    public override void Enter()
    {
        base.Enter();

        isAbilityDone = false;
        isExitingState = false;
    }
    public override void Exit()
    {
        base.Exit();

        isExitingState = true;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        moveInput = character.InputHandler.MovementInput;
        //dashDirectionInput = character.InputHandler.DashDirectionInput;

        if (isAbilityDone)
        {
            if(!isGrounded && character.CurrentVelocity.y < -8)
            {
                stateMachine.ChangeState(character.inAirState);                
            }
            else
            {
                if ((moveInput.x == 0) && (moveInput.y == 0))
                {
                    stateMachine.ChangeState(character.idleState);
                }
                else
                {
                    stateMachine.ChangeState(character.moveState);
                }
            }
        }
    }
}
