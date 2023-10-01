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

    public Action OnRespawn;
    public Action<EnemyView> OnDeath;
    public Func<UnitPresenter> OnPresenterCall;
    public Func<List<EffectSettings>, Team, bool> OnTryAddEffects;

    public void Respawn()
    {
        OnRespawn?.Invoke();
    }

    public override bool TryAddEffects(List<EffectSettings> effects, Team team)
    {
        return OnTryAddEffects.Invoke(effects, team);
    }

    public override UnitPresenter GetPresenter()
    {
        return OnPresenterCall.Invoke();
    }

    public override Team GetTeam()
    {
        var presenter = OnPresenterCall.Invoke();
        return presenter.Team;
    }

    public override void Death()
    {
        OnDeath?.Invoke(this);
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
