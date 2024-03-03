using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit.Bullet;
using Unit;
using UnityEngine;
using Zenject;
using Helpers;

public class EnemyInstaller : MonoInstaller<EnemyInstaller>
{
    [SerializeField] private HealthSettings healthSettings;
    [SerializeField] private WeaponSettings weaponSettings;
    [SerializeField] private EnemyMovementSettings enemyMovement;
    [SerializeField] private HealthView healthPrefab;
    [SerializeField] private Sprite healthBarIcon;

    public override void InstallBindings()
    {
        Container.Bind<EnemyView>().FromComponentOnRoot().AsSingle();

        BindMovement();
        BindHealth();
        BindWeapon();

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

    private void BindSignals()
    {
        Container.DeclareSignalWithInterfaces<SignalOnUnitDamage>();
        Container.DeclareSignalWithInterfaces<SignalOnUnitHeal>();

        Container.BindSignal<SignalOnRoomReset>()
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
            .WithArguments(healthBarIcon);
    }

    private void BindWeapon()
    {
        Container.BindFactory<BulletRuntimeSettings, BulletView, BulletView.Factory>()
        .FromMonoPoolableMemoryPool(
        x => x.WithInitialSize(weaponSettings.BulletCount)
            .FromComponentInNewPrefab(weaponSettings.BulletPrefab));

        Container.Bind<IWeaponModel>().To<WeaponModel>()
        .AsSingle()
            .WithArguments(weaponSettings);
        Container.Bind<IWeaponPresenter>()
            .To(weaponSettings.WeaponType)
            .AsSingle()
            .WhenInjectedInto<EnemyModel>();
    }
}

