using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
	public CharacterStates CurrentState {get; private set;}
	public void Initialize(CharacterStates startingState)
	{
		CurrentState = startingState;
		CurrentState.Enter();
	}
	public void ChangeState(CharacterStates newState)
	{
		CurrentState.Exit();
		CurrentState = newState;
		CurrentState.Enter();
	}
}	