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

    public Action OnRespawn;
    public Func<UnitPresenter> OnPresenterCall;
    public Func<List<EffectSettings>, Team, bool> OnTryAddEffects;

    public void Respawn()
    {
        //presentor.Respawn();
        OnRespawn?.Invoke();
    }

    public override bool TryAddEffects(List<EffectSettings> effects, Team team)
    {
        //return presentor.AddEffects(effects, team);
        return OnTryAddEffects.Invoke(effects, team);
    }

    public override UnitPresenter GetPresenter()
    {
        return OnPresenterCall.Invoke();
    }

    public override Team GetTeam()
    {
        return Team.Enemy;
        var presenter = OnPresenterCall.Invoke();
        return presenter.Team;
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

    public class Factory : PlaceholderFactory<UnityEngine.Object, EnemyView>
    {
    }
}

public class CustomEnemyFactory : IFactory<EnemyView>
{
    private DiContainer container;
    private ISpawnManager spawnManager;


    public CustomEnemyFactory(DiContainer container, ISpawnManager spawnManager)
    {
        this.container = container;
        this.spawnManager = spawnManager;
    }

    public EnemyView Create()
    {
        var enemy = container.InstantiatePrefab(spawnManager.GetPrefab());
        return enemy.GetComponent<EnemyView>();
    }
}
