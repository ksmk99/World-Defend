using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class RoomsInstaller : MonoInstaller
{
    [SerializeField] private GameObjectContext context;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<GameObjectContext>()
            .AsSingle()
            .WithArguments(context);
    }
}
