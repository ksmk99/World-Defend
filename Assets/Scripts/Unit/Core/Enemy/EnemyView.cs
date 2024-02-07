using System;
using System.Collections.Generic;
using Unit;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class EnemyView : UnitView
{
    public NavMeshAgent Agent;
    public EnemyType Type;

    public override Action<UnitView> OnDeath { get; set; }

    public Action OnRespawn;

    [Inject]
    public void Init(EnemyPresenter presenter)
    {
        this.presenter = presenter;
    }

    public void Respawn()
    {
        OnRespawn?.Invoke();
    }

    public override void Death()
    {  
        OnDeath?.Invoke(this);
        gameObject.SetActive(false);
    }

    public override UnitPresenter GetPresenter()
    {
        return presenter;
    }

    public override int GetPoolID()
    {
        return (int)Type;
    }

    public class Factory : PlaceholderFactory<UnityEngine.Object, EnemyView>
    {
    }
}

public enum EnemyType
{
    None,
    Default,
    Big
}
//public class CustomEnemyFactory : IFactory<EnemyView>
//{
//    private DiContainer container;
//    private ISpawnManager spawnManager;


//    public CustomEnemyFactory(DiContainer container, ISpawnManager spawnManager)
//    {
//        this.container = container;
//        this.spawnManager = spawnManager;
//    }

//    public EnemyView Create()
//    {
//        var enemy = container.InstantiatePrefab(spawnManager.GetPrefab());
//        return enemy.GetComponent<EnemyView>();
//    }
//}
