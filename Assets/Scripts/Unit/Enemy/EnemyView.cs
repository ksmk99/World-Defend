using System;
using System.Collections.Generic;
using Unit;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class EnemyView : UnitView, IPoolable<IMemoryPool>, IDisposable
{
    public NavMeshAgent Agent;

    private IMemoryPool pool;
    private EnemyPresentor presentor;

    [Inject]
    public void Init(EnemyPresentor presentor)
    {
        this.presentor = presentor;
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

    public class Factory : PlaceholderFactory<EnemyView>
    {
    }
}
