using GameplayState;
using UnityEngine;
using Zenject;

public class Bootstrap : IInitializable
{
    private readonly GameplayStateMachine stateMachine;

    public Bootstrap(GameplayStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public void Initialize()
    {
        Debug.Log("Start");
        stateMachine.TransitionTo(new MainMenuState());
    }
}

