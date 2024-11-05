using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolveGameState : MiniGameState
{
    public ResolveGameState(MiniGameContext context, MiniGameStateMachine.EMiniGameState estate) : base(context, estate)
    {
        MiniGameContext Context = context;
    }

    public override void EnterState() {
        Debug.Log("Entering Resolve State");
    }
    public override void ExitState() {
        Debug.Log("Exiting Reslove State");
    }
    public override void UpdateState() { }
    public override MiniGameStateMachine.EMiniGameState GetNextState()
    {
        return MiniGameStateMachine.EMiniGameState.Macro; //Returns MacroState key (ResolveGameState --> MacroState)
        //return StateKey; //Returns same state (ResloveState --> ResloveState) use if needed
    }
    public override void OnTriggerEnter(Collider other) { }
    public override void OnTriggerStay(Collider other) { }
    public override void OnTriggerExit(Collider other) { }
}
