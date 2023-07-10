using System;
using System.Collections;
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

    public override bool TryAddEffects(List<EffectSettings> effects, Team team)
    {
        return presentor.AddEffects(effects, team);
    }

    public void Dispose()
    {
        pool.Despawn(this);
    }

    public void OnDespawned()
    {
        pool = null;
    }

    public void OnSpawned(IMemoryPool pool)
    {
        this.pool = pool;
    }

    public class Factory : PlaceholderFactory<EnemyView>
    {
    }
}
