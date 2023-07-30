using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unit;
using UnityEngine;
using Zenject;

public class MobView : UnitView, IPoolable<IMemoryPool>, IDisposable
{
    private MobPresentor mobPresentor;
    private IMemoryPool pool;

    [Inject]
    public void Init(MobPresentor presentor)
    {
        this.presentor = presentor;
        mobPresentor = presentor;
    }

    public void Respawn()
    {
        presentor.Respawn();
    }

    public override bool TryAddEffects(List<EffectSettings> effects, Team team)
    {
        return presentor.AddEffects(effects, team);
    }

    public override void Death()
    {
        transform.localScale = Vector3.zero;
        Dispose();
    }

    public void Activate(PlayerView player)
    {
        var playerPresentor = player.GetPresentor();
        mobPresentor.SetPlayer(playerPresentor);
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
