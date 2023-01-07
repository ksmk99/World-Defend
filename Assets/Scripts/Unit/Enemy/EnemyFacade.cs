using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyFacade : MonoBehaviour, IPoolable<IMemoryPool>, IDisposable
{
    private IMemoryPool pool;
    private EnemyController enemyController;

    [Inject]
    public void Construct(EnemyController enemyController)
    {
        this.enemyController = enemyController;
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

    public class Factory : PlaceholderFactory<EnemyFacade>
    {
    }
}
