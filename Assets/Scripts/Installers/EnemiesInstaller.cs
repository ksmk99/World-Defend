using Helpers;
using System;
using Unit;
using Unit.Bullet;
using UnityEngine;
using Zenject;

public class EnemiesInstaller : MonoInstaller
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private SpawnerSettings spawnerSettings;
    [SerializeField] private Transform poolParent;

    public override void InstallBindings()
    {
        InstallEnemyFactory();
    }

    private void InstallEnemyFactory()
    {
        Container.Bind<CustomPool<EnemyView>>().AsSingle();
        Container.Bind<ISpawnManager>().To<SpawnManager>()
            .AsCached()
            .WithArguments(spawnerSettings)
            .WhenInjectedInto<EnemySpawner>();

        Container.BindFactory<UnityEngine.Object, EnemyView, EnemyView.Factory>()
            .FromFactory<PrefabFactory<EnemyView>>();

        Container.BindInterfacesAndSelfTo<EnemySpawner>()
            .AsSingle()
            .WithArguments(poolParent);
    }
}
