using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit.Bullet;
using Unit;
using UnityEngine;
using Zenject;

public class MobInstaller : MonoInstaller<MobInstaller>
{
    [SerializeField] private HealthSettings healthSettings;
    [SerializeField] private WeaponSettings weaponSettings;
    [SerializeField] private MovementSettings movement;

    public override void InstallBindings()
    {
        Container.Bind<Transform>().FromComponentOnRoot().AsSingle();
        Container.Bind<IMovement>().To<MobMovement>().AsSingle();

        Container.Bind<HealthModel>().AsSingle().WithArguments(healthSettings);
        Container.BindInterfacesAndSelfTo<HealthPresenter>().AsSingle();
        Container.BindInterfacesAndSelfTo<HealthView>().FromComponentInHierarchy()
            .AsTransient();

        Container.DeclareSignalWithInterfaces<SignalOnUnitDamage>();
        Container.DeclareSignalWithInterfaces<SignalOnUnitHeal>();

        Container.BindFactory<BulletRuntimeSettings, BulletView, BulletView.Factory>()
        .FromMonoPoolableMemoryPool(
        x => x.WithInitialSize(weaponSettings.BulletCount)
            .FromComponentInNewPrefab(weaponSettings.BulletPrefab)
            .UnderTransform(new GameObject("[Mob Bullets]").transform));

        Container.Bind<IWeaponModel>().To<WeaponModel>()
            .AsTransient()
            .WithArguments(weaponSettings);
        Container.Bind<IWeaponPresentor>()
            .To(weaponSettings.WeaponType)
            .AsTransient();

        Container.Bind<MobView>().FromComponentOnRoot().AsSingle().WhenInjectedInto<MobPresenter>();
        Container.Bind<MobModel>().AsSingle();
        Container.BindInterfacesAndSelfTo<MobPresenter>().AsSingle();

        Container.BindSignal<SignalOnUnitDied>().ToMethod<MobPresenter>(x => x.OnDeath).FromResolve();
    }
}

