using Helpers;
using Unit;
using Unit.Bullet;
using UnityEngine;
using Zenject;

public class MobInstaller : MonoInstaller<MobInstaller>
{
    [SerializeField] private int RoomIndex = 1;
    [Header("Settings")]
    [SerializeField] private MobMovementSettings movement;
    [SerializeField] private HealthView healthPrefab;
    [SerializeField] private HealthViewData healthData;
    [SerializeField] private HealthSettings healthSettings;
    [Space]
    [SerializeField] private AWeaponSettings weaponSettings;
    [SerializeField] private Animator animator;

    public override void InstallBindings()
    {
        Container.BindInstance(movement);
        Container.Bind<Transform>().FromComponentOnRoot().AsSingle();
        Container.Bind<IMovement>().To<MobMovement>().AsSingle();

        Container.Bind<HealthFollower>().AsSingle();
        Container.Bind<HealthModel>().AsTransient().WithArguments(healthSettings);
        Container.BindInterfacesAndSelfTo<HealthPresenter>().AsSingle();
        Container.BindInterfacesAndSelfTo<HealthView>()
            .FromComponentInNewPrefab(healthPrefab)
            .AsSingle()
            .WithArguments(healthData);

        Container.DeclareSignalWithInterfaces<SignalOnUnitDamage>();
        Container.DeclareSignalWithInterfaces<SignalOnUnitHeal>();

        Container.BindFactory<BulletRuntimeSettings, BulletView, BulletView.Factory>()
        .FromMonoPoolableMemoryPool(
        x => x.WithInitialSize(weaponSettings.BulletPrespawnCount)
            .FromComponentInNewPrefab(weaponSettings.BulletPrefab));

        Container.BindFactory<HitRuntimeSettings, HitView, HitView.Factory>()
    .FromMonoPoolableMemoryPool(
    x => x.WithInitialSize(weaponSettings.BulletPrespawnCount)
    .FromComponentInNewPrefab(weaponSettings.HitPrefab));

        Container.Bind<IWeaponModel>().To<WeaponModel>()
            .AsTransient()
            .WithArguments(weaponSettings);
        Container.Bind<IWeaponPresenter>()
            .To(weaponSettings.WeaponType)
            .AsTransient();

        Container.Bind<MobView>().FromComponentOnRoot().AsSingle();
        Container.Bind<MobModel>().AsSingle();
        Container.BindInterfacesAndSelfTo<MobPresenter>().AsSingle();

        BindAnimator();

        Container
            .BindSignal<SignalOnRoomResetUnits>()
            .ToMethod<MobPresenter>(x => x.Reset)
            .FromResolve();
    }

    private void BindAnimator()
    {
        Container.BindInterfacesAndSelfTo<AnimationData>()
            .AsSingle();
        Container.Bind<IAnimationsController>()
            .To<AnimationsController>()
            .AsSingle()
            .WithArguments(animator, transform);

        Container.BindSignal<SignalOnMove>().ToMethod<IAnimationsController>(x => x.SetMovement).FromResolve();
        Container.BindSignal<SignalOnAttack>().ToMethod<IAnimationsController>(x => x.TriggerAttack).FromResolve();
    }
}

