﻿using System;
using System.Collections.Generic;
using Unit;
using Zenject;

public class PlayerView : UnitView
{
    public override Action<UnitView> OnDeath { get; set; }

    [Inject]
    public void Init(PlayerPresenter presenter)
    {
        this.presenter = presenter;
    }

    public override void Death()
    {
        return;
    }

    public override int GetPoolID()
    {
        return 0;
    }

    public class Factory : PlaceholderFactory<UnityEngine.Object, PlayerView>
    {
    }
}
