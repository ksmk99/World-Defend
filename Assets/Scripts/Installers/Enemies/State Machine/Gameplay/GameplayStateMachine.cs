namespace GameplayState
{
    public class GameplayStateMachine : StateMachine<GameState>
    {
        public override void TransitionTo(GameState state)
        {
            base.TransitionTo(state);

            state.SetStateMachine(this);
        }
    }
}
