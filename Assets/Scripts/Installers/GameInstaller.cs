using Installers;
using System;
using System.Collections;
using System.Collections.Generic;
using Unit;
using Unit.Bullet;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private HealthSettings healthSettings;
    [SerializeField] private WeaponSettings weaponSettings;
    [SerializeField] private MovementSettings enemyMovement;

    public override void InstallBindings()
    {
        Container.BindInstance(enemyMovement).WhenInjectedInto<EnemyMovement>();
        Container.BindFactory<EnemyView, EnemyView.Factory>()
            .FromPoolableMemoryPool(poolBinder => poolBinder
                .WithInitialSize(1)
                .FromSubContainerResolve()
                .ByNewPrefabMethod(enemy, InstallEnemy)
                .UnderTransformGroup("Enemy"));

        Container.BindInterfacesAndSelfTo<EnemySpawner>().AsSingle();
    }

    private void InstallEnemy(DiContainer subContainer)
    {
        subContainer.Bind<IMovement>().To<EnemyMovement>().AsSingle();

        subContainer.Bind<HealthModel>().AsSingle().WithArguments(healthSettings);
        subContainer.BindInterfacesAndSelfTo<HealthPresentor>().AsSingle();
        subContainer.BindInterfacesAndSelfTo<HealthView>().FromComponentInHierarchy()
            .AsTransient();

        subContainer.DeclareSignalWithInterfaces<SignalOnUnitDamage>();
        subContainer.DeclareSignalWithInterfaces<SignalOnUnitHeal>();
        subContainer.DeclareSignal<SignalOnUnitDied>();


        subContainer.Bind<EnemyModel>().AsSingle();
        subContainer.BindInterfacesAndSelfTo<EnemyPresentor>().AsSingle();
    }
}
