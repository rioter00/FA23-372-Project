using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGameState : MiniGameState
{
    public PlayGameState(MiniGameContext context, MiniGameStateMachine.EMiniGameState estate) : base(context, estate)
    {
        MiniGameContext Context = context;
    }

    public override void EnterState() {
        Debug.Log("Entering Play State");
    }
    public override void ExitState() {
        Debug.Log("Exiting Play State");
    }
    public override void UpdateState() { }
    public override MiniGameStateMachine.EMiniGameState GetNextState()
    {
        return MiniGameStateMachine.EMiniGameState.End; //Returns EndGameState key (PlayState --> EndGameState)
        return MiniGameStateMachine.EMiniGameState.Pause; //Returns PauseState key (PlayState --> PauseState)
        //return StateKey; //Returns same State (PlayState --> PlayState) use when needed
    }
    public override void OnTriggerEnter(Collider other) { }
    public override void OnTriggerStay(Collider other) { }
    public override void OnTriggerExit(Collider other) { }
}
