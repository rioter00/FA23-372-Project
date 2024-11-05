using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine<EState> : MonoBehaviour where EState : Enum
{

    protected Dictionary<EState, BaseState<EState>> States = new Dictionary<EState, BaseState<EState>>();
    protected BaseState<EState> CurrentState;

    protected bool _IsTransistioningState = false;
    void Start() {
        CurrentState.EnterState();
    }

    void Update() {
        EState _nextStateKey = CurrentState.GetNextState();

        if (!_IsTransistioningState && _nextStateKey.Equals(CurrentState.StateKey)) {
            CurrentState.UpdateState();
        } else if (!_IsTransistioningState)
        {
            TransitionToState(_nextStateKey);
        }

        CurrentState.UpdateState();
    }

    public void TransitionToState(EState StateKey) {
       
        _IsTransistioningState = true;
        CurrentState.ExitState();
        CurrentState = States[StateKey];
        CurrentState.EnterState();
        _IsTransistioningState = false;
    }

    void OnTriggerEnter(Collider other) {
        CurrentState.OnTriggerEnter(other);
        
    }

    void OnTriggerStay(Collider other) {
        CurrentState.OnTriggerStay(other);
    }

    void OnTriggerExit(Collider other) {
        CurrentState.OnTriggerExit(other);
    }

}
