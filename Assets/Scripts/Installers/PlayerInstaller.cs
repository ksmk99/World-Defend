using System;
using Unit;
using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private Settings settings;

    public override void InstallBindings()
    {
        Container.Bind<InputService>().AsSingle()
            .WithArguments(settings.Joystick);
        Container.Bind<PlayerModel>().AsSingle()
            .WithArguments(settings.Transform);
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
