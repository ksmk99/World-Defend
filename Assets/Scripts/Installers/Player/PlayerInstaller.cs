using Helpers;
using Services;
using System;
using Unit;
using Unit.Bullet;
using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller<PlayerInstaller>
{
    [SerializeField] private int RoomIndex = 1;
    [Header("Settings")]
    [SerializeField] private Settings settings;
    [SerializeField] private MovementSettings movementSettings;
    [SerializeField] private HealthSettings healthSettings;
    [SerializeField] private EnemyDetectorData enemyDetectorData;
    [SerializeField] private AWeaponSettings weaponSettings;
    [SerializeField] private HealthView healthPrefab;
    [SerializeField] private Sprite healthBarIcon;

    public override void InstallBindings()
    {
        Container.Bind<HealthModel>()
            .AsTransient().WithArguments(healthSettings);

        BindWeapon();
        BindHealth();
        BindMovement();
        BindPlayer();
        BindAgent();
        BindAnimations();
        BindEnemyDetector();

        Container
            .BindSignal<SignalOnPlayerDeath>()
            .ToMethod<PlayerPresenter>(x => x.Death)
            .FromResolve();
        Container
            .BindSignal<SignalOnRoomResetUnits>()
            .ToMethod<PlayerPresenter>(x => x.Reset)
            .FromResolve();
    }

    private void BindWeapon()
    {
        Container.BindFactory<BulletRuntimeSettings, BulletView, BulletView.Factory>()
        .FromMonoPoolableMemoryPool(
             x => x.WithInitialSize(weaponSettings.BulletPrespawnCount)
            .FromComponentInNewPrefab(weaponSettings.BulletPrefab));

        Container.BindFactory<HitRuntimeSettings, HitView, HitView.Factory>()
            .FromMonoPoolableMemoryPool(
            x => x.WithInitialSize(weaponSettings.BulletPrespawnCount)
            .FromComponentInNewPrefab(weaponSettings.HitPrefab));

        Container.Bind<IWeaponModel>()
            .To<WeaponModel>()
            .AsTransient()
            .WithArguments(weaponSettings);
        Container.Bind<IWeaponPresenter>()
            .To(weaponSettings.WeaponType)
            .AsSingle();
    }

    private void BindHealth()
    {
        Container.Bind<HealthFollower>().AsSingle().WithArguments(settings.Transform);
        Container.BindInterfacesAndSelfTo<HealthPresenter>().AsSingle();
        Container.BindInterfacesAndSelfTo<HealthView>()
            .FromComponentInNewPrefab(healthPrefab)
            .AsSingle()
            .WithArguments(healthBarIcon, "Player");
    }

    private void BindPlayer()
    {
        Container.BindInstance(settings.Transform).WhenInjectedInto<PlayerModel>();
        Container.Bind<IUnitModel>().To<PlayerModel>()
            .AsSingle()
            .WhenInjectedInto<PlayerPresenter>();
        Container.BindInterfacesAndSelfTo<PlayerPresenter>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerView>()
            .FromComponentInHierarchy()
            .AsSingle();
    }

    private void BindMovement()
    {
        Container.Bind<IInputService>()
            .To<MLInputService>()
            .AsSingle();
        Container.Bind<IMovement>().To<PlayerMovement>()
            .AsTransient()
            .WithArguments(settings.Transform, movementSettings)
            .WhenInjectedInto<PlayerModel>();
    }

    private void BindAnimations()
    {
        Container.BindInterfacesAndSelfTo<AnimationData>()
            .AsSingle();
        Container.Bind<IAnimationsController>()
            .To<AnimationsController>()
            .AsSingle()
            .WithArguments(settings.Animator, settings.Transform);


        Container.BindSignal<SignalOnMove>().ToMethod<IAnimationsController>(x => x.SetMovement).FromResolve();
        Container.BindSignal<SignalOnAttack>().ToMethod<IAnimationsController>(x => x.TriggerAttack).FromResolve();
    }

    private void BindEnemyDetector()
    {
        Container.Bind<IEnemyDetectorModel>()
            .To<EnemyDetectorModel>()
            .AsSingle()
            .WithArguments(enemyDetectorData, transform);
        Container.Bind<EnemyDetectorView>()
            .FromComponentInHierarchy()
            .AsSingle();
        Container.BindInterfacesAndSelfTo<EnemyDetectorPresenter>()
            .AsSingle();
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
            .BindSignal<SignalOnRoomReset>()
            .ToMethod<PlayerAgent>(x => x.Begin)
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
        public Animator Animator { get; private set; }
    }
}

