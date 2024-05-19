using Helpers;
using Unit;
using UnityEngine;
using Zenject;

public class MobsInstaller : AUnitInstaller
{
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

        Container
            .BindSignal<SignalOnRoomReset>()
            .ToMethod<MobSpawner>(x => x.Reset)
            .FromResolve();

        Container
          .BindSignal<SignalOnMobDeath>()
          .ToMethod<MobSpawner>(x => x.Release)
          .FromResolve();
        Container
            .BindSignal<SignalOnMobReset>()
            .ToMethod<MobSpawner>(x => x.Release)
            .FromResolve();
    }
}
