using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unit;
using UnityEngine;
using Zenject;

public enum MobType
{
    None,
    Default,
    Big
}

public class MobView : UnitView
{
    public MobType Type;

    public override Action<UnitView> OnDeath { get; set; }

    [Inject]
    public void Init(MobPresenter presenter)
    {
        this.presenter = presenter;
    }

    public override void Death()
    {
        OnDeath?.Invoke(this);
    }

    public override int GetPoolID()
    {
        return (int)Type;
    }

    public class Factory : PlaceholderFactory<UnityEngine.Object, MobView>
    {
    }
}
