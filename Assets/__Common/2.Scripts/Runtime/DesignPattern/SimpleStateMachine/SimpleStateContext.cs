using System;
using UnityEngine;

[Serializable]
public abstract class SimpleStateContext<TController>
{
    // # Event
    
    public delegate void StateChangedDelegate(ISimpleState<TController> prevState, ISimpleState<TController> currentState);
    
    public event StateChangedDelegate OnStateChanged;
    
    // # Current State
    
    private ISimpleState<TController> _currentState;
    
    public ISimpleState<TController> CurrentState
    {
        get => _currentState;
        set
        {
            PrevState = _currentState;
            PrevState?.OnExit(Controller);
            
            _currentState = value;
            _currentState?.OnEnter(Controller);
            
            OnStateChanged?.Invoke(PrevState, _currentState);
        }
    }
    
    // # Prev State
    
    public ISimpleState<TController> PrevState { get; private set; }
    
    // # Init
    
    public TController Controller { get; }

    protected SimpleStateContext(TController controller)
    {
        Controller = controller;
    }

    // # Transition
    
    public void Transition(ISimpleState<TController> state)
    {
        CurrentState = state;
    }
}