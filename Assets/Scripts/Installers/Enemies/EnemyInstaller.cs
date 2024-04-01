using Helpers;
using Unit;
using Unit.Bullet;
using UnityEngine;
using Zenject;

public class EnemyInstaller : MonoInstaller<EnemyInstaller>
{
    [SerializeField] private HealthSettings healthSettings;
    [SerializeField] private EnemyMovementSettings enemyMovement;
    [SerializeField] private AWeaponSettings weaponSettings;
    [Space]
    [SerializeField] private HealthView healthPrefab;
    [SerializeField] private Sprite healthBarIcon;
    [Space]
    [SerializeField] private Animator animator;
    [SerializeField] private Transform unit;

    public override void InstallBindings()
    {
        Container.Bind<EnemyView>().FromComponentOnRoot().AsSingle();

        BindMovement();
        BindHealth();
        BindWeapon();
        BindAnimator();

        Container.Bind<EnemyModel>().AsSingle();
        Container.BindInterfacesAndSelfTo<EnemyPresenter>().AsSingle();

        BindSignals();
    }

    private void BindMovement()
    {
        Container.Bind<Transform>().FromComponentOnRoot().AsSingle();
        Container.Bind<IMovement>().To<EnemyMovement>().AsSingle();

        Container.BindInstance(enemyMovement).WhenInjectedInto<EnemyMovement>();
    }

    private void BindAnimator()
    {
        Container.BindInterfacesAndSelfTo<AnimationData>()
            .AsSingle();
        Container.Bind<IAnimationsController>()
            .To<AnimationsController>()
            .AsSingle()
            .WithArguments(animator, unit);

        Container.BindSignal<SignalOnMove>().ToMethod<IAnimationsController>(x => x.SetMovement).FromResolve();
        Container.BindSignal<SignalOnAttack>().ToMethod<IAnimationsController>(x => x.TriggerAttack).FromResolve();
    }

    private void BindSignals()
    {
        Container.DeclareSignalWithInterfaces<SignalOnUnitDamage>();
        Container.DeclareSignalWithInterfaces<SignalOnUnitHeal>();

        Container.BindSignal<SignalOnRoomResetUnits>()
            .ToMethod<EnemyPresenter>(x => x.Reset)
            .FromResolve();
    }

    private void BindHealth()
    {
        Container.Bind<HealthModel>().AsTransient().WithArguments(healthSettings);
        Container.Bind<HealthFollower>().AsSingle();
        Container.BindInterfacesAndSelfTo<HealthPresenter>().AsSingle();
        Container.BindInterfacesAndSelfTo<HealthView>()
            .FromComponentInNewPrefab(healthPrefab)
            .AsSingle()
            .WithArguments(healthBarIcon, "");
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

        Container.Bind<IWeaponModel>().To<WeaponModel>()
        .AsSingle()
            .WithArguments(weaponSettings);
        Container.Bind<IWeaponPresenter>()
            .To(weaponSettings.WeaponType)
            .AsSingle()
            .WhenInjectedInto<EnemyModel>();
    }
}

