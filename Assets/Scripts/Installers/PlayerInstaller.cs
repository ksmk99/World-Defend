using Installers;
using System;
using Unit;
using Unit.Bullet;
using UnityEngine;
using Zenject;
using static Zenject.CheatSheet;

public class PlayerInstaller : MonoInstaller
{
    [Header("Settings")]
    [SerializeField] private Settings settings;
    [SerializeField] private HealthSettings healthSettings;
    [SerializeField] private WeaponSettings weaponSettings;

    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        Container.DeclareSignalWithInterfaces<SignalOnUnitDamage>();
        Container.DeclareSignalWithInterfaces<SignalOnUnitHeal>();
        Container.DeclareSignal<SignalOnUnitDied>();

        Container.Bind<InputService>()
            .AsSingle()
            .WithArguments(settings.Joystick);
        Container.Bind<HealthModel>()
            .AsSingle()
            .WithArguments(healthSettings);
        Container.Bind<IWeaponModel>()
            .To<WeaponModel>()
            .AsSingle()
            .WithArguments(Team.Ally, weaponSettings);

        Container.BindFactory<BulletRuntimeSettings, BulletView, BulletView.Factory>()
            .FromMonoPoolableMemoryPool(
                x => x.WithInitialSize(weaponSettings.BulletCount)
                .FromComponentInNewPrefab(weaponSettings.BulletPrefab)
                .UnderTransformGroup("Bullet Pool"));

        Container.Bind<IWeapon>().To(weaponSettings.WeaponType).AsTransient();
        Container.Bind<IHealthPresentor>().To<HealthPresentor>().AsTransient();
        Container.Bind<IMovement>().To<PlayerMovement>().AsTransient();

        Container.BindInterfacesAndSelfTo<PlayerModel>()
            .AsSingle()
            .WithArguments(settings.Transform);
        Container.Bind<PlayerPresentor>().AsSingle();
        Container.BindInterfacesAndSelfTo<HealthView>()
            .FromComponentInHierarchy().AsSingle();
    }

    [Serializable]
    public class Settings
    {
        [field: SerializeField] 
        public Transform Transform { get; private set; }
        [field: SerializeField] 
        public Joystick Joystick { get; private set; }
    }
}
