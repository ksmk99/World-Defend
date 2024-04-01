using Helpers;
using Unit;
using UnityEngine;
using Zenject;

public class PlayerSpawnerInstaller : AUnitInstaller
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
            .WithArguments(playerSpawnerSettings, poolParent, RoomIndex);


        Container
            .BindSignal<SignalOnRoomReset>()
            .ToMethod<PlayerSpawner>(x => x.Reset)
            .FromResolve();
        Container
            .BindSignal<SignalOnPlayerReset>()
            .ToMethod<PlayerSpawner>(x => x.Release)
            .FromResolve();


        Container
          .BindSignal<SignalOnPlayerDeath>()
          .ToMethod<PlayerSpawner>(x => x.Release)
          .FromResolve();
    }
}
