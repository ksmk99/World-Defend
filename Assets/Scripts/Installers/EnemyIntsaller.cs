using System;
using Unit;
using Unit.Bullet;
using UnityEngine;
using Zenject;

public class EnemyIntsaller : MonoInstaller
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private HealthSettings healthSettings;
    [SerializeField] private WeaponSettings weaponSettings;
    [SerializeField] private MovementSettings enemyMovement;
    [SerializeField] private Transform bulletParent;

    public override void InstallBindings()
    {
        InstallEnemyFacotry();
    }

    private void InstallEnemyFacotry()
    {
        Container.BindInstance(enemyMovement).WhenInjectedInto<EnemyMovement>();
        Container.BindFactory<EnemyView, EnemyView.Factory>()
            .FromPoolableMemoryPool(poolBinder => poolBinder
                .WithInitialSize(1)
                .FromSubContainerResolve()
                .ByNewPrefabMethod(enemy, InstallEnemy)
                .UnderTransformGroup("Enemy"));

        Container.BindInterfacesAndSelfTo<EnemySpawner>().AsSingle();
    }

    private void InstallEnemy(DiContainer subContainer)
    {
        subContainer.Bind<IMovement>().To<EnemyMovement>().AsSingle();

        subContainer.Bind<HealthModel>().AsSingle().WithArguments(healthSettings);
        subContainer.BindInterfacesAndSelfTo<HealthPresentor>().AsSingle();
        subContainer.BindInterfacesAndSelfTo<HealthView>().FromComponentInHierarchy()
            .AsTransient();

        subContainer.DeclareSignalWithInterfaces<SignalOnUnitDamage>();
        subContainer.DeclareSignalWithInterfaces<SignalOnUnitHeal>();


        Container.BindMemoryPool<BulletView, BulletView.Pool>()
            .WithInitialSize(5)
            .FromSubContainerResolve()
            .ByNewPrefabMethod(weaponSettings.BulletSettings.Prefab, BulletBind)
            .UnderTransformGroup("PlayerBullet").AsTransient(); ;

        //Container.BindFactory<UnityEngine.Object, IBulletSettings, BulletRuntimeSettings, BulletView, BulletView.Factory>()
        //    .FromFactory<PrefabFactory<IBulletSettings, BulletRuntimeSettings, BulletView>>();

        subContainer.Bind<IWeaponModel>().To<WeaponModel>()
            .AsTransient()
            .WithArguments(weaponSettings);
        subContainer.Bind<IWeaponPresentor>()
            .To(weaponSettings.WeaponType)
            .AsTransient();


        subContainer.Bind<EnemyModel>().AsSingle();
        subContainer.BindInterfacesAndSelfTo<EnemyPresentor>().AsSingle();

        subContainer.BindSignal<SignalOnUnitDied>().ToMethod<EnemyPresentor>(x => x.OnDeath).FromResolve();
    }

    private void BulletBind(DiContainer container)
    {
        container.Bind<BulletModel>().AsSingle();
        container.Bind<BulletView>().AsSingle();
        container.Bind<BulletPresentor>().AsSingle();
    }
}
