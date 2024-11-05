using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameState : MiniGameState
{
    public EndGameState(MiniGameContext context, MiniGameStateMachine.EMiniGameState estate) : base(context, estate)
    {
        MiniGameContext Context = context;
    }

    public override void EnterState() {
        Debug.Log("Entering End Game State");
    }
    public override void ExitState() {
        Debug.Log("Exiting End Game State");
    }
    public override void UpdateState() { }
    public override MiniGameStateMachine.EMiniGameState GetNextState()
    {
        return MiniGameStateMachine.EMiniGameState.Resolve; //Returns ResolveState key (EndGameState --> ResolveState)
       // return StateKey; // Returns same state key (EndGameState --> EndGameState) Use if needed
    }
    public override void OnTriggerEnter(Collider other) { }
    public override void OnTriggerStay(Collider other) { }
    public override void OnTriggerExit(Collider other) { }
}
