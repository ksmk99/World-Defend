using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit.Bullet;
using Unit;
using UnityEngine;
using Zenject;


public class EnemyInstaller : MonoInstaller<EnemyInstaller>
{
    [SerializeField] private HealthSettings healthSettings;
    [SerializeField] private WeaponSettings weaponSettings;
    [SerializeField] private MovementSettings enemyMovement;

    public override void InstallBindings()
    {
        Container.Bind<EnemyView>().FromComponentOnRoot().AsSingle();

        Container.Bind<Transform>().FromComponentOnRoot().AsSingle().WhenInjectedInto<EnemyModel>();
        Container.Bind<IMovement>().To<EnemyMovement>().AsSingle();

        Container.BindInstance(enemyMovement).WhenInjectedInto<EnemyMovement>();
        Container.Bind<HealthModel>().AsTransient().WithArguments(healthSettings);
        Container.BindInterfacesAndSelfTo<HealthPresenter>().AsSingle();
        Container.BindInterfacesAndSelfTo<HealthView>().FromComponentInHierarchy()
            .AsSingle();

        Container.DeclareSignalWithInterfaces<SignalOnUnitDamage>();
        Container.DeclareSignalWithInterfaces<SignalOnUnitHeal>();

        Container.BindFactory<BulletRuntimeSettings, BulletView, BulletView.Factory>()
        .FromMonoPoolableMemoryPool(
        x => x.WithInitialSize(weaponSettings.BulletCount)
            .FromComponentInNewPrefab(weaponSettings.BulletPrefab)
            .UnderTransform(new GameObject("[Enemy Bullets]").transform));

        Container.Bind<IWeaponModel>().To<WeaponModel>()
        .AsSingle()
            .WithArguments(weaponSettings);
        Container.Bind<IWeaponPresentor>()
            .To(weaponSettings.WeaponType)
            .AsSingle()
            .WhenInjectedInto<EnemyModel>();

        Container.Bind<EnemyModel>().AsSingle();
        Container.BindInterfacesAndSelfTo<EnemyPresenter>().AsSingle();

        Container.BindSignal<SignalOnUnitDied>().ToMethod<EnemyPresenter>(x => x.OnDeath).FromResolve();
    }
}

