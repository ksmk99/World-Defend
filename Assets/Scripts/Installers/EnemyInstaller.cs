using System;
using Unit;
using Unit.Bullet;
using UnityEngine;
using Zenject;

public class EnemyInstaller : MonoInstaller
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private HealthSettings healthSettings;
    [SerializeField] private WeaponSettings weaponSettings;
    [SerializeField] private MovementSettings enemyMovement;
    [SerializeField] private Transform bulletParent;

    public override void InstallBindings()
    {
        InstallEnemyFactory();
    }

    private void InstallEnemyFactory()
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

        subContainer.BindFactory<BulletRuntimeSettings, BulletView, BulletView.Factory>()
        .FromMonoPoolableMemoryPool(
             x => x.WithInitialSize(weaponSettings.BulletCount)
            .FromComponentInNewPrefab(weaponSettings.BulletPrefab)
            .UnderTransform(new GameObject("[Enemy Bullets]").transform));

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
}
