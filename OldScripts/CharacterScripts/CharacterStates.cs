using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStates
{
	protected Character character;
	protected StateMachine stateMachine;
	protected CharacterData characterData;
	protected float startTime;
	private string animBoolName;
    public CharacterStates(Character character, StateMachine stateMachine, CharacterData characterData, string animBoolName)
    {
        this.character = character;
        this.stateMachine = stateMachine;
        this.characterData = characterData;
        this.animBoolName = animBoolName;
    }    
    public virtual void Enter()
    {
        DoChecks();

        character.Animator.SetBool(animBoolName, true);

        startTime = Time.time;
    }
    public virtual void Exit()
    {
        character.Animator.SetBool(animBoolName, false);
    }
    public virtual void LogicUpdate()
    {
        
    }
    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }
    public virtual void DoChecks()
    {
        
    }
}