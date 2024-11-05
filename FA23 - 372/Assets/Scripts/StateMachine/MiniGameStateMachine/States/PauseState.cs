using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseState : MiniGameState
{
    public PauseState(MiniGameContext context, MiniGameStateMachine.EMiniGameState estate) : base(context, estate)
    {
        MiniGameContext Context = context;
    }

    public override void EnterState() {
        Debug.Log("Entering Pause State");
    }
    public override void ExitState() {
        Debug.Log("Exiting Pause State");
    }
    public override void UpdateState() { }
    public override MiniGameStateMachine.EMiniGameState GetNextState()
    {
        return MiniGameStateMachine.EMiniGameState.Play; //Returns PlayState Key (PauseState --> PlayState)
        return MiniGameStateMachine.EMiniGameState.Macro; //Returns MacroState Key (PauseState --> MacroState) This can be used to exit to level select
        //return StateKey; //Returns same State (PauseState --> Pause State) use while needed
    }
    public override void OnTriggerEnter(Collider other) { }
    public override void OnTriggerStay(Collider other) { }
    public override void OnTriggerExit(Collider other) { }
}
