using Helpers;
using System;
using Unit;
using Unit.Bullet;
using UnityEngine;
using Zenject;
using static Zenject.CheatSheet;

public class MobsInstaller : AUnitInstaller
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private MobSpawnerSettings mobSpawnerSettings;
    [SerializeField] private SpawnerSettings spawnerSettings;
    [SerializeField] private Transform poolParent;

    public override void InstallBindings()
    {
        InstallMobFactory();
    }

    private void InstallMobFactory()
    {
        Container.Bind<CustomPool<MobView>>().AsSingle();
        Container.Bind<ISpawnManager>().To<SpawnManager>()
            .AsCached()
            .WithArguments(spawnerSettings)
            .WhenInjectedInto<MobSpawner>();

        Container.BindFactory<UnityEngine.Object, MobView, MobView.Factory>()
            .FromFactory<PrefabFactory<MobView>>();

        Container.BindInterfacesAndSelfTo<MobSpawner>()
            .AsSingle()
            .WithArguments(mobSpawnerSettings, poolParent, RoomIndex);
    }
}
