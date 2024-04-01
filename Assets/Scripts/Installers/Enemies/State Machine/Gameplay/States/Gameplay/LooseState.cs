﻿using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameplayState
{
    public class LooseState : GameState
    {
        public override void Enter()
        {
            Time.timeScale = 0;
            SceneManager.LoadSceneAsync("Loose Scene", LoadSceneMode.Additive);
        }

        public override void Exit()
        {
            Time.timeScale = 1f;
        }
    }
}