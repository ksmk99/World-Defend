using Helpers;
using System;
using System.Collections.Generic;
using Unit;
using UnityEngine;

public abstract class UnitView : MonoBehaviour, IRoomResettable
{
    protected UnitPresenter presenter;

    public abstract int GetPoolID();
    public abstract void Death();

    public virtual UnitPresenter GetPresenter()
    {
        return presenter;
    }

    public virtual void Activate(UnitView player)
    {
        var playerPresenter = player.GetPresenter();
        presenter.SetPlayer(playerPresenter);
    }

    public void Reset(SignalOnRoomReset signal)
    {
        if (presenter.RoomIndex == signal.RoomIndex)
        {

        }
    }
}
