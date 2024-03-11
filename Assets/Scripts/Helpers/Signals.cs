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

    public class SignalOnDeath : ISignalOnDeath
    {
        public int RoomIndex { get; }
        public UnitView View { get; }

        public SignalOnDeath(int roomIndex, UnitView view)
        {
            RoomIndex = roomIndex;
            View = view;
        }
    }
    public class SignalOnPlayerDeath : SignalOnDeath
    {
        public SignalOnPlayerDeath(int roomIndex, UnitView view) : base(roomIndex, view)
        {
        }
    }

    public class SignalOnEnemyDeath : SignalOnDeath
    {
        public SignalOnEnemyDeath(int roomIndex, UnitView view) : base(roomIndex, view)
        {
        }
    }

    public class SignalOnMobDeath : SignalOnDeath
    {
        public SignalOnMobDeath(int roomIndex, UnitView view) : base(roomIndex, view)
        {
        }
    }
    #endregion

    #region RoomReset
    public class SignalOnUnitReset : ISignalOnDeath
    {
        public int RoomIndex { get; }
        public UnitView View { get; }

        public SignalOnUnitReset(int roomIndex, UnitView view)
        {
            RoomIndex = roomIndex;
            View = view;
        }
    }

    public class SignalOnPlayerReset : SignalOnUnitReset
    {
        public SignalOnPlayerReset(int roomIndex, UnitView view) : base(roomIndex, view)
        {
        }
    }
    public class SignalOnEnemyReset : SignalOnUnitReset
    {
        public SignalOnEnemyReset(int roomIndex, UnitView view) : base(roomIndex, view)
        {
        }
    }
    public class SignalOnMobReset : SignalOnUnitReset
    {
        public SignalOnMobReset(int roomIndex, UnitView view) : base(roomIndex, view)
        {
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

    public class SignalOnDamage
    {
        public Team Team { get; }
        public UnitPresenter Target { get; }

        public SignalOnDamage(UnitPresenter presenter, Team team)
        {
            Target = presenter;
            Team = team;
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

    public class SignalOnRoomResetUnits
    {
        public int RoomIndex { get; }

        public SignalOnRoomResetUnits(int roomIndex)
        {
            RoomIndex = roomIndex;
        }
    }

    public class SignalOnTimeRoomReset
    {
        public int RoomIndex { get; }

        public SignalOnTimeRoomReset(int roomIndex)
        {
            RoomIndex = roomIndex;
        }
    }

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

    public class SignalOnObstacleTouch
    {
        public UnitPresenter Presenter { get; }

        public SignalOnObstacleTouch(UnitPresenter presenter)
        {
            Presenter = presenter;
        }
    }

    public class SignalOnMobActivate
    {
        public UnitPresenter Player { get; }
        public UnitPresenter Mob { get; }

        public SignalOnMobActivate(UnitPresenter player, UnitPresenter mob)
        {
            Player = player;
            Mob = mob;
        }
    }

    public class SignalOnRoomLoose
    {

    }

    public class SignalOnRoomWin
    {

    }
}
