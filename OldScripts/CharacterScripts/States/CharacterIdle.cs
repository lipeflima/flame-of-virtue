using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterIdle : CharacterGrounded
{
    public CharacterIdle (Character character, StateMachine stateMachine, CharacterData characterData, string animBoolName) : base (character, stateMachine, characterData, animBoolName){}
    public override void Enter()
    {
        base.Enter();
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        character.SetStop();
    }
}
