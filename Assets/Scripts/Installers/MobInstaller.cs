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

        //Container.BindFactory<UnityEngine.Object, IBulletSettings, BulletRuntimeSettings, BulletView, BulletView.Factory>()
        //    .FromFactory<PrefabFactory<IBulletSettings, BulletRuntimeSettings, BulletView>>();

        subContainer.Bind<IWeaponModel>().To<WeaponModel>()
            .AsTransient()
            .WithArguments(weaponSettings);
        subContainer.Bind<IWeaponPresentor>()
            .To(weaponSettings.WeaponType)
            .AsTransient();


        subContainer.Bind<MobModel>().AsSingle();
        subContainer.BindInterfacesAndSelfTo<MobPresentor>().AsSingle();

        subContainer.BindSignal<SignalOnUnitDied>().ToMethod<MobPresentor>(x => x.OnDeath).FromResolve();
    }
}
