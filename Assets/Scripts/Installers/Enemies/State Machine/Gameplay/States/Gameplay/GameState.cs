namespace GameplayState
{
    public abstract class GameState : State
    {
        protected GameplayStateMachine stateMachine;

        public void SetStateMachine(GameplayStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }
    }
}
