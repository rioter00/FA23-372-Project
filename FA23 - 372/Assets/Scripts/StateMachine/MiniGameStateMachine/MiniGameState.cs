using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MiniGameState : BaseState<MiniGameStateMachine.EMiniGameState>
{
    protected MiniGameContext Context;

    public MiniGameState(MiniGameContext context, MiniGameStateMachine.EMiniGameState statekey) : base(statekey)
    {
        Context = context;
    }
}
