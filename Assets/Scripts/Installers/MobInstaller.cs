using System;
using Unit;
using Unit.Bullet;
using UnityEngine;
using Zenject;

public class MobInstaller : MonoInstaller
{
    [SerializeField] private GameObject mob;
    [SerializeField] private HealthSettings healthSettings;
    [SerializeField] private WeaponSettings weaponSettings;
    [SerializeField] private MovementSettings movement;
    [SerializeField] private Transform bulletParent;

    public override void InstallBindings()
    {
        InstallMobFacotry();
    }

    private void InstallMobFacotry()
    {
        Container.BindInstance(movement).WhenInjectedInto<MobMovement>();
        Container.BindFactory<MobView, MobView.Factory>()
            .FromPoolableMemoryPool(poolBinder => poolBinder
                .WithInitialSize(1)
                .FromSubContainerResolve()
                .ByNewPrefabMethod(mob, InstallMob)
                .UnderTransformGroup("Mobs"));

        Container.BindInterfacesAndSelfTo<MobSpawner>().AsSingle();
    }

    private void InstallMob(DiContainer subContainer)
    {
        subContainer.Bind<IMovement>().To<MobMovement>().AsSingle();

        subContainer.Bind<HealthModel>().AsSingle().WithArguments(healthSettings);
        subContainer.BindInterfacesAndSelfTo<HealthPresentor>().AsSingle();
        subContainer.BindInterfacesAndSelfTo<HealthView>().FromComponentInHierarchy()
            .AsTransient();

        subContainer.DeclareSignalWithInterfaces<SignalOnUnitDamage>();
        subContainer.DeclareSignalWithInterfaces<SignalOnUnitHeal>();

        subContainer.Bind<IWeaponModel>().To<WeaponModel>()
            .AsTransient()
            .WithArguments(weaponSettings);
        subContainer.Bind<IWeaponPresentor>()
            .To(weaponSettings.WeaponType)
            .AsTransient();


        subContainer.Bind<MobModel>().AsSingle();
        subContainer.BindInterfacesAndSelfTo<MobPresentor>().AsSingle();

        subContainer.BindSignal<SignalOnUnitDied>().ToMethod<MobPresentor>(x => x.OnDeath).FromResolve();

        subContainer.Bind<BulletModel>().AsTransient().WithArguments(weaponSettings.BulletSettings);
        subContainer.Bind<BulletPresenter>().AsTransient();
        subContainer.Bind<BulletView>().AsTransient().WhenInjectedInto<BulletPresenter>();
        subContainer.Bind<ITickable>().To<BulletPresenter>().AsTransient();

        subContainer.BindFactory<BulletRuntimeSettings, BulletView, BulletView.Factory>()
            .FromMonoPoolableMemoryPool(x =>
             x.WithInitialSize(5)
            .FromComponentInNewPrefab(weaponSettings.BulletSettings.Prefab)
            .UnderTransformGroup("PlayerBullet"));
    }

    private void BulletBind(DiContainer container)
    {
        container.Bind<BulletModel>().AsTransient().WithArguments(weaponSettings.BulletSettings);
        //container.Bind<BulletView>().AsTransient();
        container.Bind<BulletPresenter>().AsTransient();
    }
}
