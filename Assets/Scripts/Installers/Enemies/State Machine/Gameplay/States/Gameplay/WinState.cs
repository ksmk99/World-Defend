using System;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace GameplayState
{
    public class WinState : GameState
    {
        public override void Enter()
        {
            Time.timeScale = 0;
            SceneManager.LoadSceneAsync("Win Scene", LoadSceneMode.Additive);
        }

        public override void Exit()
        {
            Time.timeScale = 1f;
        }
    }
}
