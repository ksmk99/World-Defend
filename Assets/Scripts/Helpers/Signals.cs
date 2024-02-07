using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Helpers
{
    public class SignalOnProgressionChange
    {
        public int KillCount { get; }
        public int Percent { get; }

        public SignalOnProgressionChange(int killCount, int percent)
        {
            KillCount = killCount;
            Percent = percent;
        }
    }

    #region Death
    public interface ISignalOnDeath { }
    public class SignalOnPlayerDeath : ISignalOnDeath
    {
        public int RoomIndex { get; }

        public SignalOnPlayerDeath(int roomIndex)
        {
            RoomIndex = roomIndex;
        }
    }

    public class SignalOnEnemyDeath : ISignalOnDeath
    {
        public int RoomIndex { get; }

        public SignalOnEnemyDeath(int roomIndex)
        {
            RoomIndex = roomIndex;
        }
    }

    public class SignalOnUnitDeath
    {
        public int RoomIndex { get; }

        public SignalOnUnitDeath(int roomIndex)
        {
            RoomIndex = roomIndex;
        }
    }
    #endregion

    #region Health
    public interface ISignalOnHealthChange
    {
        public float Percent { get; }
        public float Health { get; }
    }

    public class SignalOnUnitDamage : ISignalOnHealthChange
    {
        public float Percent { get; }

        public float Health { get; }

        public SignalOnUnitDamage(float percent, float health)
        {
            Percent = percent;
            Health = health;
        }
    }

    public class SignalOnUnitHeal : ISignalOnHealthChange
    {
        public float Percent { get; }
        public float Health { get; }

        public SignalOnUnitHeal(float percent, float health)
        {
            Percent = percent;
            Health = health;
        }
    }
    #endregion

    public class SignalOnRoomReset
    {
        public int RoomIndex { get; }

        public SignalOnRoomReset(int roomIndex)
        {
            RoomIndex = roomIndex;
        }
    }

    public class SignalOnMove
    {
        public bool IsMoving { get; }
        public Transform Sender { get; }

        public SignalOnMove(bool isMoving, Transform sender)
        {
            IsMoving = isMoving;
            Sender = sender;
        }
    }

    public class SignalOnRoomLoose
    {

    }

    public class SignalOnRoomWin
    {

    }
}
