using Helpers;
using System.Collections;
using System.Collections.Generic;
using Unit;
using UnityEngine;
using Zenject;

public class PlayerSpawnerInstaller : MonoInstaller
{
    [SerializeField] private PlayerSpawnerSettings playerSpawnerSettings;
    [SerializeField] private SpawnerSettings spawnerSettings;
    [SerializeField] private Transform poolParent;

    public override void InstallBindings()
    {
        InstallMobFactory();
    }

    private void InstallMobFactory()
    {
        Container.Bind<CustomPool<PlayerView>>().AsSingle();
        Container.Bind<ISpawnManager>().To<SpawnManager>()
            .AsCached()
            .WithArguments(spawnerSettings)
            .WhenInjectedInto<PlayerSpawner>();

        Container.BindFactory<UnityEngine.Object, PlayerView, PlayerView.Factory>()
            .FromFactory<PrefabFactory<PlayerView>>();

        Container.BindInterfacesAndSelfTo<PlayerSpawner>()
            .AsSingle()
            .WithArguments(playerSpawnerSettings, poolParent);
    }
}
