using System;
using Unit;
using Unit.Bullet;
using UnityEngine;
using Zenject;

public class EnemiesInstaller : MonoInstaller
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private SpawnerSettings spawnerSettings;

    public override void InstallBindings()
    {
        InstallEnemyFactory();
    }

    private void InstallEnemyFactory()
    {
        Container.Bind<ISpawnManager>().To<EnemySpawnManager>().AsSingle().WithArguments(spawnerSettings);
        Container.BindFactory<UnityEngine.Object, EnemyView, EnemyView.Factory>().FromFactory<PrefabFactory<EnemyView>>();

        Container.BindInterfacesAndSelfTo<EnemySpawner>().AsSingle();
    }
}
