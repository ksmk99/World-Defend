using System;
using Unit;
using UnityEngine;
using Zenject;
using static Zenject.CheatSheet;

public class PlayerInstaller : MonoInstaller
{
    [Header("References")]
    //[SerializeField] private HealthView healthView;
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
        Container.Bind<PlayerModel>()
            .AsSingle()
            .WithArguments(settings.Transform);
        Container.Bind<HealthModel>()
            .AsSingle()
            .WithArguments(healthSettings);
        Container.Bind<IWeaponModel>()
            .To<WeaponModel>()
            .AsSingle()
            .WithArguments(weaponSettings);

        Container.Bind<IWeapon>().To(weaponSettings.WeaponType).AsTransient();
        Container.Bind<IHealth>().To<HealthPresentor>().AsTransient();
        Container.Bind<IMovement>().To<PlayerMovement>().AsTransient();

        Container.BindInterfacesAndSelfTo<PlayerPresentor>().AsSingle();
        Container.BindInterfacesAndSelfTo<HealthView>()
            .FromComponentInHierarchy().AsSingle();
    }

    [Serializable]
    public class Settings
    {
        [field: SerializeField] public Transform Transform { get; private set; }
        [field: SerializeField] public Joystick Joystick { get; private set; }
    }
}
