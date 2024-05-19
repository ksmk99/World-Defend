using GameplayState;
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
        stateMachine.TransitionTo(new MainMenuState());
    }
}

