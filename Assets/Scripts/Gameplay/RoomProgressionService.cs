using GameplayState;
using Helpers;
using System.Threading.Tasks;
using Zenject;

namespace Gameplay
{
    public class RoomProgressionService
    {
        private readonly GameplayStateMachine stateMachine;
        private readonly SignalBus signalBus;

        private readonly int spawnCount;
        private readonly int roomIndex;

        private int killCount;
        private bool isFinish;

        public RoomProgressionService(GameplayStateMachine stateMachine, SignalBus signalBus, int spawnCount, int roomIndex)
        {
            this.stateMachine = stateMachine;
            this.signalBus = signalBus;
            this.spawnCount = spawnCount;
            this.roomIndex = roomIndex;
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
            if (killCount >= spawnCount)
            {
                isFinish = true;
                RefreshRoom();
                //stateMachine.TransitionTo(new WinState());
            }
        }

        private async void RefreshRoom()
        {

            signalBus.TryFire(new SignalOnRoomResetUnits(roomIndex));
            await Task.Delay(5);
            signalBus.TryFire(new SignalOnRoomReset(roomIndex));


            killCount = 0;
            isFinish = false;
        }

        private void SendProgressionSignal()
        {
            var completePercent = killCount / spawnCount;
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
            RefreshRoom();
        }

        public void TimeReset(SignalOnTimeRoomReset signal)
        {
            if (isFinish)
            {
                return;
            }

            isFinish = true;
            RefreshRoom();
        }
    }
}