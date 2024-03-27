using System;

namespace GameplayState
{
    public abstract class StateMachine<T> 
        where T : State
    {
        private T state = null;

        public virtual void TransitionTo(T state)
        {
            if (this.state != null && this.state.Equals(state))
            {
                return;
            }

            if (this.state != null)
            {
                this.state.Exit();
            }

            this.state = state;
            this.state.Enter();
        }
    }
}