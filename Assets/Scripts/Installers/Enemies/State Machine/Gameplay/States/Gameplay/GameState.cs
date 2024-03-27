using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
