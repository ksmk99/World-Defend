using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unit;
using UnityEngine;
using Zenject;

public class MobView : UnitView, IPoolable<IMemoryPool>, IDisposable
{
    private MobPresenter mobPresenter;
    private IMemoryPool pool;

    [Inject]
    public void Init(MobPresenter presenter)
    {
        this.presenter = presenter;
        mobPresenter = presenter;
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
        transform.localScale = Vector3.zero;
        Dispose();
    }

    public void Activate(PlayerView player)
    {
        var playerPresenter = player.GetPresenter();
        mobPresenter.SetPlayer(playerPresenter);
    }

    public void Dispose()
    {
        if (pool == null)
        {
            return;
        }

        pool.Despawn(this);
    }

    public void OnDespawned()
    {
        pool = null;
    }

    public void OnSpawned(IMemoryPool pool)
    {
        this.pool = pool;
        transform.localScale = Vector3.one;
    }

    public class Factory : PlaceholderFactory<MobView>
    {
    }
}
