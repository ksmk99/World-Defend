using Gameplay;
using Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class RoomsInstaller : MonoInstaller
{
    [SerializeField] private GameObjectContext[] context;
    [SerializeField] private Joystick joystick;
    [SerializeField] private PoolParentFlag poolParentFlag;

    public override void InstallBindings()
    {
        Container.BindInstance(poolParentFlag);
        Container.Bind<InputService>()
            .AsSingle()
            .WithArguments(joystick);
        Container.BindInterfacesAndSelfTo<GameObjectContext>()
            .AsSingle()
            .WithArguments(context);
    }

    private void OnValidate()
    {
        var index = 1;
        foreach (var context in context)
        {
            var installers = context.GetComponentsInChildren<AUnitInstaller>();
            foreach (var installer in installers) 
            {
                installer.RoomIndex = index;
            }

            index++;
        }
    }
}
