using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace GameplayState
{
    public class MainMenuState : GameState
    {
        public override void Enter()
        {
            SceneManager.activeSceneChanged += LoadLevel;
            SceneManager.LoadSceneAsync("Main Menu");
        }

        private void LoadLevel(Scene arg0, Scene arg1)
        {
            stateMachine.TransitionTo(new StartState());
        }

        public override void Exit()
        {
            SceneManager.activeSceneChanged -= LoadLevel;
        }
    }
}
