using Gameplay;
using Helpers;
using System;
using Unit;
using Unit.Bullet;
using UnityEngine;
using Zenject;

public class EnemiesInstaller : AUnitInstaller
{
    [SerializeField] private EnemySpawnerSettings enemiesSpawnerSettings;
    [SerializeField] private SpawnerSettings spawnerSettings;
    [SerializeField] private Transform poolParent;
    [SerializeField] private HealthParentFlag healthParentFlag;

    public override void InstallBindings()
    {
        InstallEnemyFactory();
    }

    private void InstallEnemyFactory()
    {
        Container.BindInstance(healthParentFlag);
        Container.Bind<CustomPool<EnemyView>>().AsSingle();
        Container.Bind<ISpawnManager>().To<SpawnManager>()
            .AsCached()
            .WithArguments(spawnerSettings)
            .WhenInjectedInto<EnemySpawner>();

        Container.BindFactory<UnityEngine.Object, EnemyView, EnemyView.Factory>()
            .FromFactory<PrefabFactory<EnemyView>>();

        Container.BindInterfacesAndSelfTo<EnemySpawner>()
            .AsSingle()
            .WithArguments(enemiesSpawnerSettings, poolParent, RoomIndex);

        Container
            .BindSignal<SignalOnRoomReset>()
            .ToMethod<EnemySpawner>(x => x.Reset)
            .FromResolve();

        Container
          .BindSignal<SignalOnEnemyDeath>()
          .ToMethod<EnemySpawner>(x => x.Release)
          .FromResolve();
        Container
            .BindSignal<SignalOnEnemyReset>()
            .ToMethod<EnemySpawner>(x => x.Release)
            .FromResolve();


        BindLevelProgression();
    }

    private void BindLevelProgression()
    {
        Container
            .Bind<RoomProgressionService>()
            .AsTransient()
            .WithArguments(enemiesSpawnerSettings.Count, RoomIndex);
        Container
            .BindSignal<SignalOnPlayerDeath>()
            .ToMethod<RoomProgressionService>(x => x.PlayerDeath)
            .FromResolve();
        Container
            .BindSignal<SignalOnEnemyDeath>()
            .ToMethod<RoomProgressionService>(x => x.EnemyDeath)
            .FromResolve();
    }
}
