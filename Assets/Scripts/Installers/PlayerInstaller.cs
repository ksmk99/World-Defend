using Helpers;
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
    [SerializeField] private PoolParentFlag poolParentFlag;

    public override void InstallBindings()
    {
        Container.BindInstance(poolParentFlag);

        SignalBusInstaller.Install(Container);
        Container.DeclareSignalWithInterfaces<SignalOnUnitDamage>();
        Container.DeclareSignalWithInterfaces<SignalOnUnitHeal>();
        Container.DeclareSignal<SignalOnUnitDied>();
        Container.DeclareSignal<SignalOnMove>();
        Container.DeclareSignal<SignalOnAttack>();

        Container.Bind<InputService>().
            AsSingle().WithArguments(settings.Joystick);
        Container.Bind<HealthModel>()
            .AsTransient().WithArguments(healthSettings);

        Container.BindFactory<BulletRuntimeSettings, BulletView, BulletView.Factory>()
        .FromMonoPoolableMemoryPool(
             x => x.WithInitialSize(weaponSettings.BulletCount)
            .FromComponentInNewPrefab(weaponSettings.BulletPrefab));

        Container.Bind<IWeaponModel>()
            .To<WeaponModel>()
            .AsTransient()
            .WithArguments(weaponSettings);
        Container.Bind<IWeaponPresenter>()
            .To(weaponSettings.WeaponType)
            .AsSingle()
            .WhenInjectedInto<PlayerModel>();
        Container.Bind<IHealthPresenter>().To<HealthPresenter>().AsSingle().WhenInjectedInto<PlayerModel>();
        Container.Bind<IMovement>().To<PlayerMovement>()
            .AsTransient()
            .WithArguments(settings.Transform)
            .WhenInjectedInto<PlayerModel>();
        Container.BindInstance(settings.Transform).WhenInjectedInto<PlayerModel>();
        Container.Bind<IUnitModel>().To<PlayerModel>()
            .AsSingle()
            .WhenInjectedInto<PlayerPresenter>();
        Container.BindInterfacesAndSelfTo<PlayerPresenter>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerView>()
            .FromComponentInHierarchy()
            .AsSingle();
        Container.BindInterfacesAndSelfTo<HealthView>()
            .FromComponentInHierarchy()
            .AsTransient();

        Container.BindInterfacesAndSelfTo<AnimationData>()
            .AsSingle(); 
        Container.Bind<IAnimationsController>()
            .To<AnimationsController>()
            .AsSingle()
            .WithArguments(settings.Animator);
        Container.BindSignal<SignalOnMove>().ToMethod<IAnimationsController>(x => x.SetMovement).FromResolve();
        Container.BindSignal<SignalOnAttack>().ToMethod<IAnimationsController>(x => x.TriggerAttack).FromResolve();


        Container.BindSignal<SignalOnUnitDied>().ToMethod<PlayerPresenter>(x => x.OnDeath).FromResolve();
    }

    [Serializable]
    public class Settings
    {
        [field: SerializeField]
        public Transform Transform { get; private set; }
        [field: SerializeField]
        public Joystick Joystick { get; private set; }
        [field: SerializeField]
        public Animator Animator { get; private set; }
    }
}
