﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Helpers
{
    #region Death
    public interface ISignalOnDeath { }
    public class SignalOnPlayerDeath : ISignalOnDeath { }
    public class SignalOnEnemyDeath : ISignalOnDeath { }

    public class SignalOnUnitDeath
    {
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
}
