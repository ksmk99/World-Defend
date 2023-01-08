using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class EnemyView : MonoBehaviour, IPoolable<IMemoryPool>, IDisposable
{
    public NavMeshAgent Agent;

    private IMemoryPool pool;
    private EnemyPresentor enemyController;

    [Inject]
    public void Construct(EnemyPresentor enemyController)
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

    public class Factory : PlaceholderFactory<EnemyView>
    {
    }
}
