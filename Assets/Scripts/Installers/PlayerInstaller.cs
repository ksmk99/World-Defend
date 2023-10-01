using System;
using Unit;
using Unit.Bullet;
using UnityEngine;
using Zenject;

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

        Container.BindFactory<BulletRuntimeSettings, BulletView, BulletView.Factory>()
        .FromMonoPoolableMemoryPool(
             x => x.WithInitialSize(weaponSettings.BulletCount)
            .FromComponentInNewPrefab(weaponSettings.BulletPrefab)
            .UnderTransformGroup("Bullet Pool"));

        Container.Bind<IWeaponModel>()
            .To<WeaponModel>()
            .AsTransient()
            .WithArguments(weaponSettings);
        Container.Bind<IWeaponPresentor>()
            .To(weaponSettings.WeaponType)
            .AsSingle()
            .WhenInjectedInto<PlayerModel>();
        Container.Bind<IHealthPresentor>().To<HealthPresenter>().AsSingle().WhenInjectedInto<PlayerModel>();
        Container.Bind<IMovement>().To<PlayerMovement>()
            .AsTransient()
            .WithArguments(settings.Transform)
            .WhenInjectedInto<PlayerModel>();
        Container.BindInstance(settings.Transform).WhenInjectedInto<PlayerModel>();
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

        Container.BindSignal<SignalOnUnitDied>().ToMethod<PlayerPresentor>(x => x.OnDeath).FromResolve();
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
