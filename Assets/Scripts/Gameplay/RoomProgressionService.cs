using GameplayState;
using Helpers;
using Unit;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class RoomProgressionService
    {
        private readonly EnemySpawnerSettings spawnerSettings;
        private readonly GameplayStateMachine stateMachine;
        private readonly SignalBus signalBus;

        private int killCount;
        private bool isFinish;

        public RoomProgressionService(GameplayStateMachine stateMachine, SignalBus signalBus, EnemySpawnerSettings spawnerSettings)
        {
            this.stateMachine = stateMachine;
            this.signalBus = signalBus;
            this.spawnerSettings = spawnerSettings;
        }

        public void EnemyDeath(SignalOnEnemyDeath signal)
        {
            if (isFinish)
            {
                return;
            }

            killCount++;

            SendProgressionSignal();
            CheckWinCondition();
        }

        private void CheckWinCondition()
        {
            if (killCount >= spawnerSettings.Count)
            {
                isFinish = true;

                //stateMachine.TransitionTo(new WinState());
            }
        }

        private void SendProgressionSignal()
        {
            var completePercent = killCount / spawnerSettings.Count;
            var progressionChangeSignal = new SignalOnProgressionChange(killCount, completePercent);
            signalBus.TryFire(progressionChangeSignal);
        }

        public void PlayerDeath(SignalOnPlayerDeath signal)
        {
            if (isFinish)
            {
                return;
            }

            isFinish = true;
            //stateMachine.TransitionTo(new LooseState());
        }
    }
}