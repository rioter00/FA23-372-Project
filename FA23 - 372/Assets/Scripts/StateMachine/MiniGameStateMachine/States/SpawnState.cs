using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnState : MiniGameState
{
    public SpawnState(MiniGameContext context, MiniGameStateMachine.EMiniGameState estate) : base(context, estate)
    {
        MiniGameContext Context = context;
    }

    public override void EnterState() {
        Debug.Log("Entering Spawn State");
    }
    public override void ExitState() {
        Debug.Log("Exiting Spawn State");
    }
    public override void UpdateState() { }
    public override MiniGameStateMachine.EMiniGameState GetNextState()
    {
        return MiniGameStateMachine.EMiniGameState.Start; //Returns StartState key (SpawnState --> StartState)
        //return StateKey; //Returns same State (SpawnState --> SpawnState) Use if needed
    }
    public override void OnTriggerEnter(Collider other) { }
    public override void OnTriggerStay(Collider other) { }
    public override void OnTriggerExit(Collider other) { }
}
