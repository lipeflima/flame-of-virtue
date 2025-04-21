using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInAir : CharacterStates
{
    public CharacterInAir (Character character, StateMachine stateMachine, CharacterData characterData, string animBoolName) : base (character, stateMachine, characterData, animBoolName){}
    public override void Enter()
    {
        base.Enter();        
    }
    public override void DoChecks()
    {
        base.DoChecks();
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(character.isGrounded)
        {
            stateMachine.ChangeState(character.idleState);
        }
        else
        {
            //character.SetVelocityY(characterData.gravity);
            character.CheckGravity();
        }
    }
}
