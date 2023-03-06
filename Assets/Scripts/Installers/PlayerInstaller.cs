using Installers;
using System;
using Unit;
using Unit.Bullet;
using Unity.Burst.Intrinsics;
using UnityEngine;
using Zenject;
using static Zenject.CheatSheet;

public class PlayerInstaller : MonoInstaller
{
    [Header("Settings")]
    [SerializeField] private Settings settings;
    [SerializeField] private HealthSettings healthSettings;
    [SerializeField] private WeaponSettings weaponSettings;

    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        Container.DeclareSignalWithInterfaces<SignalOnUnitDamage>();
        Container.DeclareSignalWithInterfaces<SignalOnUnitHeal>();
        Container.DeclareSignal<SignalOnUnitDied>();

        Container.Bind<InputService>().
            AsSingle().WithArguments(settings.Joystick);
        Container.Bind<HealthModel>()
            .AsTransient().WithArguments(healthSettings);
        Container.Bind<IWeaponModel>().To<WeaponModel>()
            .AsTransient().WithArguments(Team.Ally, weaponSettings);

        Container.BindFactory<BulletRuntimeSettings, BulletView, BulletView.Factory>()
            .FromMonoPoolableMemoryPool(
                x => x.WithInitialSize(weaponSettings.BulletCount)
                .FromComponentInNewPrefab(weaponSettings.BulletPrefab)
                .UnderTransformGroup("Bullet Pool"));


        Container.BindInstance(settings.Transform).WhenInjectedInto<PlayerModel>();
        Container.Bind<IWeapon>().To(weaponSettings.WeaponType)
            .AsTransient()
            .WithArguments(settings.Transform)
            .WhenInjectedInto<PlayerModel>(); 
        Container.Bind<IHealthPresentor>().To<HealthPresentor>().AsTransient().WhenInjectedInto<PlayerModel>(); ;
        Container.Bind<IMovement>().To<PlayerMovement>()
            .AsTransient()
            .WithArguments(settings.Transform)
            .WhenInjectedInto<PlayerModel>(); 

        Container.Bind<IUnitModel>().To<PlayerModel>()
            .AsSingle()
            .WhenInjectedInto<PlayerPresentor>();
        Container.BindInterfacesAndSelfTo<PlayerPresentor>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerView>()
            .FromComponentInHierarchy()
            .AsSingle();
        Container.BindInterfacesAndSelfTo<HealthView>()
            .FromComponentInHierarchy()
            .AsTransient();
    }

    [Serializable]
    public class Settings
    {
        [field: SerializeField] 
        public Transform Transform { get; private set; }
        [field: SerializeField] 
        public Joystick Joystick { get; private set; }
    }
}
