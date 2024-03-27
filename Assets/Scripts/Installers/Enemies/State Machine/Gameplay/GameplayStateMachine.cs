using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;

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
