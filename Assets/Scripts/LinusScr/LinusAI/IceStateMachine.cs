using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public class IceStateMachine : MonoBehaviour
{
    private Dictionary<Type, IceState> possibleStates;
    public event Action<IceState> OnStateChanged;
    public IceState CurrentState { get; private set; }


    public void SetStates(Dictionary<Type, IceState> states)
    {
        possibleStates = states;
    }

    private void Update()
    {
        if (CurrentState == null)
        {
            CurrentState = possibleStates.Values.First();
        }

        //This is what calls the executable part of the different states. 
        //Anything written in Tick() for the selected state will be executed here.
        //Tick() returns a type, which is then used to switch to the next state in line 34.

        var nextState = CurrentState?.Tick();

        if (nextState != null && nextState != CurrentState?.GetType())
        {
            CurrentState = possibleStates[nextState];
        }
    }
    private void SwapState(Type nextState)
    {
        CurrentState = possibleStates[nextState];
        OnStateChanged?.Invoke(CurrentState);
    }
    
}
