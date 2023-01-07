using System;
using Unit;
using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private Settings settings;
    [SerializeField] private HealthSettings healthSettings;

    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        Container.Bind<InputService>().AsSingle()
            .WithArguments(settings.Joystick);
        Container.Bind<PlayerModel>().AsSingle()
            .WithArguments(settings.Transform);
        Container.Bind<HealthModel>().AsSingle()
            .WithArguments(healthSettings);
        Container.Bind<IHealth>().To<HealthPresentor>().AsTransient();
        Container.Bind<IMovement>().To<PlayerMovement>().AsTransient();
        Container.BindInterfacesAndSelfTo<PlayerController>().AsSingle();
    }

    [Serializable]
    public class Settings
    {
        [field: SerializeField] public Transform Transform { get; private set; }
        [field: SerializeField] public Joystick Joystick { get; private set; }
    }
}
