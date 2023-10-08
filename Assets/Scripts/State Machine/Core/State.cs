namespace GameplayState
{
    public abstract class State : IState
    { 

        public abstract void Enter();

        public abstract void Exit();
    }
}