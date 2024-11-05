using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MacroState : MiniGameState
{
    public MacroState(MiniGameContext context, MiniGameStateMachine.EMiniGameState estate) : base(context, estate)
    {
        MiniGameContext Context = context;
    }

    public override void EnterState() {
        Debug.Log("Entering Macro State");
    }
    public override void ExitState() {
        Debug.Log("Exiting Macro State");
    }
    public override void UpdateState() { }
    public override MiniGameStateMachine.EMiniGameState GetNextState()
    {
        return MiniGameStateMachine.EMiniGameState.Enter; //Returns EnterState key (MacroState --> EnterGameState)
        //return StateKey; //Returns same State (MacroState --> MacroState) Use if needed
    }
    public override void OnTriggerEnter(Collider other) { }
    public override void OnTriggerStay(Collider other) { }
    public override void OnTriggerExit(Collider other) { }
}
