using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class MiniGameStateMachine : StateMachine<MiniGameStateMachine.EMiniGameState>
{
    public enum EMiniGameState
    {
        Macro,
        Enter,
        Spawn,
        Start,
        Play,
        Pause,
        End,
        Resolve
    }

  

    // Create Sterialized Feilds for any object that will need to be used here

    //[SerializedField] private [itemtype] _[itemname];


    //Validation for assembalers (If needed)
    //-----------------------------------------------------------

    //V- runs item validation check and throws error if item is gone -V

    private MiniGameContext _context;

    private void Awake()
    {
    //    ItemValidation();

    //    _context = new MiniGameContext(_[itemname]) <-- Add all items here
        InitializeStates();
    }

    //V- Items added from the Sterialized Fields will be added here along with error messages -V

    //private void ItemValidation()
    //{
    //    Assert.IsNotNull(_[itemname], "This _gameitem is not assigned");
    //}
    //-----------------------------------------------------------

    private void InitializeStates()
    {
        //adds "states" to StateManager dictionary
        States.Add(EMiniGameState.Macro, new MacroState(_context, EMiniGameState.Macro));
        States.Add(EMiniGameState.Enter, new EnterGameState(_context, EMiniGameState.Enter));
        States.Add(EMiniGameState.Spawn, new SpawnState(_context, EMiniGameState.Spawn));
        States.Add(EMiniGameState.Start, new StartGameState(_context, EMiniGameState.Start));
        States.Add(EMiniGameState.Play, new PlayGameState(_context, EMiniGameState.Play));
        States.Add(EMiniGameState.Pause, new PauseState(_context, EMiniGameState.Pause));
        States.Add(EMiniGameState.End, new EndGameState(_context, EMiniGameState.End));
        States.Add(EMiniGameState.Resolve, new ResolveGameState(_context, EMiniGameState.Resolve));
        CurrentState = States[EMiniGameState.Macro];
    }

}
