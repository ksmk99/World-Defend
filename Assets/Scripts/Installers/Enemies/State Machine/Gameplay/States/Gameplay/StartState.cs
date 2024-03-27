using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameplayState
{
    public class StartState : GameState
    {
        public override void Enter()
        {
            SceneManager.LoadSceneAsync("Main Scene");
            Time.timeScale = 1;
        }

        public override void Exit()
        {
            Time.timeScale = 1f;
        }
    }
}