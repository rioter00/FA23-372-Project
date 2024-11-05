using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameState : MiniGameState
{
    public StartGameState(MiniGameContext context, MiniGameStateMachine.EMiniGameState estate) : base(context,estate)
    {
        MiniGameContext Context = context;
    }

    public override void EnterState() { }
    public override void ExitState() { }
    public override void UpdateState() { }
    public override MiniGameStateMachine.EMiniGameState GetNextState()
    {
        return MiniGameStateMachine.EMiniGameState.Play; //Returns PlayState key (StateGameState --> PlayState)
        //return StateKey; //Returns same state (StartGameState --> StartGameState) Use if needed
    }
    public override void OnTriggerEnter(Collider other) { }
    public override void OnTriggerStay(Collider other) { }
    public override void OnTriggerExit(Collider other) { }
}
