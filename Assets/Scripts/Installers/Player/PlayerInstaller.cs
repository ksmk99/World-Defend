using Unit.Bullet;
using Unit;
using UnityEngine;
using Zenject;
using Helpers;
using System;
using Services;

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

        Container.Bind<IInputService>()
            .To<MLInputService>()
            .AsSingle();
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

        BindAgent();

        Container.BindInterfacesAndSelfTo<AnimationData>()
            .AsSingle();
        Container.Bind<IAnimationsController>()
            .To<AnimationsController>()
            .AsSingle()
            .WithArguments(settings.Animator, settings.Transform);

        Container.BindSignal<SignalOnMove>().ToMethod<IAnimationsController>(x => x.SetMovement).FromResolve();
        Container.BindSignal<SignalOnAttack>().ToMethod<IAnimationsController>(x => x.TriggerAttack).FromResolve();


        Container
            .BindSignal<SignalOnPlayerDeath>()
            .ToMethod<PlayerPresenter>(x => x.Death)
            .FromResolve();
        Container
            .BindSignal<SignalOnRoomReset>()
            .ToMethod<PlayerPresenter>(x => x.Reset)
            .FromResolve();
    }

    private void BindAgent()
    {
        Container.BindInterfacesAndSelfTo<PlayerAgent>()
            .FromComponentInHierarchy()
            .AsSingle();
        Container
            .BindSignal<SignalOnRoomReset>()
            .ToMethod<PlayerAgent>(x => x.RoomReset)
            .FromResolve();

        Container
            .BindSignal<SignalOnEnemyDeath>()
            .ToMethod<PlayerAgent>(x => x.EnemyDeath)
            .FromResolve();

        Container
            .BindSignal<SignalOnDamage>()
            .ToMethod<PlayerAgent>(x => x.UnitDamage)
            .FromResolve();

        Container
            .BindSignal<SignalOnMobActivate>()
            .ToMethod<PlayerAgent>(x => x.MobActivate)
            .FromResolve();

        Container
            .BindSignal<SignalOnObstacleTouch>()
            .ToMethod<PlayerAgent>(x => x.TouchBorder)
            .FromResolve();
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

