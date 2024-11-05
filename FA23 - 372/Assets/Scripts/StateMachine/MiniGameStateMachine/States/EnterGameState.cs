using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterGameState : MiniGameState
{
    public EnterGameState(MiniGameContext context, MiniGameStateMachine.EMiniGameState estate) : base(context, estate)
    {
        MiniGameContext Context = context;
    }

    public override void EnterState() {
        Debug.Log("Entering Enter Game State");
    }
    public override void ExitState() {
        Debug.Log("Exiting Enter Game State");
    }
    public override void UpdateState() { }
    public override MiniGameStateMachine.EMiniGameState GetNextState()
    {
        return MiniGameStateMachine.EMiniGameState.Spawn; // Returns SpawnState Key (EnterGameState --> SpawnState)
        //return StateKey; // Returns EnterState (EnterGameState --> EnterGameState) use if needed
    }
    public override void OnTriggerEnter(Collider other) { }
    public override void OnTriggerStay(Collider other) { }
    public override void OnTriggerExit(Collider other) { }
}