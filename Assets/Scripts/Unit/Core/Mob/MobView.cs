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

    public void Respawn()
    {
        presenter.Respawn();
    }

    public override bool TryAddEffects(List<EffectSettings> effects, Team team)
    {
        return presenter.AddEffects(effects, team);
    }

    public override void Death()
    {
        OnDeath?.Invoke(this);
    }

    public override int GetID()
    {
        return (int)Type;
    }

    public class Factory : PlaceholderFactory<UnityEngine.Object, MobView>
    {
    }
}
