using Unit.Bullet;
using Unit;
using UnityEngine;
using Zenject;
using Helpers;
using System;

public class PlayerInstaller : MonoInstaller<PlayerInstaller>
{
    [SerializeField] private int RoomIndex = 1;
    [Header("Settings")]
    [SerializeField] private Settings settings;
    [SerializeField] private HealthSettings healthSettings;
    [SerializeField] private WeaponSettings weaponSettings;
    [SerializeField] private HealthView healthPrefab;
    [SerializeField] private Sprite healthBarIcon;

    public override void InstallBindings()
    {
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

        Container.Bind<HealthFollower>().AsSingle().WithArguments(settings.Transform);
        Container.BindInterfacesAndSelfTo<HealthPresenter>().AsSingle();
        Container.BindInterfacesAndSelfTo<HealthView>()
            .FromComponentInNewPrefab(healthPrefab)
            .AsSingle()
            .WithArguments(healthBarIcon);

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

        Container.BindInterfacesAndSelfTo<AnimationData>()
            .AsSingle();
        Container.Bind<IAnimationsController>()
            .To<AnimationsController>()
            .AsSingle()
            .WithArguments(settings.Animator, settings.Transform);
        Container.BindSignal<SignalOnMove>().ToMethod<IAnimationsController>(x => x.SetMovement).FromResolve();
        Container.BindSignal<SignalOnAttack>().ToMethod<IAnimationsController>(x => x.TriggerAttack).FromResolve();


        Container.BindSignal<SignalOnUnitDeath>().ToMethod<PlayerPresenter>(x => x.OnDeath).FromResolve();
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

