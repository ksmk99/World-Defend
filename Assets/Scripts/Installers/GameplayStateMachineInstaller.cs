﻿using GameplayState;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace Installers
{
    public class GameplayStateMachineInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<GameplayStateMachine>()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<Bootstrap>()
                .AsSingle();
        }
    }
}
